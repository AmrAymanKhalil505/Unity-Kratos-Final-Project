using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthChestScript : MonoBehaviour {
    private Animator anim;
    public AudioClip OpeningChestSound;
    public AudioSource audioSource;
    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kratos" ) //&&Kratos Presses E?
        {
            anim.SetTrigger("OpenHealthChest");
            audioSource.PlayOneShot(OpeningChestSound, 0.1F);
            //HEAL KRATOS HERE
        }
    }
}
