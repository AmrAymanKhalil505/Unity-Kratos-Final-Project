using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().currentHealth;
	}
}
