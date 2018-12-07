using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDoorScript : MonoBehaviour {
    private Animator anim;
    AudioSource audioSource;
    public AudioClip DoorCloseSound;
    Collider m_Collider;
    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        m_Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kratos")
        {
            audioSource.PlayOneShot(DoorCloseSound, 0.3F);
            anim.Play("DoorClose");
            m_Collider.enabled = false;

            //KILL KRATOS HERE
        }
    }
}
