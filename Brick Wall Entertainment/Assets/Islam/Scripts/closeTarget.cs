using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class closeTarget : MonoBehaviour {
    public float threshold;
    public Transform target;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 displacement = target.position - transform.position;
        if (displacement.magnitude <= threshold)
            anim.SetBool("CloseTarget",true);
        else
            anim.SetBool("CloseTarget", false);
    }
}
