using UnityEngine;

public class AcornBulletScript : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [Header("Speed of the Bullet")]
    [SerializeField, Tooltip("Speed of the Bullet")] public float force; //speed of bullet
    private float timer;
    private Player playerScript;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
        if (force == 0) force = 3f;

        //flies towards player's location at time of fire
        Vector3 direction = player.transform.position - transform.position;
        rb.linearVelocity = new Vector2(direction.x, direction.y).normalized * force;

        //rotating the bullet, so it always faces the player's location (at fire time)
        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    void Update()
    {
        //after 10 seconds will destroy the bullet
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Ground") || other.gameObject.CompareTag("Damage"))
        {
            if (other.gameObject.CompareTag("Player"))
                playerScript.TakeDamage(); //calls the player to take damage before destroyed
            Destroy(gameObject);
        }
    }
}
