using DG.Tweening;
using TMPro;
using UnityEngine;
using Utils;
using static DG.Tweening.DOTween;

namespace Animations
{
    public class ScoreAnimation : MonoBehaviour
    {
        private TextMeshProUGUI _text;
        private readonly Vector3 _targetScale = Vector3.one * 1.1f;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }

        public void PlayAnimation()
        {
            Sequence()
                .Append(_text.transform.DOScale(_targetScale, 0.1f))
                .Append(_text.transform.DOScale(Vector3.one, 0.1f))
                .AddToController();
        }
    }
}