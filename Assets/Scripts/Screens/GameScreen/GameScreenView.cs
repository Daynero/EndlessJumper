using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Screens.GameScreen
{
    public class GameScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Button pauseButton;
        
        private CanvasGroup _screenCanvasGroup;

        public event Action OnPauseClick;

        private void Awake()
        {
            _screenCanvasGroup = GetComponent<CanvasGroup>();
            pauseButton.ActionWithThrottle(() => OnPauseClick?.Invoke());
        }

        public void BlockScreenTouches(bool isBlock)
        {
            _screenCanvasGroup.blocksRaycasts = !isBlock;
        }

        public void SetScore(int score)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}