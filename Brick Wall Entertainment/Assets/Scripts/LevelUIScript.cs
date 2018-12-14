using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIScript : MonoBehaviour {

	private PlayerController Kratos;

	// Use this for initialization
	void Start () {
		Kratos = GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Text>().text = Kratos.level.ToString();
	}
}
