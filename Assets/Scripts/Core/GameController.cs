using UnityEngine;

namespace Core
{
    public class GameController : MonoBehaviour
    {
        // private BoardSquaresController _boardSquaresController;
        // private TokenLineController _tokenLineController;
        // private ScoreController _scoreController;
        // private StepsController _stepsController;
        //
        // private bool _gameInFocus;
        // private string _gameId;
        // private string _userId;
        // private GameTime.GameTime _gameTime;
        // private ScreenNavigationSystem _screenNavigationSystem;
        // private SettingsPopupPresenter _settingsPopupPresenter;
        // private GameScreenPresenter _gameScreenPresenter;
        // private PowerUpsController _powerUpsController;
        // private BingoController _bingoController;
        // private float _endGameAnimationTimer;
        // private Action AllAnimationCompleted;
        // private ApiInterface _apiInterface;
        //
        // [Inject]
        // public void Construct(BoardSquaresController boardSquaresController, TokenLineController tokenLineController,
        //     ScoreController scoreController, GameTime.GameTime gameTime, ScreenNavigationSystem screenNavigationSystem,
        //     SettingsPopupPresenter settingsPopupPresenter, GameScreenPresenter gameScreenPresenter,
        //     PowerUpsController powerUpsController, BingoController bingoController, StepsController stepsController,
        //     ApiInterface apiInterface)
        // {
        //     _apiInterface = apiInterface;
        //     _boardSquaresController = boardSquaresController;
        //     _tokenLineController = tokenLineController;
        //     _scoreController = scoreController;
        //     _gameTime = gameTime;
        //     _screenNavigationSystem = screenNavigationSystem;
        //     _settingsPopupPresenter = settingsPopupPresenter;
        //     _gameScreenPresenter = gameScreenPresenter;
        //     _powerUpsController = powerUpsController;
        //     _bingoController = bingoController;
        //     _stepsController = stepsController;
        // }
        //
        // private void Start()
        // {
        //     var challengeData = XmannaAdapter.instance.GetCurrentChallengeData();
        //     _gameId = challengeData._id;
        //     _apiInterface.GetPickOfTokens(_gameId,
        //         data => 
        //             XmannaAdapter.instance.GameStarted(onSuccess: () => InitializeGameSession(data), OnError),
        //         status =>
        //         {
        //             Debug.LogError(status.errorMessage);
        //             OnError();
        //         });
        //
        //     void OnError()
        //     {
        //         XmannaAdapter.instance.ComeBackToSDKBecauseWeHadErrorInGame();
        //     }
        // }
        //
        // private void InitializeGameSession(GameSessionData gameSessionData)
        // {
        //     _tokenLineController.Init(gameSessionData.tokens);
        //     _boardSquaresController.Init(gameSessionData.board);
        //     _powerUpsController.SetPowerUpsList(gameSessionData.bonuses);
        //     _stepsController.ClearStepsList();
        //     _gameInFocus = true;
        //     SubscribeOnGameEnding();
        //     XmannaAdapter.instance.ShowHideXmannaScreens(false);
        //     Debug.Log("GameSessionData: " + JsonUtility.ToJson(gameSessionData));
        // }
        //
        // private void SubscribeOnGameEnding()
        // {
        //     _settingsPopupPresenter.OnGameEnding += () => EndGame(GameEndType.GameOver);
        //     _gameTime.OnGameEnding += () => EndGame(GameEndType.TimeOver);
        //     _bingoController.AllBingoCollected += () => EndGame(GameEndType.GameCompleted);
        // }
        //
        // private void EndGame(GameEndType gameEndType)
        // {
        //     _tokenLineController.StopShowingTokens();
        //     SoundsManager.instance.PlaySound(AudioKey.TimesUpWin);
        //     _gameTime.StopGame();
        //     _gameScreenPresenter.BlockBoardTouches(true);
        //     _gameScreenPresenter.ShowTopAnimation(TopAnimationType.GameOver);
        //     AnimationsController.StopAddingAnimations(() => EndGameAnimationTimer(gameEndType));
        // }
        //
        // private void EndGameAnimationTimer(GameEndType gameEndType)
        // {
        //     GameResult gameResult = _scoreController.GetGameResults();
        //     gameResult.GameEndType = gameEndType;
        //     
        //     GameResultModel result = new GameResultModel()
        //     {
        //         challengeId = _gameId,
        //         userId = _gameId,
        //         scores = new GameResultScoresModel
        //         {
        //             correctDaubsScore = gameResult.DaubPoints,
        //             speedBonusScore = gameResult.SpeedBonusPoints,
        //             bingosScore = gameResult.BingoPoints,
        //             bingoBonusScore = gameResult.BingoBonus,
        //             boostBonusScore = gameResult.PowerUpPoints,
        //             timeBonusScore = gameResult.TimeBonus,
        //             penalty = gameResult.PenaltyPoints,
        //             totalScore = gameResult.FinalScore
        //         },
        //         gameActions = _stepsController.GetAllSteps(),
        //         timeActions = _gameTime.GetTimeActions()
        //     };
        //     string jsonStr = JsonUtility.ToJson(result);
        //     Debug.Log("GameResult: " + jsonStr);
        //     
        //     _apiInterface.VerifyScore(result, data =>
        //     {
        //         Debug.Log("Bingo Score valid is: " + data.body.valid);
        //         if (data.body.valid)
        //         {
        //             _screenNavigationSystem.Show(ScreenName.Results, gameResult);
        //         }
        //         else
        //         {
        //             Debug.LogError("Invalid Score!");
        //         }
        //     }, (ResponseStatus status) => Debug.LogError(status.errorMessage));
        // }
        //
        // private void OnApplicationFocus(bool hasFocus)
        // {
        //     if (!_gameInFocus) return;
        //     if (!hasFocus && !_gameTime.Pause)
        //     {
        //         _screenNavigationSystem.Show(ScreenName.Paused);
        //     }
        //
        //     _gameTime.ChangeApplicationFocus(hasFocus);
        // }
    }
}