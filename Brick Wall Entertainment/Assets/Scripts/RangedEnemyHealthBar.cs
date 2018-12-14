using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RangedEnemyHealthBar : MonoBehaviour {

	private GameObject Enemy;

	// Use this for initialization
	void Start () {
		Enemy = this.transform.parent.transform.parent.transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Slider>().value = Enemy.GetComponent<Animator>().GetInteger("HP");
	}
}
