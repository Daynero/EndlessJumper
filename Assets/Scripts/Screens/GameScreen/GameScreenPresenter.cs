using System;
using Objects.Ball;
using Objects.Score;
using UniRx;
using Unity.VisualScripting;

namespace Screens.GameScreen
{
    public class GameScreenPresenter : IInitializable, IDisposable
    {
        private readonly GameScreenView _view;
        private readonly ScreenNavigationSystem _screenNavigationSystem;
        private readonly CompositeDisposable _compositeDisposable = new();
        private readonly BallController _ballController;

        public GameScreenPresenter(GameScreenView view, ScoreController scoreController,
            ScreenNavigationSystem screenNavigationSystem, BallController ballController)
        {
            _view = view;
            _screenNavigationSystem = screenNavigationSystem;
            scoreController.TotalScore.Subscribe(_view.SetScore).AddTo(_compositeDisposable);
            _ballController = ballController;

            Initialize();
        }

        public void Initialize()
        {
            _view.OnPauseClick += SetGameOnPause;
            _ballController.OnJumpOrGravity += _view.AnimateScore;
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