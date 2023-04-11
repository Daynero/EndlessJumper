using UnityEngine;
using Utils;

public class BallController : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    private Rigidbody2D rb;
    private float gravityModifier = 1f;
    private bool isGrounded = true;
    private float lastClickTime;
    private float doubleClickTime = 0.2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            float timeSinceLastClick = Time.time - lastClickTime;
            
            if (timeSinceLastClick <= doubleClickTime)
            {
                ChangeGravity();
            }
            else 
            {
                Jump();
            }

            lastClickTime = Time.time;
        }
    }

    private void ChangeGravity()
    {
        gravityModifier *= -1;
        rb.gravityScale = gravityModifier * 100;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(StringConstants.PlatformTag))
        {
            isGrounded = true;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.velocity = Vector2.zero; 
            rb.AddForce(new Vector2(0, jumpForce * gravityModifier), ForceMode2D.Impulse); 
        }

        isGrounded = false;
    }
}
