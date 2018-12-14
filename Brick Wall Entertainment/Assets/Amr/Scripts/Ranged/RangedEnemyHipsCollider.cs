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
			if(GameObject.FindGameObjectWithTag("Kratos").GetComponent<Animator>().GetBool("LightAttack")){
				LEB.damage(10);
			}
			else{
				LEB.damage(30);
			}
			if(!GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().RageMode){
				GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().currentRage += 5;
			}
		}
    }
}
