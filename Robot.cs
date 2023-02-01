using UnityEngine;

public class Robot : MonoBehaviour {
    public float horizontalInput;
    public float maxHorizontalSpeed;
    public float jumpForce;

    public bool isGrounded;

    public Transform lineCastStart;
    public Transform lineCastEnd;

    Animator animatorComponent;
    SpriteRenderer spriteRendererComponent;
    Rigidbody2D rigidbody2DComponent;

    bool isFacingRight = true;

    void Awake() {
        // Get references to components
        animatorComponent = GetComponent<Animator>();
        spriteRendererComponent = GetComponent<SpriteRenderer>();
        rigidbody2DComponent = GetComponent<Rigidbody2D>();

        isFacingRight = true;
    }


    // Update is called once per frame
    void Update() {


        Debug.DrawLine(lineCastStart.position, lineCastEnd.position, Color.green);

        // Check if grounded
        int groundLayerMask = LayerMask.GetMask("Ground");
        isGrounded = (Physics2D.Linecast(lineCastStart.position, lineCastEnd.position, groundLayerMask)) ? true : false;


        // Input
        horizontalInput = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && isGrounded) {
            // it's okay to add Impulse type force in Update().  this is an exception to the rule
            rigidbody2DComponent.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }


        // Flip sprite
        if (horizontalInput < 0 && isFacingRight) {
            spriteRendererComponent.flipX = true;
            isFacingRight = false;
        } else if (horizontalInput > 0 && !isFacingRight) {
            spriteRendererComponent.flipX = false;
            isFacingRight = true;
        }

        // Set Animator variables
        animatorComponent.SetFloat("horizontalSpeed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        animatorComponent.SetBool("isGrounded", isGrounded);

    }

    void FixedUpdate() {

        // Move the player horizontally based on input
        // NOTE THAT THIS IS A HACK.   Best practice when moving an object with the physics simulation is to use AddForce,
        // but controlling the velocity of the player controller would require a lot more code.
        // this scene is really about demonstrating how to use the Animator, not how to make a good player controller.
        rigidbody2DComponent.velocity = new Vector2(horizontalInput * maxHorizontalSpeed, rigidbody2DComponent.velocity.y);
    }
}