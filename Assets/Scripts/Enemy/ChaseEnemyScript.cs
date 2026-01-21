using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ChaseEnemyScript : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float scanDist = 5f;
    private GameObject player;
    public Transform[] points;

    private int i;
    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private Player playerScript;

    private bool hasLineOfSight = false;
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {        
        //will move the enemy from their current location to the player's location depending on the speed
        if (hasLineOfSight)
        {
            StartCoroutine(BlinkRed());
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        //will move between two points
        else
        {
            if (Vector2.Distance(transform.position, points[i].position) < 0.25f)
            {
                i++;
                if (i == points.Length)
                {
                    i = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
        }
        sr.flipX = (transform.position.x - points[i].position.x) < 0f;
    }

    public void FixedUpdate()
    {
        //creates a line between the player and the enemy (stops at first box collider which includes enemy unless their
        //box colliders is set to "ignore raycast") (can also use RaycastAll)
        RaycastHit2D ray = Physics2D.Raycast(transform.position, player.transform.position - transform.position);
        //this next part is pretty much really cool and meant for debugging purposes. it does not hold any client side purpsoe
        if (ray.collider != null)
        {
            float distToPlayer = Vector2.Distance(transform.position, player.transform.position);
            hasLineOfSight = (distToPlayer <= scanDist) && (ray.collider.CompareTag("Player"));
            if (hasLineOfSight)
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.green);
            }
            else
            {
                Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.red);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        playerScript.TakeDamage();
    }
    private IEnumerator BlinkRed()
    {
        sr.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        sr.color = Color.white;
    }
}
