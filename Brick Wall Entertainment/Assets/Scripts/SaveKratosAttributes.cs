using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SaveKratosAttributes : MonoBehaviour {

	private PlayerController Kratos;
	public float maxHealth;

	public float currentHealth;
	public int currentXP ;

	public int requiredXP;
	public int level;

	public int SkillPoints;

	public int currentRage;

	public float lightAttackDamage;

	public float heavyAttackDamage;

	public float currentSpeed;

	private bool KratosValuesChanged = false;

	// Use this for initialization
	void Start () {
		GameObject[] objs = GameObject.FindGameObjectsWithTag("KratosAttributes");
		if(objs.Length > 1){
			Destroy(objs[0].gameObject);
		}
		DontDestroyOnLoad(this.gameObject);
		Kratos = GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.GetActiveScene().name == "Normal Level"){
			maxHealth = Kratos.maxHealth;
			currentHealth = Kratos.currentHealth;
			currentXP = Kratos.currentXP;
			requiredXP = Kratos.requiredXP;
			level = Kratos.level;
			SkillPoints = Kratos.SkillPoints;
			currentRage = Kratos.currentRage;
			lightAttackDamage = Kratos.lightAttackDamage;
			heavyAttackDamage = Kratos.heavyAttackDamage;
			currentSpeed = Kratos.currentSpeed;
		}	
		
		if(SceneManager.GetActiveScene().name == "Boss Level" && !KratosValuesChanged){
			Kratos = GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>();
			Kratos.maxHealth = maxHealth;
			Kratos.currentHealth = currentHealth;
			Kratos.currentXP = currentXP;
			Kratos.requiredXP = requiredXP;
			Kratos.level = level;
			Kratos.SkillPoints = SkillPoints;
			Kratos.currentRage = currentRage;
			Kratos.lightAttackDamage = lightAttackDamage;
			Kratos.heavyAttackDamage = heavyAttackDamage;
			Kratos.currentSpeed = currentSpeed;
			KratosValuesChanged = true;
		}
		if(SceneManager.GetActiveScene().name == "Boss Level" && KratosValuesChanged){
			if(Kratos == null){
				Kratos = GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>();
				KratosValuesChanged = false;
			}
		}

		if(SceneManager.GetActiveScene().name == "MainMenuScene"){
			Destroy(this.gameObject);
		}
		
	}
}
