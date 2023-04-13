using System;
using UnityEngine;

namespace Objects.Platforms
{
    public class PlatformSpawnerView : MonoBehaviour
    {
        [SerializeField] private PlatformView platformPrefab;
        [SerializeField] private Transform topSpawner;
        [SerializeField] private Transform bottomSpawner;
        [SerializeField] private StartLine startLine;

        public event Action OnStartBallMoving;
        public PlatformView PlatformPrefab => platformPrefab;
        public Transform TopSpawnerTransform => topSpawner;
        public Transform BottomSpawnerTransform => bottomSpawner;

        private void Start()
        {
            startLine.OnStartBallMoving += OnStartBallMoving;
        }
    }
}