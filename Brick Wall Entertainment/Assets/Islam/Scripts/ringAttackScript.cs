using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ringAttackScript : MonoBehaviour {
    float time;
    float scaleMax = 16;
    float scaleMin = 1;
    float yMax = 5f;
    float yMin = .2f;
    float scaleTime = .005f;
    float moveTime = .02f;
    // Use this for initialization
    void Start () {
        time = 0;
        StartCoroutine(ExecuteAfterTime(1));
    }
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time > 5)
            GameObject.Destroy(gameObject);
    }

    IEnumerator scaleUp()
    {
        for (float f = scaleMin; f < scaleMax; f += .1f)
        {
            gameObject.transform.localScale = new Vector3(f,f,f);
            yield return new WaitForSeconds(scaleTime);
        }
        StartCoroutine(scaleDown());
    }
    IEnumerator scaleDown()
    {
        for (float f = scaleMax; f > scaleMin; f -= .1f)
        {
            gameObject.transform.localScale = new Vector3(f, f, f);
            yield return new WaitForSeconds(scaleTime);
        }
        StartCoroutine(scaleUp());
    }

    IEnumerator moveUp()
    {
        for (float f = yMin; f < yMax; f += .1f)
        {
            transform.position = new Vector3(0,f,0);
            yield return new WaitForSeconds(moveTime);
        }
        StartCoroutine(moveDown());
    }
    IEnumerator moveDown()
    {
        for (float f = yMax; f > yMin; f -= .1f)
        {
            transform.position = new Vector3(0, f, 0);
            yield return new WaitForSeconds(moveTime);
        }
        StartCoroutine(moveUp());
    }

    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(scaleUp());
        StartCoroutine(moveDown());
    }
}
