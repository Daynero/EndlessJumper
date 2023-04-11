using System;
using Core.GameTime;
using UniRx;
using Zenject;

namespace Screens.GamePausedPopup
{
    public class GamePausedPopupPresenter : ScreenPresenter, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly GamePausedPopupView _view;
        private readonly GameTime _gameTime;
        private readonly ScreenNavigationSystem _screenNavigationSystem;

        public GamePausedPopupPresenter(GamePausedPopupView view, GameTime gameTime)
        {
            _view = view;
            _gameTime = gameTime;
        }

        public void Initialize()
        {
            _view.OnResumeButtonClick += CloseScreen;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public override void ShowScreen(object extraData = null)
        {
            _gameTime.AddTimeAction(TimeType.PauseStart);
            _view.OpenCloseScreen(true);
        }

        public override void CloseScreen()
        {
            _gameTime.AddTimeAction(TimeType.PauseFinish);
            OnCloseAction.Invoke();
            _view.OpenCloseScreen(false);
        }
    }
}