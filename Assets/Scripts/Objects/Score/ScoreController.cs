using System;
using Core.GameTime;
using Zenject;

namespace Objects.Score
{
    public class ScoreController : IInitializable
    {
        private readonly GameTime _gameTime;
        public event Action<int> ScoreChanged;

        public ScoreController(GameTime gameTime)
        {
            _gameTime = gameTime;
        }

        public void Initialize()
        {
        }

        //     _boardSquaresController.WrongSquare += () =>
        //     {
        //         OnShowTopAnimation?.Invoke(TopAnimationType.WrongToken, TopAnimationBingoType.Empty);
        //         TryToPenalty(GlobalConstants.WrongDaubPenalty);
        //         Debug.Log($"WrongSquare! Score: {_gameResult.TotalScore}");
        //     };
        //
        //     _bingoController.BingoCollected += bingoCount =>
        //     {
        //         _gameResult.BingoPoints = GlobalConstants.BingoPoints * bingoCount;
        //         _gameResult.BingoCount += bingoCount;
        //         int totalScore = _gameResult.TotalScore;
        //         
        //         TopAnimationBingoType prefixType = (TopAnimationBingoType) _gameResult.BingoCount;
        //         
        //         OnShowTopAnimation?.Invoke(TopAnimationType.Bingo, prefixType);
        //         ScoreChanged?.Invoke(totalScore);
        //
        //         Debug.Log($"{bingoCount} Bingo! Score: {totalScore}");
        //     };
        //
        //     _bingoController.WrongBingo += () =>
        //     {
        //         OnShowTopAnimation?.Invoke(TopAnimationType.WrongBingo, TopAnimationBingoType.Empty);
        //         TryToPenalty(GlobalConstants.WrongBingoPenalty);
        //         Debug.Log($"Bingo penalty! Score: {_gameResult.TotalScore}");
        //     };
        // }

    }
}