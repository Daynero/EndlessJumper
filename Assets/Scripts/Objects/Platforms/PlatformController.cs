using System;
using Core.GameTime;
using Enums;
using Objects.Ball;
using UniRx;
using UnityEngine;
using Utils;
using static Utils.GlobalConstants;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Objects.Platforms
{
    public class PlatformController : IDisposable
    {
        private readonly PlatformSpawnerView _spawnerView;
        private readonly Pool<PlatformView> _topPlatformPool;
        private readonly Pool<PlatformView> _bottomPlatformPool;
        private readonly BallController _ballController;
        private readonly CompositeDisposable _disposables = new();
        private bool _isPaused;

        public PlatformController(PlatformSpawnerView spawnerView, BallController ballController, GameTime gameTime)
        {
            _spawnerView = spawnerView;
            _ballController = ballController;
            gameTime.Pause.Subscribe(StopOrMovePlatforms).AddTo(_disposables);
            _topPlatformPool = CreatePoolWith(_spawnerView.TopSpawnerTransform);
            _bottomPlatformPool = CreatePoolWith(_spawnerView.BottomSpawnerTransform);
            SpawnPlatform(Spawn.Top, true);
            SpawnPlatform(Spawn.Bottom, true);
        }

        private Pool<PlatformView> CreatePoolWith(Transform spawnerTransform) =>
            new(PlatformPoolSize, _spawnerView.PlatformPrefab, spawnerTransform);

        private void SpawnPlatform(Spawn spawn, bool isFirst = false)
        {
            var newPlatform = spawn == Spawn.Top ? _topPlatformPool.Create() : _bottomPlatformPool.Create();
            SetInitialSettingsForPlatform(newPlatform, spawn, isFirst);
            _spawnerView.OnStartBallMoving += StartGame;
        }

        private void SetInitialSettingsForPlatform(PlatformView newPlatform, Spawn spawn,  bool isFirst = false)
        {
            BoxCollider2D platformCollider = newPlatform.GetComponent<BoxCollider2D>();
            Rigidbody2D platformRigidbody = newPlatform.GetComponent<Rigidbody2D>();
            RectTransform platformRectTransform = newPlatform.GetComponent<RectTransform>();
            float platformWidth = isFirst ? MaxPlatformWidth : RandomPlatformWidth;

            newPlatform.Spawn = spawn;
            platformRectTransform.localPosition = Vector3.zero;
            platformCollider.offset = new Vector2(platformWidth / 2, 0);
            platformRigidbody.velocity = _isPaused ? Vector2.zero : Vector2.left * PlatformSpeed;
            Vector2 sizeDelta = platformRectTransform.sizeDelta;
            platformCollider.size = new Vector2(platformWidth, sizeDelta.y);
            sizeDelta.x = platformWidth;
            platformRectTransform.sizeDelta = sizeDelta;
            newPlatform.OnOutOfTheSpawner += SpawnNewPlatform;
            newPlatform.OnDeactivatePlatform += () => newPlatform.gameObject.SetActive(false);
        }

        private void StartGame()
        {
            _ballController.StartBallMoving();
            _spawnerView.OnStartBallMoving -= StartGame;
        }

        private void SpawnNewPlatform(Spawn spawn, PlatformView newPlatform)
        {
            float nextPlatformSpawnTime = Random.Range(1, 2);

            Observable.Timer(TimeSpan.FromSeconds(nextPlatformSpawnTime))
                .Subscribe(_ =>
                {
                    SpawnPlatform(spawn);
                    newPlatform.OnOutOfTheSpawner -= SpawnNewPlatform;
                })
                .AddTo(_disposables);
        }

        private void StopOrMovePlatforms(bool isPaused)
        {
            _isPaused = isPaused;
            PlatformView[] platforms = Object.FindObjectsOfType<PlatformView>();
            foreach (PlatformView platform in platforms)
            {
                Rigidbody2D platformRigidbody = platform.GetComponent<Rigidbody2D>();
                platformRigidbody.velocity = isPaused ? Vector2.zero : Vector2.left * PlatformSpeed;
            }
        }

        public void Dispose()
        {
            _disposables?.Dispose();
        }
    }
}