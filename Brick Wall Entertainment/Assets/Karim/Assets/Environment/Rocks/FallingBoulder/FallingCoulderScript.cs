using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BrickWallEntertainment.Managers;

//MUST INSERT KILL LOGIC ON KRATOS WHEN ROCK FALLS ON HIM.
public class FallingCoulderScript : MonoBehaviour
{
    public SphereCollider killCollider;
    public BoxCollider FallCollider;
    private Animator animator;
    private int counter;

    void Start()
    {
        animator = GetComponent<Animator>();
        counter = 0;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag.Equals("Kratos") && counter == 0)
        {
            AudioManager.Instance.Play("FallingBoulder");
            counter++;
            animator.SetTrigger("RockFall");
            Invoke("blockRock", 1);//this will happen after 2 seconds

        }
        if (other.gameObject.tag == "Kratos" && counter == 1)
        {
            //KILL KRATOS HERE 
        }
    }
    void blockRock()
    {
        killCollider.isTrigger = false; //to make rock impassable and no longer kill you
    }
}
