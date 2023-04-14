using System;
using Core.GameTime;
using Objects.Ball;
using Objects.Platforms;
using Objects.Score;
using Screens;
using Screens.GameScreen;
using UnityEngine;
using Zenject;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        private ScoreController _scoreController;
        private GameTime.GameTime _gameTime;
        private ScreenNavigationSystem _screenNavigationSystem;
        private GameScreenPresenter _gameScreenPresenter;
        private BallController _ballController;
        private PlatformController _platformController;
        private bool _gameInFocus;
        private string _gameId;
        private string _userId;
        private float _endGameAnimationTimer;
        private Action AllAnimationCompleted;

        [Inject]
        public void Construct(ScoreController scoreController, GameTime.GameTime gameTime,
            ScreenNavigationSystem screenNavigationSystem, GameScreenPresenter gameScreenPresenter,
            BallController ballController,
            PlatformController platformController)
        {
            _scoreController = scoreController;
            _gameTime = gameTime;
            _screenNavigationSystem = screenNavigationSystem;
            _gameScreenPresenter = gameScreenPresenter;
            _ballController = ballController;
            _platformController = platformController;
            _ballController.OnGameEnded += EndGame;
        }


        private void Start()
        {
            _gameInFocus = true;
        }

        private void EndGame()
        {
            _gameTime.AddTimeAction(TimeType.GameFinish);
            _gameScreenPresenter.BlockBoardTouches(true);
            _screenNavigationSystem.Show(ScreenName.Result, _scoreController.TotalScore.Value);
        }
    }
}