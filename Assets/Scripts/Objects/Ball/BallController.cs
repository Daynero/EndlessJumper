using System;
using Core.GameTime;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utils;
using Zenject;

public class BallController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;

    private Rigidbody2D _rb;
    private float _gravityModifier = 1f;
    private bool _isGrounded = true;
    private float _lastClickTime;
    private const float DoubleClickTime = 0.2f;

    public event Action OnGameEnded;

    [Inject]
    public void Construct(GameTime gameTime)
    {
        gameTime.Pause.Subscribe(StopOrMovePlatforms);
    }

    private void StopOrMovePlatforms(bool isStop)
    {
        if (isStop)
        {
            _rb.velocity = Vector2.zero;
        }

        _rb.bodyType = isStop ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic;
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
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

        _lastClickTime = Time.time;
    }

    public void SetBallDynamic(bool isDynamic)
    {
        _rb.bodyType = isDynamic ? RigidbodyType2D.Dynamic : RigidbodyType2D.Static;
    }

    private void ChangeGravity()
    {
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
                OnGameEnded?.Invoke();
                break;
        }
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _rb.velocity = Vector2.zero;
            _rb.AddForce(new Vector2(0, jumpForce * _gravityModifier), ForceMode2D.Impulse);
        }

        _isGrounded = false;
    }
}