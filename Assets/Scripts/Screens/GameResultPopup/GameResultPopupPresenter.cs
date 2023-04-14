using System;
using Core.GameTime;
using Screens.GamePausedPopup;
using UniRx;
using UnityEngine.SceneManagement;
using Zenject;

namespace Screens.GameResultPopup
{
    public class GameResultPopupPresenter : ScreenPresenter, IInitializable, IDisposable
    {
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly GameResultPopupView _view;
        private readonly GameTime _gameTime;
        private readonly ScreenNavigationSystem _screenNavigationSystem;

        public GameResultPopupPresenter(GameResultPopupView view, GameTime gameTime)
        {
            _view = view;
            _gameTime = gameTime;
        }

        public void Initialize()
        {
            _view.OnRestartButtonClick += CloseScreen;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }

        public override void ShowScreen(object extraData = null)
        {
            _gameTime.AddTimeAction(TimeType.PauseStart);
            _view.OpenCloseScreen(true);
            
            if (extraData != null && int.TryParse(extraData.ToString(), out var score))
            {
                _view.SetScore(score);
            }
        }

        public override void CloseScreen()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}