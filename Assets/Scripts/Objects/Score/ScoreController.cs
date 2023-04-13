using System;
using Core.GameTime;
using UniRx;
using Zenject;

namespace Objects.Score
{
    public class ScoreController : IInitializable
    {
        private readonly GameTime _gameTime;
        public event Action<int> ScoreChanged;
        IReactiveProperty<int> TotalScore { get; set; }

        public ScoreController(GameTime gameTime)
        {
            _gameTime = gameTime;
        }

        public void Initialize()
        {

        }

    }
}