using System;
using Enums;
using UnityEngine;
using Utils;

namespace Objects.Platforms
{
    public class PlatformView : MonoBehaviour
    {
        public Spawn Spawn { private get; set; }
        public event Action<Spawn> OnOutOfTheSpawner;
        public event Action OnDeactivatePlatform;

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.collider.CompareTag(StringConstants.SpawnTag))
            {
                OnOutOfTheSpawner?.Invoke(Spawn);
            }
            else if (other.collider.CompareTag(StringConstants.DestroingLineTag))
            {
                OnDeactivatePlatform?.Invoke();
            }
        }
    }
}