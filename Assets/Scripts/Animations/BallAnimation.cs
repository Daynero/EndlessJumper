using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using static DG.Tweening.DOTween;

namespace Animations
{
    public class BallAnimation : MonoBehaviour
    {
        private Image _image;
        private readonly Vector3 _beforeJumpScale = new(1, 0.8f, 1);
        private readonly Vector3 _afterJumpScale = new(1, 1.2f, 1);
        private Sequence _jumpSequence;
        private Sequence _gravitySequence;
        private Sequence _shakeSequence;

        public void SetJumpAnimation()
        {
            Vector3 currentAngle = _image.transform.rotation.eulerAngles;
            Vector3 firstJumpStateRotation = currentAngle + new Vector3(0, 0, -180);
            Vector3 secondJumpStateRotation = currentAngle + new Vector3(0, 0, -360);
            _gravitySequence?.Pause();
            _jumpSequence = Sequence()
                .Append(_image.transform.DOScale(_beforeJumpScale, 0))
                .Append(_image.transform.DOScale(_afterJumpScale, 0.2f))
                .Append(_image.transform.DORotate(firstJumpStateRotation, 0.5f).SetEase(Ease.Linear))
                .Append(_image.transform.DORotate(secondJumpStateRotation, 0.5f).SetEase(Ease.Linear))
                .Append(_image.transform.DOScale(_beforeJumpScale, 0.3f))
                .Append(_image.transform.DOScale(Vector3.one, 0.1f))
                .AddToController();
        }

        public void SetGravityAnimation()
        {
            Vector3 currentAngle = _image.transform.rotation.eulerAngles;
            Vector3 firstJumpStateRotation = currentAngle + new Vector3(0, 0, -180);
            _jumpSequence?.Pause();
            _gravitySequence = Sequence()
                .Append(_image.transform.DOScale(_beforeJumpScale, 0))
                .Append(_image.transform.DOScale(_afterJumpScale, 0.05f))
                .Append(_image.transform.DORotate(firstJumpStateRotation, 0.3f).SetEase(Ease.Linear))
                .Append(_image.transform.DOScale(_beforeJumpScale, 0.1f))
                .Append(_image.transform.DOScale(Vector3.one, 0.1f))
                .AddToController();
        }

        public void SetShakeAnimation()
        {
            _image = GetComponent<Image>();
            _shakeSequence = Sequence()
                .Append(_image.transform.DOShakePosition(10f, 15f, 20, 120f).SetEase(Ease.InQuad))
                .AddToController();
        }

        public void StopShakeAnimation()
        {
            if (_shakeSequence != null && _shakeSequence.IsActive()) 
            {
                _shakeSequence.Kill(); 
            }
        }
    }
}