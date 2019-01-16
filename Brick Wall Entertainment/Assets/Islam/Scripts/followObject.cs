using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class followObject : MonoBehaviour {
    public Transform target;
    NavMeshAgent nav;
    Animator anim;

	// Use this for initialization
	void Start () {
        nav = gameObject.GetComponent<NavMeshAgent>();
        nav.isStopped = true;
        anim = gameObject.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        //if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        //{
        //    Debug.Log("Idle");
        //}
        //else if(anim.GetCurrentAnimatorStateInfo(0).IsName("Follow"))
        //{
        //    Debug.Log("Follow");
        //}
        //else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Rotate"))
        //{
        //    Debug.Log("Rotate");
        //}
        nav.isStopped = anim.GetBool("StopNav");
        if (!nav.isStopped)
        {
            if(nav.remainingDistance!=0)
                if (nav.remainingDistance>nav.stoppingDistance)
                    anim.SetBool("Follow", true);
                else
                    anim.SetBool("Follow", false);

            nav.destination = target.position;

            Vector3 targetForward = nav.destination - transform.position;
            Vector3 newForward = Vector3.RotateTowards(transform.forward, targetForward, 4f*Time.deltaTime,0);

            if (!transform.forward.Equals(newForward))
            {
                anim.SetBool("Rotate",true);
                transform.rotation = Quaternion.LookRotation(newForward);
            }
            else
            {
                anim.SetBool("Rotate", false);
            }
        }
    }
}
