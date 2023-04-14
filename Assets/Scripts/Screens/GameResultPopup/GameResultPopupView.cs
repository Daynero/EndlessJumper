using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Screens.GamePausedPopup
{
    public class GameResultPopupView : ScreenView
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private TextMeshProUGUI totalScore;
        
        public event Action OnRestartButtonClick;

        private new void Awake()
        {
            base.Awake();
            restartButton.ActionWithThrottle(() => OnRestartButtonClick?.Invoke());
        }

        public void SetScore(int score)
        {
            totalScore.text = $"Total score: {score}";
        }
    }
}