using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //Variables
    public float speed;
    public float shootDistance;
    public float chaseTime;
    public float chaseDistance = 5;
    public float health;
    public float flashTime;
    public bool dead;
    public bool flashed;

    //Game Objects
    public LayerMask wall;
    public GameObject player;
    public GameObject weapon;
    public GameObject playerWeapon;
    public GameObject bodyBlood;
    public GameObject headBlood;
    public GameObject head;

    //Animation
    public ParticleSystem blood;

    //Collider
    Collider2D collider;

    //RigidBodies
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        bodyBlood.SetActive(false);
        headBlood.SetActive(false);

        if (gameObject.transform.tag == "Enemy")
        {
            speed = 1;
            health = 3;
            shootDistance = 10;
        }
        else if (gameObject.transform.tag == "FastEnemy")
        {
            speed = 5;
            health = 1;
            shootDistance = 20;
        }
        else if (gameObject.transform.tag == "BigEnemy")
        {
            speed = 0.5f;
            health = 10;
            shootDistance = 5;
        }
        if (gameObject.transform.tag == "KnifeEnemy")
        {
            speed = 5;
            health = 3;
            shootDistance = 0;
            chaseDistance = -1;
        }

        if (flashed == true)
        {
            Flashed();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Headshot headScript = head.GetComponent<Headshot>();
        EnemyWeapon enemyWeaponScript = weapon.transform.GetComponent<EnemyWeapon>();
        PlayerController playerScript = player.GetComponent<PlayerController>();
        //Death
        if(headScript.hit == true)
        {
            health = 0;
            headBlood.SetActive(true);
            blood.Play();

        }
        if(health <= 0)
        {
            blood.Play();
            transform.rotation = Quaternion.Euler(0, 0, 90f);
            dead = true;
            collider.enabled = false;
            rb.gravityScale = 0;
        }

        if (enemyWeaponScript.spotted == true)
        {
            SpotCooldown();
            if (Vector2.Distance(transform.position, player.transform.position) > shootDistance && chaseTime <= 25)
            {
                if (player.transform.position.x - transform.position.x < 0)
                {
                    transform.Translate(transform.right * - 1 * speed * Time.deltaTime);
                }
                else if (player.transform.position.x - transform.position.x > 0)
                {
                    transform.Translate(transform.right * speed * Time.deltaTime);
                }
            }
        }
        if (Vector2.Distance(transform.position, player.transform.position) < chaseDistance)
        {
            speed = 0;
        }
        else
        {
            speed = 5;
        }
    }

    void SpotCooldown()
    {
        chaseTime += Time.deltaTime;
        if (chaseTime > 25)
        {
            EnemyWeapon enemyWeaponScript = weapon.transform.GetComponent<EnemyWeapon>();
            enemyWeaponScript.spotted = false;
            chaseTime = 0;
        }
    }

    void Flashed()
    {
        EnemyWeapon enemyWeapon = weapon.GetComponent<EnemyWeapon>();
        speed = 0;
        enemyWeapon.shootRadius = 0;
        flashTime += Time.deltaTime;

        if (flashTime >= 8)
        {
            speed = 5;
            enemyWeapon.shootRadius = 5;
            flashed = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlayerBullet")
        {
            PlayerCombat combatScript = playerWeapon.transform.GetComponent<PlayerCombat>();
            health -= combatScript.damage;
            bodyBlood.SetActive(true);
        }

    }

}