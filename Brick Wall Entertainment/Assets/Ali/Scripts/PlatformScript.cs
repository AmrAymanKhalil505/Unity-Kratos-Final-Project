using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour {
    bool goingDown=true;
    int counter=0;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (this.tag == "RightLeftPlatforms")
        {
            if(counter >= 200)
            {
                goingDown = !goingDown;
                counter = 0;
            }
            if (goingDown)
            {
                this.transform.Translate(0, -Time.deltaTime*1.15f, 0);
            }
            else
            {
                this.transform.Translate(0, Time.deltaTime*1.15f, 0);
            }
            counter++;
        }

        if (this.tag == "FrontBackPlatforms")
        {
            if (counter >= 200)
            {
                goingDown = !goingDown;
                counter = 0;
            }
            if (!goingDown)
            {
                this.transform.Translate(0, -Time.deltaTime*1.15f, 0);
            }
            else
            {
                this.transform.Translate(0, Time.deltaTime*1.15f, 0);
            }
            counter++;
        }

    }
}
