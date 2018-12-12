using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class face : MonoBehaviour {
    public Transform target;
    Animator anim;
	// Use this for initialization
	void Start () {
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        bool roared = anim.GetBool("Roared");
        bool attack = anim.GetBool("Attack");
        if (roared&&!attack)
        {
            Vector3 oldForward = transform.forward.normalized;
            oldForward.y = 0;

            Vector3 targetForward = target.position - transform.position;
            targetForward.Normalize();
            targetForward.y = 0;

            Vector3 newForward = Vector3.RotateTowards(oldForward, targetForward, 4f * Time.deltaTime, 0);
            //Debug.Log(newForward);
            float difference = (oldForward.normalized - targetForward.normalized).magnitude;
            if (difference>0.5f)
            {
                anim.SetBool("Rotate", true);
                transform.rotation = Quaternion.LookRotation(newForward);
            }
            else
            {
                anim.SetBool("Rotate", false);
            }
        }
    }
}
