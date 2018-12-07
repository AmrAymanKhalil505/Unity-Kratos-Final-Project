using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpenCloseScript : MonoBehaviour {
    //MUST CALL ALLEnemiesDefeated() WHEN ALL THE ENEMIES ON THIS LEVEL ARE DEAD SO THE GATE OPENS!!!
    private Animator anim;
    private Transform gateTransform;
    public BoxCollider collider1;
    // Use this for initialization
    void Start () {

        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Kratos")
        {
            anim.SetTrigger("CloseGate");
            collider1.enabled = true;


        }
    }

    void AllEnemiesDefeeated() 
    {
        anim.SetTrigger("OpenGate");
        collider1.enabled = false;
    }
}
