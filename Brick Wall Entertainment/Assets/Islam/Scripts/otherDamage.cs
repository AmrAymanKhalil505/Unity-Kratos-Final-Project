using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class otherDamage : MonoBehaviour {
    public GameObject target;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KratosAxe"))
            target.GetComponent<bossTakeDamage>().takeOtherDamage();
    }
}
