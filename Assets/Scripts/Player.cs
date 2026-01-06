using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private bool isGrounded;
    private Rigidbody2D rb;

    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    void Update()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }

        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGrounded", isGrounded);
        animator.SetFloat("YVelocity", rb.linearVelocityY);

        SetAnimation(moveInput);
    }

    private void FixedUpdate()
    {
        //checks whether or not the player has hit the ground to stop things like double jump
        //NOTE: Transform groundCheck is placed basically at the player's feet as a child of "player"
        //this works be creating a circle that will check in a $groundCheckRadius (0.2f) whether or not at groundLayer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void SetAnimation(float moveInput)
    {
        if (isGrounded)
        {
            if (moveInput == 0)
            {
                animator.Play("Player_Idle");
                Debug.Log("Idle animation");
            }
            else
            {
                animator.Play("Player_Run");
                Debug.Log("run animation");
            }
        }
        else
        {
            if (rb.linearVelocityY > 0)
            {
                animator.Play("Player_Jump_Up");
                Debug.Log("jumpup animation");
            }
            else
            {
                animator.Play("Player_Jump_Fall");
                Debug.Log("jumpfall animation");
            }
        }
    }
}
