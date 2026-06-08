using System.Collections;
using System.Linq;
using UnityEngine;

namespace _Core._Scripts._Original
{
    public class MonolithInteractable : MonoBehaviour
    {
        [Header("Spawning")] [SerializeField] private GameObject prefabToSpawn;
        [SerializeField] private float spawnDelayInSeconds;
        [SerializeField] private Transform spawnPoint;
        [SerializeField] private int spawnCount;

        [Header("Effects")] [SerializeField] private ParticleSystem particles;
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private AudioClip audioClip;

        [Header("Trigger Objects")] [SerializeField]
        private GameObject doorToOpen;

        private void OnCollisionEnter2D(Collision2D other)
        {
            var normal = other.contacts.FirstOrDefault().normal;
            
            Debug.Log(normal);
            
            if (!other.gameObject.CompareTag("Player") && normal.y < 0.2f ||
                this.prefabToSpawn == null || this.spawnPoint == null || this.particles == null ||
                this.audioSource == null || this.audioClip == null || this.doorToOpen == null) return;

            this.StopAllCoroutines();
            this.StartCoroutine(this.SpawningCoroutine());

            this.audioSource.PlayOneShot(this.audioClip);
            this.audioSource.PlayOneShot(this.audioClip);

            this.doorToOpen.SetActive(true);
        }

        private IEnumerator SpawningCoroutine()
        {
            for (var i = 0; i < this.spawnCount; i++)
            {
                Instantiate(this.prefabToSpawn, this.spawnPoint.position, Quaternion.identity);
                yield return new WaitForSeconds(this.spawnDelayInSeconds);
            }

            this.particles.Play();
        }
    }
}