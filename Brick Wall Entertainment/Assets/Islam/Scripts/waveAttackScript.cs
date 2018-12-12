using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waveAttackScript : MonoBehaviour
{
    GameObject target;
    float time;
    // Use this for initialization
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Kratos");
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float v = 3f * Time.deltaTime;
        float av = 1f * Time.deltaTime;
        Vector3 oldForward = transform.forward;
        Vector3 targetForward = target.transform.position - transform.position;
        targetForward.y = oldForward.y;
        Vector3 newForward = Vector3.RotateTowards(oldForward, targetForward,av,0);
        transform.rotation = Quaternion.LookRotation(newForward);
        transform.Translate(0, 0,v);
        if (time > 3) GameObject.Destroy(gameObject);
    }

}
