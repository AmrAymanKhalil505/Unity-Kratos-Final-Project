using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPointsUIScript : MonoBehaviour {

	private PlayerController Kratos;

	// Use this for initialization
	void Start () {
		Kratos = GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Kratos.SkillPoints == 0){
			this.GetComponent<Text>().text = "";
		}
		else{
			this.GetComponent<Text>().text = Kratos.SkillPoints.ToString() + "*";
		}
	}
}
