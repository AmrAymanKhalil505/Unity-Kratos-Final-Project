using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossSounds : MonoBehaviour {
    public AudioClip roarClip;
    public AudioClip axeAttackClip;
    public AudioClip rockClip;
    public AudioClip magicClip;
    public AudioClip hornsClip;
    public AudioClip stunnedClip;
    public AudioClip dyingClip;

    public void roar()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(roarClip);
    }

    public void axeAttackSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(axeAttackClip);
    }

    public void rockSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(rockClip);
    }

    public void magicSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(magicClip);
    }

    public void hornsSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(hornsClip);
    }

    public void stunnedSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(stunnedClip);
    }

    public void dyingSound()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(dyingClip);
    }
}
