using System;
using UnityEngine;
using Utils;

namespace Objects
{
    public class StartLine : MonoBehaviour
    {
        public event Action OnStartBallMoving;

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.collider.CompareTag(StringConstants.PlatformTag)) return;
            
            OnStartBallMoving?.Invoke();
            Destroy(this);
        }
    }
}