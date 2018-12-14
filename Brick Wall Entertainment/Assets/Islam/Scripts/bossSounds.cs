using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BrickWallEntertainment.Managers;

public class bossSounds : MonoBehaviour {
    // public AudioClip roarClip;
    // public AudioClip axeAttackClip;
    // public AudioClip rockClip;
    // public AudioClip magicClip;
    // public AudioClip hornsClip;
    // public AudioClip stunnedClip;
    // public AudioClip dyingClip;

    public void roar()
    {
        AudioManager.Instance.Play("Boss_Roar");
    }

    public void axeAttackSound()
    {
        AudioManager.Instance.Play("Boss_axeAttack");
    }

    public void rockSound()
    {
        AudioManager.Instance.Play("Boss_Rock");
    }

    public void magicSound()
    {
        AudioManager.Instance.Play("Boss_Magic");
    }

    public void hornsSound()
    {
        AudioManager.Instance.Play("Boss_Roar");
    }

    public void stunnedSound()
    {
        AudioManager.Instance.Play("Boss_horns");
    }

    public void dyingSound()
    {
        AudioManager.Instance.Play("BossDying");
    }
}
