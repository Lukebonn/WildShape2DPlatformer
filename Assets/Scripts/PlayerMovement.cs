using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private float moveInput = 0f;
    private bool jumpPressed = false;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //moveInput = 0f;

        //if (Keyboard.current != null)
        //{
        //    if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        //    {
        //        moveInput = -1f;
        //    }
        //    if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        //    {
        //        moveInput = 1f;
        //    }
        //}
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void FixedUpdate()
    {
        //checks whether or not the player has hit the ground to stop things like double jump
        //NOTE: Transform groundCheck is placed basically at the player's feet as a child of "player"
        //this works be creating a circle that will check in a $groundCheckRadius (0.2f) whether or not at groundLayer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }
}
