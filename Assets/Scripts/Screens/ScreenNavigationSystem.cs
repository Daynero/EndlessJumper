using System.Collections.Generic;
using Screens.GamePausedPopup;
using Screens.GameResultPopup;
using Zenject;

namespace Screens
{
    public class ScreenNavigationSystem : IInitializable
    {
        private ScreenName _openedScreen = ScreenName.Empty;
        private ScreenPresenter _currScreen;

        private readonly Dictionary<ScreenName, ScreenPresenter> _screenPresenters;

        public ScreenNavigationSystem(GamePausedPopupPresenter gamePausedPopupPresenter,
            GameResultPopupPresenter gameResultPopupPresenter)
        {
            _screenPresenters = new Dictionary<ScreenName, ScreenPresenter>
            {
                {ScreenName.Paused, gamePausedPopupPresenter},
                {ScreenName.Result, gameResultPopupPresenter},
            };
        }

        public void Initialize()
        {
            foreach (ScreenPresenter screenPresenter in _screenPresenters.Values)
            {
                screenPresenter.OnCloseAction = delegate
                {
                    _openedScreen = ScreenName.Empty;
                    _currScreen = null;
                };
            }
        }

        public void Show(ScreenName screenName, object extraData = null)
        {
            if (_openedScreen == screenName) return;
            if (_openedScreen != ScreenName.Empty)
            {
                CloseCurrentScreen();
            }

            _openedScreen = screenName;
            _currScreen = _screenPresenters[screenName];
            _currScreen?.ShowScreen(extraData);
        }

        public void CloseCurrentScreen()
        {
            _currScreen?.CloseScreen();
        }
    }
}