using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxePos : MonoBehaviour {

	public GameObject Spine;
	public GameObject hand;
	public GameObject Axe;
	private bool throwing = false;

	private bool wayback = false;
	private Vector3 direction;

	// Use this for initialization
	void Start () {
		putOnSpine();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void putOnSpine(){
		throwing = false;
		Axe.transform.parent = Spine.transform;
		Axe.transform.localPosition = new Vector3(-0.04273097f,0.5498853f,-0.3957425f);
		Axe.transform.localEulerAngles = new Vector3(59.617f,109.646f,120.542f);
	}

	public void putInHand(){
		throwing = false;
		wayback = false;
		this.GetComponent<Animator>().SetBool("LightAttack",false);
		Axe.transform.parent = hand.transform;
		Axe.transform.localPosition = new Vector3(-0.9542757f,0.4619941f,-0.1204005f);
		Axe.transform.localEulerAngles = new Vector3(4.4f,78.464f,264.392f);
	}

	public void leaveAxe(){
		Axe.transform.parent = null;
	}


	public void throw1(){
		direction = transform.forward;
		leaveAxe();
		throwing = true;
		Invoke("throw1action",0);
	}

	void throw1action(){
		// Axe.transform.Translate(this.transform.up.normalized);
		if(!wayback){
			Axe.transform.position += direction * Time.deltaTime * 12f;
		}else{
			Axe.transform.position -= direction * Time.deltaTime * 12f;
		}
		if((Axe.transform.position - transform.position).magnitude > 10){
			wayback = true;
		}
		Axe.transform.Rotate(0,10,0);
		if(throwing){
			Invoke("throw1action",0.0001f);
		}
		else{
			putInHand();
		}
	}

	public void throw2(){
		direction = transform.forward;
		leaveAxe();
		throwing = true;
		Invoke("throw2action",0);
	}

	void throw2action(){
		// Axe.transform.Translate(this.transform.up.normalized);
		if(!wayback){
			Axe.transform.position += direction * Time.deltaTime * 12f;
		}else{
			Axe.transform.position -= direction * Time.deltaTime * 12f;
		}
		if((Axe.transform.position - transform.position).magnitude > 20){
			wayback = true;
		}
		Axe.transform.Rotate(0,10,0);
		if(throwing){
			Invoke("throw2action",0.0001f);
		}
		else{
			putInHand();
		}
	}

	public void activateHitbox(){
		Axe.GetComponent<MeshCollider>().enabled = true;
	}

	public void deactivateHitbox(){
		Axe.GetComponent<MeshCollider>().enabled = false;
	}
}
