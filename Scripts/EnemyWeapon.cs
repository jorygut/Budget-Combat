using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    public float shootRadius;
    public float bulletForce;
    public float timeBetweenShots;
    public float cooldown;
    public bool spotted;
    public string equipped;

    public LayerMask playerMask;

    public GameObject player;
    public GameObject enemy;
    public GameObject bullet;
    public Transform firePoint;

    // Start is called before the first frame update
    void Start()
    {
        if (enemy.transform.tag == "Enemy")
        {
            cooldown = 2;
            shootRadius = 5;
            bulletForce = 50;
        }
        else if(enemy.transform.tag == "FastEnemy")
        {
            cooldown = 1;
            shootRadius = 10;
            bulletForce = 50;
        }
        else if (enemy.transform.tag == "BigEnemy")
        {
            cooldown = 5;
            shootRadius = 10;
            bulletForce = 75;
        }
        else if (enemy.transform.tag == "KnifeEnemy")
        {
            cooldown = 1;
            shootRadius = 20;
            bulletForce = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = enemy.transform.position;
        if (Physics2D.OverlapCircle(transform.position, shootRadius, playerMask))
        {
            transform.LookAt(player.transform.position);
            Attack();
            AttackCooldown();
            spotted = true;
        }
    }

    void Attack()
    {
        EnemyScript enemyScript = enemy.GetComponent<EnemyScript>();
        if ((timeBetweenShots >= cooldown) && enemyScript.dead == false)
        {
            Vector2 newFirePoint = new Vector2(firePoint.position.x, firePoint.position.y);
            GameObject bulletClone = Instantiate(bullet, newFirePoint, firePoint.rotation);
            Rigidbody2D bulletRb = bulletClone.GetComponent<Rigidbody2D>();
            bulletClone.transform.Rotate(0, -90, 0);
            bulletRb.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);
            timeBetweenShots = 0;
        }
    }

    private void AttackCooldown()
    {
        timeBetweenShots += 1 * Time.deltaTime;
    }
}
