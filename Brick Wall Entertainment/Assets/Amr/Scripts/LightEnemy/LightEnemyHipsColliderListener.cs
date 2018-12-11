using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightEnemyHipsColliderListener : MonoBehaviour {
	public GameObject LightEnemyParent;
	public string TagAxeKratos;
	LightEnemyBehaviour LEB;
	int Damage =10;
	// Use this for initialization
	void Start () {
		LEB= LightEnemyParent.GetComponent<LightEnemyBehaviour>();
	}
	
	void OnTriggerEnter(Collider other)
    {	
		if(TagAxeKratos == other.gameObject.tag){
			LEB.damage(Damage);
		}
    }
}
