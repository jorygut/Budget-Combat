using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float shootSpeed;

    Collider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = gameObject.transform.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * Time.deltaTime * shootSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != "Enemy")
        {
            Destroy(gameObject);
            collider.enabled = false;
        }
        else
        {
            collider.enabled = true;
        }
        if (collision.transform.tag == "Player")
        {
            PlayerController playerScript = collision.transform.GetComponent<PlayerController>();
            playerScript.health -= 1;
        }
    }
}