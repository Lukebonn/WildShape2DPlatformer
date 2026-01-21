using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;
    private float destroyTimer;
    private Player playerScript;
    private float currDirection;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnEnable()
    {
        currDirection = playerScript.Direction();
        destroyTimer = 0;
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime * currDirection;
        transform.Translate(movementSpeed, 0, 0);
        destroyTimer += Time.deltaTime;
        if (destroyTimer >= 5) Deactivate();
        if (movementSpeed == 0) Deactivate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        //anim.SetTrigger("Explode");
    }
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction) localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    
    private void Deactivate()
    {
        Debug.Log("PlayerBullet destroyed");
        gameObject.SetActive(false);
    }
}
