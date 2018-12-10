using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followHandsOfTheWizard : MonoBehaviour {
	public GameObject HandOfTheWizard;
	private Vector3 Offest;
	// Use this for initialization
	void Start () {
		Offest = HandOfTheWizard.transform.position-transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Offest+  HandOfTheWizard.transform.position;
	}
}
