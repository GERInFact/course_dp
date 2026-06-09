using System.Linq;
using _Core._Scripts.Clean_Architecture;
using UnityEngine;

namespace _Core._Scripts._Original
{
    public class MonolithInteractable : MonoBehaviour
    {
        private bool isHit;
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (this.isHit || !IsPlayer(other) && IsHitFromAbove(other)) return;

            this.isHit = true;
            OnInteracted();
        }

        private static bool IsPlayer(Collision2D other) => other.gameObject.CompareTag("Player");
        private static bool IsHitFromAbove(Collision2D other) =>other.contacts.FirstOrDefault().normal.y < 0.2f;
        private static void OnInteracted() => EventBus<InteractableEventData>.Raise(new InteractableEventData());
    }
}