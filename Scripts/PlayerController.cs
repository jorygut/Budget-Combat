using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    //Variables
    public float speed;
    public float hInput;
    public float jumpPower;
    public bool facingRight;
    public float health;
    public float deathCooldown;
    public float kickCooldown;
    public float kickForce = 100;

    //Cam
    public Camera cam;

    //GameObjects
    public GameObject leg;

    //Layers
    public LayerMask ground;

    //RigidBodies
    public Transform groundCheck;
    public Rigidbody2D rb;

    //Animation
    public ParticleSystem blood;
    // Start is called before the first frame update
    void Start()
    {
        leg.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //Health
        if (health <= 0 || transform.position.y < -100)
        {
            speed = 0;
            blood.Play();
            transform.rotation = Quaternion.Euler(0f, 0f, 90f);
            deathCooldown += Time.deltaTime;

            if (deathCooldown >= 3)
            {
                SceneManager.LoadScene("Lose");
            }
        }
        //Movement
        hInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(hInput * speed, rb.velocity.y);
        Flip();
        //Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            Debug.Log("Jump");
        }
        //Doors
        if (Input.GetKeyDown(KeyCode.E))
        {
            DoorOpen();
            leg.SetActive(true);
            kickCooldown = 0;
        }
        kickCooldown += Time.deltaTime;
        if (kickCooldown >= 0.5f)
        {
            leg.SetActive(false);
        }

        //Sprint
        Sprint();
    }

    private void Flip()
    {
        if (facingRight && hInput < 0f || !facingRight && hInput > 0f)
        {
            facingRight = !facingRight;
            Vector2 localScale = transform.localScale;
            localScale.x *= -1;
            transform.localScale = localScale;
        }
    }
    public void DoorOpen()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 2);
        foreach (Collider2D collider in colliders)
        {
            if (collider.transform.tag == "Door")
            {
                DoorScript doorScript = collider.transform.GetComponent<DoorScript>();
                if (doorScript.open == true)
                {
                    doorScript.closed = true;
                }
                else
                {
                    doorScript.open = true;
                }
            }
            else if (collider.transform.tag == "Enemy" || collider.transform.tag == "BigEnemy")
            {
                Rigidbody2D enemyRb = collider.transform.GetComponent<Rigidbody2D>();
                enemyRb.AddForce(transform.position * kickForce);
            }
        }

    }
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, ground);
    }

    public void Sprint()
    {
        float prevSpeed = speed;
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = speed * 1.5f;
        }
        else
        {
            speed = prevSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "EnemyBullet")
        {
            health -= 1;
        }
        if (collision.transform.tag == "KnifeEnemy")
        {
            KnifeEnemyScript knifeScript = collision.transform.GetComponent<KnifeEnemyScript>();
            if(knifeScript.health > 0)
            {
                health = 0;
            }
        }
        if (collision.transform.tag == "EndPoint")
        {
            SceneManager.LoadScene("Win");
        }
    }
}
