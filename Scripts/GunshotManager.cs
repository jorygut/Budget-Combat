using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunshotManager : MonoBehaviour
{
    public AudioSource pistol;
    public AudioSource ak47;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PistolSound()
    {
        pistol.Play();
        Debug.Log("Audio played");
        if (pistol.clip.length > 1)
        {
            pistol.Stop();
            Debug.Log("Audio Stop");
        }
    }

    public void Ak47Sound()
    {
        ak47.Play();
        if (ak47.clip.length > 1)
        {
            pistol.Stop();
        }
    }
}
