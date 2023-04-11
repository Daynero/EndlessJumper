using System;
using Enums;
using UniRx;
using UnityEngine;
using Utils;
using Random = UnityEngine.Random;

namespace Objects.Platforms
{
    public class PlatformController
    {
        private PlatformSpawnerView _spawnerView;
        
        public PlatformController(PlatformSpawnerView spawnerView)
        {
            _spawnerView = spawnerView;
            SpawnPlatform(Spawn.Top);
            SpawnPlatform(Spawn.Bottom);
        }

        private void SpawnPlatform(Spawn spawn)
        {
            var newPlatform = _spawnerView.SpawnPlatform(spawn);
            SetInitialSettingsForPlatform(newPlatform, spawn);
        }

        private void SetInitialSettingsForPlatform(PlatformView newPlatform, Spawn spawn)
        {
            BoxCollider2D platformCollider = newPlatform.GetComponent<BoxCollider2D>();
            Rigidbody2D platformRigidbody = newPlatform.GetComponent<Rigidbody2D>();
            RectTransform platformRectTransform = newPlatform.GetComponent<RectTransform>();
            Transform platformTransform = newPlatform.transform;
            float platformWidth = GlobalConstants.RandomPlatformWidth;

            newPlatform.Spawn = spawn;
            platformRectTransform.localPosition = Vector3.zero;
            platformCollider.offset = new Vector2(platformWidth / 2, 0);
            platformRigidbody.velocity = Vector2.left * GlobalConstants.PlatformSpeed;
            Vector2 sizeDelta = platformRectTransform.sizeDelta;
            platformCollider.size = new Vector2(platformWidth, sizeDelta.y);
            sizeDelta.x = platformWidth;
            platformRectTransform.sizeDelta = sizeDelta;
            newPlatform.OnOutOfTheSpawner += SpawnNewPlatform;
        }

        private void SpawnNewPlatform(Spawn spawn)
        {
            Observable.Timer(TimeSpan.FromSeconds(Random.Range(1,2)))
                .Subscribe(_ =>
                {
                    SpawnPlatform(spawn);
                });
        }
    }
}