using System;
using Enums;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;
using Utils;

namespace Objects.Platforms
{
    public class PlatformView : MonoBehaviour, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        public Spawn Spawn { private get; set; }
        public event Action<Spawn, PlatformView> OnOutOfTheSpawner;
        public event Action OnDeactivatePlatform;

        private void OnCollisionExit2D(Collision2D other)
        {
            switch (other.collider.tag)
            {
                case StringConstants.SpawnTag:
                    OnOutOfTheSpawner?.Invoke(Spawn, this);
                    break;
                case StringConstants.DestroyingLineTag:
                    OnDeactivatePlatform?.Invoke();
                    break;
            }
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}