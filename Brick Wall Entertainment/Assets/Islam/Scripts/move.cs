using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float horizontal = Input.GetAxis("Horizontal")*3*Time.deltaTime;
        float vertical = Input.GetAxis("Vertical")*3*Time.deltaTime;
        float jump = Input.GetAxis("Jump") * 6 * Time.deltaTime;
        gameObject.transform.Translate(new Vector3(horizontal,jump,vertical),Space.World);

    }
}
