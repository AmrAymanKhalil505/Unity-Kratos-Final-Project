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
