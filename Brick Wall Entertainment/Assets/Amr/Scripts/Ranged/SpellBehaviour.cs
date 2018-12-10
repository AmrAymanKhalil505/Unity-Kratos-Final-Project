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
		if(Vector3.Distance(transform.position,TargetToKill)<0){
			Destroy(gameObject);
		}
	}

	void OnCollisionEnter(Collision collision)
    {	
		if(lifeTime<0){
       		Destroy(gameObject);
		}
    }
}
