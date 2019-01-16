using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KratosPartOfCode : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		checkForEnemy();
	}

	void checkForEnemy(){
		Collider[] hitColliders =Physics.OverlapSphere(transform.position, 10);
		for(int i=0;i<hitColliders.Length;i++){
			//print(hitColliders[i].gameObject.tag);
			if(hitColliders[i].gameObject.tag =="LightEnemy"||hitColliders[i].gameObject.tag =="HeavyEnemy" ||hitColliders[i].gameObject.tag =="Ranged" ){
				hitColliders[i].gameObject.GetComponent<LightEnemyBehaviour>().notifyKratosApproch();
			}
		}
		
	}
}
