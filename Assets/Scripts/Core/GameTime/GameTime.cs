using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.GameTime
{
    public class GameTime : IInitializable, IDisposable
    {
        private bool _gameStarted;
        private float _seconds;
        private float _powerUpSeconds;
        private DateTime _timeWhenPauseActivate;
        private readonly ReactiveProperty<int> _totalSeconds = new();
        private readonly CompositeDisposable _compositeDisposable = new();
        private Action _onPowerUpTimerEnded;
        private float _activeTokenTime;
        private bool _isGameEnded;

        public ReactiveProperty<bool> Pause { get; private set; } = new();
        public IReadOnlyReactiveProperty<int> TotalSeconds => _totalSeconds;

        public event Action<bool> PauseStatusChanged;
        public event Action OnGameEnding;

        public void Initialize()
        {
            Observable.EveryUpdate().Subscribe(_ => Update()).AddTo(_compositeDisposable);
        }

        private void Update()
        {
            if (!_gameStarted || Pause.Value || _isGameEnded) return;

            _seconds += Time.deltaTime;
            if (_seconds > 1f)
            {
                _seconds--;
                _totalSeconds.Value++;
            }
        }

        public void AddTimeAction(TimeType timeActionType)
        {
            Pause.Value = timeActionType switch
            {
                TimeType.PauseStart => true,
                TimeType.PauseFinish => false,
                _ => Pause.Value
            };
            PauseStatusChanged?.Invoke(Pause.Value);
        }

        public void StartGame()
        {
            Pause.Value = false;
            _gameStarted = true;
        }

        public void StopGame()
        {
            _isGameEnded = true;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}