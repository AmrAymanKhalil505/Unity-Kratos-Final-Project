using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPSystem : MonoBehaviour {

	private GameObject Kratos;

	// Use this for initialization
	void Start () {
		Kratos = GameObject.FindGameObjectWithTag("Kratos");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void upSpeed(){
		if(Kratos.GetComponent<PlayerController>().SkillPoints >= 1){
			Kratos.GetComponent<PlayerController>().SkillPoints -= 1;
			Kratos.GetComponent<PlayerController>().currentSpeed = Kratos.GetComponent<PlayerController>().currentSpeed * 0.1f + Kratos.GetComponent<PlayerController>().currentSpeed;
		}
	}

	public void upHealth(){
		if(Kratos.GetComponent<PlayerController>().SkillPoints >= 1){
			Kratos.GetComponent<PlayerController>().SkillPoints -= 1;
			Kratos.GetComponent<PlayerController>().maxHealth = Kratos.GetComponent<PlayerController>().maxHealth * 0.1f + Kratos.GetComponent<PlayerController>().maxHealth;
		}
	}

	public void upStrength(){
		if(Kratos.GetComponent<PlayerController>().SkillPoints >= 1){
			Kratos.GetComponent<PlayerController>().SkillPoints -= 1;
			Kratos.GetComponent<PlayerController>().lightAttackDamage = Kratos.GetComponent<PlayerController>().lightAttackDamage * 0.1f + Kratos.GetComponent<PlayerController>().lightAttackDamage;
			Kratos.GetComponent<PlayerController>().heavyAttackDamage = Kratos.GetComponent<PlayerController>().heavyAttackDamage * 0.1f + Kratos.GetComponent<PlayerController>().heavyAttackDamage;
		}
	}
}
