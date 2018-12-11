using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBehaviour : MonoBehaviour {
	public Vector3 TargetToKill;
	public float SpellSpeed;

	float lifeTime = 0.1f;
	void FixedUpdate () {
		float step = SpellSpeed * Time.deltaTime;
		lifeTime-=Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, TargetToKill, step);
		if(Vector3.Distance(transform.position,TargetToKill)<0.1){
				Vector3 toPlayer = TargetToKill - transform.position;
				Vector3 targetPosition = toPlayer.normalized * 3;
				TargetToKill= transform.position+targetPosition;
				GetComponent<Rigidbody>().useGravity=true;
		}
	}

	void OnCollisionEnter(Collision collision)
    {	
		if(lifeTime<0){
       		Destroy(gameObject);
		}
    }
}
