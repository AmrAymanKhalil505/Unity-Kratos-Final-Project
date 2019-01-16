using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicAttackBallScript : MonoBehaviour {
    GameObject target;
    Vector3 targetPosition;
    float time;
	// Use this for initialization
	void Start () {
        target = GameObject.FindGameObjectWithTag("Kratos");
        targetPosition = target.transform.position + new Vector3(0,1,0);
        time = 0;
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        float v = 7*Time.deltaTime;
        Vector3 directionVector = targetPosition - transform.position;
        if (directionVector.magnitude>.1f)
        {
            directionVector.Normalize();
            directionVector = directionVector*v;
            gameObject.transform.Translate(directionVector, Space.World);
        }
        if (time>1.5f)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
