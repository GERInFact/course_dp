using System.Linq;
using UnityEngine;

namespace _Core._Scripts._Original
{
    public class PlayerMonolith : MonoBehaviour
    {
        [SerializeField] private float speedDelta;
        [SerializeField] private float jumpForceDelta;
        [SerializeField] private float maxAirTime;
        [SerializeField] private int maxJumpCount;

        private Rigidbody2D physics2D;
        private float jumpTimer;
        private int remainingJumpCount;

        private void Awake() =>
            this.physics2D = this.GetComponent<Rigidbody2D>() ?? this.gameObject.AddComponent<Rigidbody2D>();

        private void Start() => this.remainingJumpCount = this.maxJumpCount;

        private void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");
            this.physics2D.linearVelocityX = horizontal * this.speedDelta;

            if (Input.GetButtonDown("Jump") && this.remainingJumpCount > 0)
            {
                --this.remainingJumpCount;
                this.jumpTimer = 0;
                this.physics2D.linearVelocityY = this.jumpForceDelta;
            }

            if (!Input.GetButton("Jump") || (!(this.jumpTimer <= this.maxAirTime)) ||
                this.remainingJumpCount <= 0) return;
            this.jumpTimer += Time.deltaTime;
            this.physics2D.linearVelocityY = this.jumpForceDelta;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.contacts.FirstOrDefault().normal.y < 0) return;

            this.remainingJumpCount = this.maxJumpCount;
        }
    }
}