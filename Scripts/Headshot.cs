using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headshot : MonoBehaviour
{
    public bool hit;
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "PlayerBullet")
        {
            Rigidbody2D rb = gameObject.AddComponent<Rigidbody2D>();
            transform.parent = null;
            hit = true;
            rb.AddForce(collision.transform.position * -1);
        }
    }
}
