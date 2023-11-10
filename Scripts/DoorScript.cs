using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public bool open;
    public bool closed = true;
    public Collider2D col;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (open == true)
        {
            transform.localScale = new Vector3(0.5f, 2, 1);
            closed = false;
            col.enabled = false;
        }
        else if (closed == true)
        {
            transform.localScale = new Vector3(1, 2, 1);
            open = false;
            col.enabled = true;
        }
    }
}
