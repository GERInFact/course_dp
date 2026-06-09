using System;
using _Core._Scripts.Clean_Architecture;
using UnityEngine;

namespace _Core._Scripts._Original
{
    public class SoundEffectPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip audioClip;
        [SerializeField] private AudioSource audioSource;

        private void OnEnable() => EventBus<InteractableEventData>.Event += this.PlayEffect;
        private void OnDisable() => EventBus<InteractableEventData>.Event -= this.PlayEffect;

        private void PlayEffect(InteractableEventData interactablePayload)
        {
            if(this.audioClip == null || this.audioSource == null) return;
            
            this.audioSource.PlayOneShot(this.audioClip);
        }
    }
}