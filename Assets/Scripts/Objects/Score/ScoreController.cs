using System;
using Core.GameTime;
using UniRx;
using Zenject;

namespace Objects.Score
{
    public class ScoreController : IInitializable
    {
        private readonly GameTime _gameTime;
        private readonly CompositeDisposable _compositeDisposable = new();

        public IReactiveProperty<int> TotalScore { get; set; } = new ReactiveProperty<int>();
        
        public ScoreController(GameTime gameTime)
        {
            _gameTime = gameTime;
        }

        public void Initialize()
        {
            _gameTime.TotalSeconds.Subscribe(SetScore).AddTo(_compositeDisposable);
        }

        private void SetScore(int time)
        {
            TotalScore.Value = time;
        }
        
        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}