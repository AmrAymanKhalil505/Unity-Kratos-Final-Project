using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Slider>().minValue = 0;
		this.GetComponent<Slider>().maxValue = GameObject.FindGameObjectWithTag("Boss").GetComponent<bossTakeDamage>().maxHealth;
		this.GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("Boss").GetComponent<bossTakeDamage>().health;
	}
}
