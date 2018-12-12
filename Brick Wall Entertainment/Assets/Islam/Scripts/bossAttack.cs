using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossAttack : MonoBehaviour {
    public GameObject attackWave;
    public GameObject magicAttackBall;
    public GameObject rock;
    public GameObject ring;
    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void axeAttack()
    {
        Vector3 axePos = GetComponent<bossTakeDamage>().axe.transform.position;
        Vector3 pos = new Vector3(axePos.x, .7f+transform.position.y, axePos.z);
        //Debug.Log("Hey");
        Instantiate(attackWave, pos, transform.rotation);
    }

    void magicAttack()
    {
        Vector3 magicBallPos = GetComponent<bossTakeDamage>().magicBall.transform.position;
        Instantiate(magicAttackBall, magicBallPos, Quaternion.identity);
    }

    void rockAttack()
    {
        Vector3 target = GameObject.FindGameObjectWithTag("Kratos").transform.position;
        Vector3 rockPos = target;
        rockPos.y += 10;
        Instantiate(rock, rockPos, Quaternion.identity);
    }

    void hornsAttack()
    {
        Vector3 pos = Vector3.zero + new Vector3(transform.position.x,0,transform.position.y);
        pos.y = 2.25f + transform.position.y;
        Instantiate(ring, pos, Quaternion.identity);
    }
}
