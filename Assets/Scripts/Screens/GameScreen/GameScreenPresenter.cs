using System;
using Core.GameTime;
using UniRx;
using Unity.VisualScripting;

namespace Screens.GameScreen
{
    public class GameScreenPresenter : IInitializable, IDisposable
    {
        private readonly GameScreenView _view;
        private readonly GameTime _gameTime;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly ScreenNavigationSystem _screenNavigationSystem;

        public GameScreenPresenter(GameScreenView view, GameTime gameTime,
            ScreenNavigationSystem screenNavigationSystem)
        {
            _view = view;
            _gameTime = gameTime;
            _screenNavigationSystem = screenNavigationSystem;

            Initialize();
        }

        public void Initialize()
        {
            _gameTime.TotalSeconds.Subscribe(_view.SetScore).AddTo(_compositeDisposable);
            _view.OnPauseClick += SetGameOnPause;
        }

        public void BlockBoardTouches(bool isBlock)
        {
            _view.BlockScreenTouches(isBlock);
        }

        private void SetGameOnPause()
        {
            _screenNavigationSystem.Show(ScreenName.Paused);
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}