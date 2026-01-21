using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform bulletOrigin;
    [SerializeField] private GameObject[] bullets;
    private Animator anim;
    private Player playerScript;
    private float cooldownTimer = Mathf.Infinity;
    private float bulletOriginX;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerScript = GetComponent<Player>();
        bulletOriginX = bulletOrigin.localPosition.x;
    }

    private void Update()
    {
        bulletOrigin.localPosition = new Vector3(bulletOriginX * playerScript.Direction(), bulletOrigin.localPosition.y, bulletOrigin.localPosition.z);
        if (Input.GetKeyDown(KeyCode.E) && cooldownTimer > attackCooldown && playerScript.CanAttack())
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //anim.SetTrigger("Attack");
        cooldownTimer = 0;

        bullets[FindBullet()].transform.position = bulletOrigin.position;
        bullets[FindBullet()].GetComponent<PlayerBulletScript>().SetDirection(playerScript.Direction());
    }

    private int FindBullet()
    {
        for (int i = 0; i < bullets.Length; i++)
        {
            if (!bullets[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
