using System;
using Animations;
using Core.GameTime;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Zenject;
using static Utils.GlobalConstants;

namespace Objects.Ball
{
    public class BallController : MonoBehaviour, IDisposable
    {
        [SerializeField] private GameObject border;
        
        private Rigidbody2D _rb;
        private float _gravityModifier = 1f;
        private bool _isGrounded;
        private float _lastClickTime;
        private GameTime _gameTime;
        private bool _isGameStarted;
        private readonly CompositeDisposable _compositeDisposable = new();
        private BallAnimation _ballAnimation;

        public event Action OnGameEnded;
        public event Action OnJumpOrGravity;

        [Inject]
        public void Construct(GameTime gameTime)
        {
            _rb = GetComponent<Rigidbody2D>();
            _gameTime = gameTime;
            _gameTime.Pause.Subscribe(StopOrMoveBall).AddTo(_compositeDisposable);
            _ballAnimation = GetComponent<BallAnimation>();
            _ballAnimation.SetShakeAnimation();
        }

        private void StopOrMoveBall(bool isPaused)
        {
            if (!_isGameStarted) return;
            if (isPaused)
            {
                _rb.velocity = Vector2.zero;
            }

            _rb.bodyType = isPaused ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
        }

        private void Update()
        {
            if (_rb.bodyType == RigidbodyType2D.Static) return;
            if (!Input.GetMouseButtonDown(0)) return;

            GameObject selectedObject = EventSystem.current.currentSelectedGameObject;

            if (selectedObject != null && selectedObject.GetComponent<Button>() != null) return;

            float timeSinceLastClick = Time.time - _lastClickTime;

            if (timeSinceLastClick <= DoubleClickTime)
            {
                ChangeGravity();
            }
            else
            {
                Jump();
            }

            OnJumpOrGravity?.Invoke();
            _lastClickTime = Time.time;
        }

        public void StartBallMoving()
        {
            _isGameStarted = true;
            _rb.bodyType = RigidbodyType2D.Dynamic;
            _gameTime.AddTimeAction(TimeType.GameStart);
            _ballAnimation.StopShakeAnimation();
            border.SetActive(false);
        }

        private void ChangeGravity()
        {
            _ballAnimation.SetGravityAnimation();
            _gravityModifier *= -1;
            _rb.gravityScale = _gravityModifier * 100;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            switch (collision.collider.tag)
            {
                case StringConstants.PlatformTag:
                    _isGrounded = true;
                    break;
                case StringConstants.DestroyingLineTag:
                    Destroy(gameObject);
                    OnGameEnded?.Invoke();
                    break;
            }
        }

        private void Jump()
        {
            if (_isGrounded)
            {
                _ballAnimation.SetJumpAnimation();
                _rb.velocity = Vector2.zero;
                _rb.AddForce(new Vector2(0, JumpForce * _gravityModifier), ForceMode2D.Impulse);
            }

            _isGrounded = false;
        }

        public void Dispose()
        {
            _compositeDisposable?.Dispose();
        }
    }
}