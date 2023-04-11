using System;
using UniRx;
using Zenject;

namespace Core.GameTime
{
    public class GameTime : IInitializable, IDisposable
    {
        private bool _gameStarted = false;
        private float _seconds;
        private float _powerUpSeconds;
        private DateTime _timeWhenPauseActivate;
        private readonly CompositeDisposable _compositeDisposable = new();
        private Action _onPowerUpTimerEnded;
        private float _activeTokenTime;
        private bool _isGameEnded;

        public bool Pause { get; private set; }

        public event Action<bool> PauseStatusChanged;
        public event Action OnGameEnding;

        public void Initialize()
        {
            
        }
        
        public void AddTimeAction(TimeType timeActionType)
        {
            Pause = timeActionType switch
            {
                TimeType.PauseStart => true,
                TimeType.PauseFinish => false,
                _ => Pause
            };
            PauseStatusChanged?.Invoke(Pause);
        }

        public void StartGame()
        {
            Pause = false;
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