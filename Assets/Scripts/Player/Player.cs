using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int health = 100;
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Image healthImage;
    public int coins;

    public int extraJumpsValue = 0;
    private int extraJumps;
    private float moveInput;
    private float direction = 1;

    private bool isGrounded;
    private bool facingRight;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Check")]
    public Transform wallCheckPos;
    public Vector2 wallCheckSize = new Vector2(0.49f, 0.03f);
    public LayerMask wallLayer;

    [Header("Wall Speed")]
    public float wallSlideSpeed = 2f;
    public bool onWall = false;
    public bool isWallSliding; //turn private

    [Header("Wall Jumping")]
    public bool isWallJumping; //turn private
    private float wallJumpDirection = 1;
    public float wallJumpTime = 0.5f; //turn private
    public float wallJumpTimer; //turn private
    private Vector2 wallJumpPower = new Vector2(5f, 10f);
    private float wallContactTime = 0f;

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
// ------------------------------------------------- Update ----------------------------------------------------------------

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (!isWallSliding && !isWallJumping)
        {
            rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
        }

        ProcessWallSlide();
        Jump();

        if (isGrounded || onWall)
        {
            extraJumps = extraJumpsValue;
        }
        //play banjo
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

        if (!isWallSliding)
        {
            if (isWallJumping) Invoke(nameof(Flip), 0.2f);
            else Flip();
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
        WallCheck();
        ProcessWallJump();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            TakeDamage();
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(wallCheckPos.position, wallCheckSize);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

    private IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sr.color = Color.white;
    }
    // ------------------------------------- Called Functions ------------------------------------------------------------

    private void Jump() //mean to differenciate between an off-wall jump and an off-ground jump
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!onWall || Mathf.Abs(moveInput) == 1 || isGrounded)
            {
                if (isGrounded)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                }

                if (extraJumps > 0)
                {
                    rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
                    extraJumps--;
                    animator.SetTrigger("doubleJump");
                }
            }
            else
            {
                //if (wallJumpTimer > 0f)
                if (true)
                {
                    isWallJumping = true;
                    //ProcessWallJump();
                    wallJumpDirection = -transform.localScale.x;
                    rb.linearVelocity = new Vector2(wallJumpDirection * wallJumpPower.x, wallJumpPower.y); //Jump away from wall
                    //wallJumpTimer = 0;
                    

                    //force flip
                    if (transform.localScale.x != wallJumpDirection)
                    {
                        facingRight = !facingRight;
                        Vector3 ls = transform.localScale;
                        ls.x *= -1f;
                        transform.localScale = ls;
                    }
                    //isWallJumping = false;

                    Invoke(nameof(CancelWallJump), 0.5f);
                }
            }
        
        }
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
    private void Flip()
    {
        if (moveInput > 0 && facingRight || !facingRight && moveInput < 0)
        {
            facingRight = !facingRight;
            Vector3 ls = transform.localScale;
            ls.x *= -1f;
            transform.localScale = ls;
        }
    }
    private void WallCheck()
    {
        onWall = Physics2D.OverlapBox(wallCheckPos.position, wallCheckSize, 0f, wallLayer) != null;
    }
    private void ProcessWallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpDirection = -transform.localScale.x;
            //wallJumpTimer = wallJumpTime;

            //CancelInvoke(nameof(CancelWallJump));
        }
        //else if (wallJumpTimer > 0f)
        //{
        //    wallJumpTimer -= Time.deltaTime;
        //}
    }
    private void CancelWallJump()
    {
        isWallJumping = false;
    }
    private void ProcessWallSlide()
    {
        WallCheck();
        if (!isGrounded && onWall && !isWallJumping) //&& moveInput != 0
        {
            wallContactTime += Time.deltaTime;
            if (wallContactTime < 0.2f) return; //will wait 0.2f before starting slide (player will grip wall for 0.2f)

            isWallSliding = true;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Max(rb.linearVelocity.y, -wallSlideSpeed)); //caps fall rate
            wallContactTime = 0f;
        }
        else
        {
            wallContactTime = 0f;
            isWallSliding = false;
        }
    }
}
