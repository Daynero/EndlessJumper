using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Screens.GamePausedPopup
{
    public class GamePausedPopupView : ScreenView
    {
        [SerializeField] private Button resumeButton;
        
        public event Action OnResumeButtonClick;

        private new void Awake()
        {
            base.Awake();
            resumeButton.ActionWithThrottle(() => OnResumeButtonClick?.Invoke());
        }
    }
}