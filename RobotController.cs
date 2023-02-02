using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator)),
RequireComponent(typeof(SpriteRenderer)),
RequireComponent(typeof(Rigidbody2D))]
public class RobotController : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D body;

    public float horizontalSpeed;
    public float jumpForce;

    public Transform lineCastStart;
    public Transform lineCastEnd;

    private float horizontalInput;

    private bool isFacingRight = true;
    private bool isGrounded = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.DrawLine(lineCastStart.position, lineCastEnd.position, Color.white);

        int groundLayerMask = LayerMask.GetMask("Ground");
        isGrounded = Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayerMask) ? true : false;

        horizontalInput = Input.GetAxis("Horizontal");

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (horizontalInput < 0 && isFacingRight)
        {
            spriteRenderer.flipX = true;
            isFacingRight = false;
        }
        else if (horizontalInput > 0 && !isFacingRight)
        {
            spriteRenderer.flipX = false;
            isFacingRight = true;
        }

        animator.SetFloat("Speed", Mathf.Abs(body.velocity.x)); //"Speed" parameter is capital sensitve so be careful >~<
        animator.SetFloat("VerticalSpeed", body.velocity.y);
        animator.SetBool("IsGrounded", isGrounded = isGrounded);
    }

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalInput * horizontalSpeed, body.velocity.y);
    }
}
