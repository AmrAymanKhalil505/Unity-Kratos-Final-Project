using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrickWallEntertainment.Managers;

public class SpikeTrapScript : MonoBehaviour {
    private Animator anim;
    AudioSource audioSource;
    public AudioClip SpikeTriggeredSound;
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
        if (other.gameObject.tag == "Kratos")
        {
            AudioManager.Instance.Play("SpikeTrap_Sound");
            //anim.SetTrigger("SpikeTrigger");
            anim.Play("TriggeredSpikes");
        }
    }
}
