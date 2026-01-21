using System;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletPos;
    public int distanceToPlayerToFire;
    private GameObject player;
    private float timer; //time between bullets
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (distanceToPlayerToFire == 0) distanceToPlayerToFire = 10;
    }

    void Update()
    {
        timer += Time.deltaTime;

        float distance = Vector2.Distance(transform.position, player.transform.position); //distance between enemy and player

        if (distance < distanceToPlayerToFire) //if player is close enough
        {
            timer += Time.deltaTime; //time between shots
            if (timer > 2)
            {
                timer = 0;
                shoot();
            }
        }
        
    }

    private void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity); //create bullet
    }
}
