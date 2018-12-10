using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellAttack : MonoBehaviour {
	public GameObject Spell;
	public string KratosTag;
	public float ProjectileSpeed=3;
	GameObject KratosGO;
	void Start () {
		KratosGO = GameObject.FindGameObjectsWithTag(KratosTag)[0];
	}
	
	public void attack (){ 
		GameObject spellPartical = Instantiate (Spell,transform.position,Quaternion.EulerAngles(0,0,0)) as GameObject;
		spellPartical.GetComponent<SpellBehaviour>().SpellSpeed = ProjectileSpeed;
		spellPartical.GetComponent<SpellBehaviour>().TargetToKill= KratosGO.transform.position;
	}
	
}
