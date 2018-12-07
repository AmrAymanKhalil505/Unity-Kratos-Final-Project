using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//MUST INSERT KILL LOGIC ON KRATOS WHEN ROCK FALLS ON HIM.
public class FallingCoulderScript : MonoBehaviour {
    public SphereCollider killCollider;
    public BoxCollider FallCollider;
    private Animator anim;
    private int  counter;
    AudioSource audioData;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
        counter = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kratos" && counter==0)
        {
            audioData.Play(0);
            counter++;
            anim.SetTrigger("RockFall");
            Invoke("blockRock", 1);//this will happen after 2 seconds

        }
        if(other.gameObject.tag == "Kratos" && counter == 1)
        {
            //KILL KRATOS HERE 
        }
    }
    void blockRock()
    {
        killCollider.isTrigger = false; //to make tock impassable and no longer kill you
    }


}
