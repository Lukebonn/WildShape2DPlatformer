using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    public Image healthImage;
    public int coins;

    public int extraJumpsValue = 0;
    private int extraJumps;
    private float moveInput;
    private float direction = 1;

    private bool isGrounded;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Animator animator;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if (sr == null) throw new Exception("no SpriteRenderer component found");
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) throw new Exception("no rigidbody2D component found");
        animator = GetComponent<Animator>();
        if (animator == null) throw new Exception("no Animator component found");

        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            }
            else if (extraJumps > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                extraJumps--;
                animator.SetTrigger("doubleJump");
            }
        }
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }
        if (Input.GetKeyDown(KeyCode.Q)) {
            animator.SetBool("isPlaying", true);
            AudioManager.Instance?.PlayerBanjo();
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            animator.SetBool("isPlaying", false);
            AudioManager.Instance?.PlayerBanjoStop();
        }

        //set animation triggers that appear in the Player's Animator Controller (need to be assigned on the other side as well)
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("YVelocity", rb.linearVelocityY);
        // flipping which side the run animation faces
        if (moveInput > 0)
        {
            sr.flipX = false; // facing right
            direction = 1;
        }
        else if (moveInput < 0)
        {
            sr.flipX = true;  // facing left
            direction = -1;
        }

        healthImage.fillAmount = health / 100f;
        if (direction == 0) Debug.Log("direction = 0");
    }

    private void FixedUpdate()
    {
        //checks whether or not the player has hit the ground to stop things like double jump
        //NOTE: Transform groundCheck is placed basically at the player's feet as a child of "player"
        //this works be creating a circle that will check in a $groundCheckRadius (0.2f) whether or not at groundLayer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            TakeDamage();
        }
    }
    private IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }

    private void Die()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
    public void TakeDamage() //can now be called by other scripts (enemy scripts mostly)
    {
        health -= 25;
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        StartCoroutine(BlinkRed());

        if (health <= 0)
        {
            Die();
        }
    }
    public bool CanAttack()
    {
        return moveInput == 0 && isGrounded;
    }
    public float Direction()
    {
        return direction;
    }
}
