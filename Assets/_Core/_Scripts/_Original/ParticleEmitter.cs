using _Core._Scripts.Clean_Architecture;
using UnityEngine;

namespace _Core._Scripts._Original
{
    public class ParticleEmitter : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;

        private void OnEnable() => EventBus<InteractableEventData>.Event += this.Emit;

        private void OnDisable() => EventBus<InteractableEventData>.Event -= this.Emit;

        private void Emit(InteractableEventData interactablePayload) => this.particles?.Play();
    }
}