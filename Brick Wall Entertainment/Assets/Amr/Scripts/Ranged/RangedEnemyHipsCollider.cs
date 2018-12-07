using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyHipsCollider : MonoBehaviour {

	public GameObject RangedEnemyParent;
	public string TagAxeKratos;
	RangedBehaviour LEB;
	// Use this for initialization
	void Start () {
		LEB= RangedEnemyParent.GetComponent<RangedBehaviour>();
	}
	
	void OnTriggerEnter(Collider other)
    {	
		if(TagAxeKratos == other.gameObject.tag){
			LEB.damage(10);
		}
    }
}
