using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rockAttackScript : MonoBehaviour {
    float time;
	// Use this for initialization
	void Start () {
        time = 0;
    }
	
	// Update is called once per frame
	void Update () {
        if (transform.position.y > .32f)
        {
            float v = 10*Time.deltaTime;
            time += Time.deltaTime;
            transform.Translate(0, -v, 0,Space.World);
            //if (time > 3) GameObject.Destroy(gameObject);
        }
        else
        {
            StartCoroutine(Fade());
        }
	}

    IEnumerator Fade()
    {
        for (float f = 1f; f >= 0; f -= 0.02f)
        {
            Color currentColor = GetComponentInChildren<Renderer>().material.color;
            Color newColor = new Color(currentColor.r, currentColor.r, currentColor.r, f);
            GetComponentInChildren<Renderer>().material.SetColor("_Color",newColor);
            yield return new WaitForSeconds(.01f);
        }
        GameObject.Destroy(gameObject);
    }

}
