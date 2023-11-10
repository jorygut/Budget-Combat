using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float explodeTime;
    public float radius;
    public float force = 100;
    public bool exploded;
    public float flashTime;
    public bool grenade;
    public bool flash;
    public bool knife;
    public float damage = 5;

    public GameObject player;
    public GameObject grenadeSprite;
    public GameObject flashbangSprite;
    public GameObject knifeSprite;
    public ParticleSystem explosionParticles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
        if (grenade == true)
        {
            
            Timer();
            grenadeSprite.SetActive(true);
            flashbangSprite.SetActive(false);
            knifeSprite.SetActive(false);
        }
        else if (flash == true)
        {
            Timer();
            grenadeSprite.SetActive(false);
            flashbangSprite.SetActive(true);
            knifeSprite.SetActive(false);
        }
        else if (knife == true)
        {
            grenadeSprite.SetActive(false);
            flashbangSprite.SetActive(false);
            knifeSprite.SetActive(true);
        }
        
    }
    void Timer()
    {
        explodeTime += 1 * Time.deltaTime;
        if (explodeTime >= 3 && grenade == true)
        {
            GrenadeF();
            explosionParticles.Play();
        }
        else if (explodeTime >= 3 && flash == true)
        {
            FlashbangF();

        }
    }
    void GrenadeF()
    {
        if (exploded == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D i in colliders)
            {
                Rigidbody2D rb = i.transform.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.AddForce(Vector2.right * force, ForceMode2D.Impulse);
                }
                if (i.transform.tag == "Enemy")
                {
                    EnemyScript enemyScript = i.transform.GetComponent<EnemyScript>();
                    enemyScript.health -= damage;
                }
            }
            exploded = true;
        }
    }
    void FlashbangF()
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();
        if (explodeTime >= 5 && exploded == false)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
            foreach (Collider2D i in colliders)
            {
                if (i.transform.tag == "Enemy")
                {
                    EnemyScript enemyScript = i.transform.GetComponent<EnemyScript>();
                    enemyScript.flashed = true;
                }
            }
            exploded = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerCombat playerCombat = player.GetComponent<PlayerCombat>();

        if (playerCombat.equipped == "ThrowingKnife" && collision.transform.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.transform.GetComponent<EnemyScript>();
            enemyScript.health -= playerCombat.damage;
        }
    }
}
