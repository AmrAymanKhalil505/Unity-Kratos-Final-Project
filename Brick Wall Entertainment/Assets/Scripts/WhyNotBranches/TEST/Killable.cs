using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// THIS IS A TEST SCRIPT
public class Killable : MonoBehaviour
{

    public bool isDead;

    void Start()
    {
        isDead = false;
        Invoke("Kill", 8);
    }

    void Kill()
    {
        isDead = true;
        this.gameObject.SetActive(false);
	}
}
