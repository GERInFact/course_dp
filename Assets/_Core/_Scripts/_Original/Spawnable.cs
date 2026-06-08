using UnityEngine;

namespace _Core._Scripts._Original
{
    public class Spawnable : MonoBehaviour
    {
        public void Disable () => this.gameObject.SetActive(false);
    }
}