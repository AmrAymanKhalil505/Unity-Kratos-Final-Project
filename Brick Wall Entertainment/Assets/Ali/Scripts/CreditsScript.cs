using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsScript : MonoBehaviour {

    public RectTransform rectTrans;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        rectTrans.Translate(0, 1f, 0);

       // print("X= " +rectTrans.position.x);
       // print("Y= " + rectTrans.position.y);
       // print("Z= " + rectTrans.position.z);

        // if (rectTrans.position.y >= 2300f)
        // {
        //    print("Y inside= " + rectTrans.position.y);
        //    rectTrans.localPosition(277f, -1600f, 0f);
        // }

    }
}
