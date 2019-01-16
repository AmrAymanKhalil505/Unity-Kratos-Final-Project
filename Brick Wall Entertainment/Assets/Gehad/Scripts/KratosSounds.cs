using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BrickWallEntertainment.Managers;

public class KratosSounds : MonoBehaviour {

	public void RageSound(){
		AudioManager.Instance.Play("Kratos_Rage");
	}

	public void FootStepSound(){
		AudioManager.Instance.Play("Kratos_footstep");
	}

	public void HitSound(){
		AudioManager.Instance.Play("Kratos_hurt");
	}

	public void DeathSound(){
		AudioManager.Instance.Play("Kratos_Die");
	}

	public void CollectSound(){
		
	}
	
}
