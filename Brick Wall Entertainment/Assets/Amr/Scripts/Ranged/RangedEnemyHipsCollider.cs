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
				if(GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().RageMode){
					LEB.damage(2*(int)GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().lightAttackDamage);
				}
				else{
					LEB.damage((int)GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().lightAttackDamage);
				}
			}
			else{
				if(GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().RageMode){
					LEB.damage(2*(int)GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().heavyAttackDamage);
				}
				else{
					LEB.damage((int)GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().heavyAttackDamage);
				}
			}
			if(!GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().RageMode){
				GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().currentRage += 5;
			}
		}
    }
}
