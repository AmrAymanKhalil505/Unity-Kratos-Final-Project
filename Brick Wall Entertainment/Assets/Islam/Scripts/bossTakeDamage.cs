using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossTakeDamage : MonoBehaviour {
    public GameObject axe;
    public GameObject magicBall;
    public GameObject leftArm;
    public GameObject rightArm;
    public ParticleSystem rightBlood;
    public ParticleSystem leftBlood;
    Animator anim;
    public int leftArmHealth;
    public int rightArmHealth;
    public int legsHealth;
    public float health;
    public float maxHealth;

	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
        leftArmHealth = 3;
        rightArmHealth = 3;
        legsHealth = 3;
        maxHealth = 100;
        health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(leftArmHealth + " " + rightArmHealth + " " + legsHealth);
        if (health <= 0)
        {
            anim.SetBool("Dying", true);

            axe.GetComponent<Rigidbody>().isKinematic = false;
            axe.GetComponent<Collider>().isTrigger = false;
            axe.transform.parent = null;

            magicBall.GetComponent<Rigidbody>().isKinematic = false;
            magicBall.GetComponent<Collider>().isTrigger = false;
            magicBall.transform.parent = null;
        }
    }

    public void takeRightArmDamage()
    {
        if (anim.GetBool("Roared"))
        {
            rightArmHealth--;
            if (rightArmHealth == 0)
            {
                anim.SetBool("RightArmInjured", true);
                axe.GetComponent<Rigidbody>().isKinematic = false;
                axe.GetComponent<Collider>().isTrigger = false;
                axe.transform.parent = null;
                rightArm.transform.localScale = new Vector3(0, 0, 0);
                rightBlood.Play();
                health -= .2f * health;
                anim.SetBool("Stunned", true);
            }
        }
    }

    public void takeLeftArmDamage()
    {
        if (anim.GetBool("Roared"))
        {
            leftArmHealth--;
            if (leftArmHealth == 0)
            {
                anim.SetBool("LeftArmInjured", true);
                magicBall.GetComponent<Rigidbody>().isKinematic = false;
                magicBall.GetComponent<Collider>().isTrigger = false;
                magicBall.transform.parent = null;
                leftArm.transform.localScale = new Vector3(0, 0, 0);
                leftBlood.Play();
                health -= .2f * maxHealth;
                anim.SetBool("Stunned", true);
            }
        }
    }

    public void takeLegsDamage()
    {
        if (anim.GetBool("Roared"))
        {
            legsHealth--;
            if (legsHealth == 0)
            {
                anim.SetBool("LegsInjured", true);
                health -= .2f * maxHealth;
                anim.SetBool("Stunned", true);
            }
            if (legsHealth < 0)
            {
                // health -= .05f * maxHealth;
            }
        }
    }

    public void takeOtherDamage()
    {
        if (anim.GetBool("Roared"))
        {
            health -= .05f * maxHealth;
        }
    }
}
