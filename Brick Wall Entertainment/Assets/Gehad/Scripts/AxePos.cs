using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePos : MonoBehaviour {

	public GameObject Spine;
	public GameObject hand;
	public GameObject Axe;

	// Use this for initialization
	void Start () {
		putOnSpine();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void putOnSpine(){
		Axe.transform.parent = Spine.transform;
		Axe.transform.localPosition = new Vector3(-0.04273097f,0.5498853f,-0.3957425f);
		Axe.transform.localEulerAngles = new Vector3(59.617f,109.646f,120.542f);
	}

	public void putInHand(){
		Axe.transform.parent = hand.transform;
		Axe.transform.localPosition = new Vector3(-0.9542757f,0.4619941f,-0.1204005f);
		Axe.transform.localEulerAngles = new Vector3(4.4f,78.464f,264.392f);
	}

	public void leaveAxe(){
		Axe.transform.parent = null;
	}
}
