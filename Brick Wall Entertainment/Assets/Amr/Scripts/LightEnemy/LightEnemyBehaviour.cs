﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BrickWallEntertainment.Managers;
public class LightEnemyBehaviour : MonoBehaviour {
	[Header ("Animation related")]
	public Animator Anim;
	public AnimationClip runningClip;

	public GameObject enemyWeapon;

	//keys used in the notifcation of animation 
	string HP = "HP";
	string KratosNear = "KratosNear";

	string CanHit = "CanHit";
	string GotHit = "GotHit";

	bool tookXP = false;



	[Header ("Audio related")]
	public AudioClip AudioClipCast ;
	public AudioClip AudioClipImpact ;
	public AudioClip AudioClipDeath ;
	public AudioClip AudioClipStep;
	public float AudioVolume; 
	private AudioSource source;
	

	//against Kratos behaviour related 

	public string KratosTag;
	public float DistanceToNotify; 
	bool KratosNearB = false;
	GameObject KratosGO ;
	Vector3 KratosLastPostion;
	NavMeshAgent NMA;
	public int MaxHP;

	public float RotationSpeed;
	public float DistanceToKratosToHit;

	public float BackingDownTime;
	public float BackingDownTimerGap=2;

	// Use this for initialization
	void Start () {
		Anim = GetComponent<Animator>();
		Anim.SetInteger(HP, MaxHP);
		Anim.SetBool(KratosNear , false);
		Anim.SetBool(CanHit, false);
		Anim.SetBool(GotHit, false);

		source = GetComponent<AudioSource>();

		NMA = GetComponent<NavMeshAgent>();	
		KratosGO = GameObject.FindGameObjectsWithTag(KratosTag)[0];
		KratosLastPostion = KratosGO.GetComponent<Transform>().position;
	}
	
	// Update is called once per frame
	// void FixedUpdate () {
	// 	BackingDownTime -= Time.deltaTime;
	// 	//is Kratos near
	// 	if(!KratosNearB && Vector3.Distance(transform.position , KratosGO.transform.position) < DistanceToNotify){
	// 		notifyKratosApproch();
	// 	}

	// 	// running towards Kratos 
	// 	if(KratosNearB){
	// 		if(Anim.GetCurrentAnimatorClipInfo(Anim.GetLayerIndex("Base Layer"))[0].clip == runningClip){
	// 				KratosLastPostion = KratosGO.transform.position;
	// 				NMA.SetDestination(KratosLastPostion);
	// 		}else{
	// 			NMA.SetDestination(transform.position);
	// 		}
	// 	}	
		
	// 	// able to hit Kratos ??
	// 	bool NearKratosDistance =Vector3.Distance(transform.position , KratosGO.transform.position)<DistanceToKratosToHit;
	// 	// looking at kratos ??
	// 	Vector3 dir = (KratosGO.transform.position- transform.position).normalized;
	// 	float dot = Vector3.Dot(dir, transform.forward);
	// 	bool lookingAtKratos = Mathf.Abs(dot - 1 )<= 0.1f;
	// 	if(NearKratosDistance){
	// 		if(BackingDownTime < 0){

	// 			cannotHit();
	// 			NMA.isStopped = false;
	// 			if(!lookingAtKratos){
	// 				Vector3 newDir=  Vector3.RotateTowards(transform.forward,  KratosGO.transform.position-transform.position, RotationSpeed * Time.deltaTime,0.0f);
	// 				transform.rotation = Quaternion.LookRotation(newDir);
	// 			}else{
	// 				canHit();
	// 				BackingDownTime = BackingDownTimerGap;
	// 				NMA.isStopped = true;
	// 			}
	// 		}
	// 	}else{
	// 		cannotHit();
	// 		NMA.isStopped = false;
	// 	}

	// }

	void FixedUpdate () {
		BackingDownTime -= Time.deltaTime;
		if(BackingDownTime < 0){
			UnBlock(); 
		}
        //is Kratos near
        if(!KratosNearB && Vector3.Distance(transform.position , KratosGO.transform.position) < DistanceToNotify){
            notifyKratosApproch();
        }

        // running towards Kratos 
        if(KratosNearB){
            if(Anim.GetCurrentAnimatorClipInfo(Anim.GetLayerIndex("Base Layer"))[0].clip == runningClip){
                    KratosLastPostion = KratosGO.transform.position;
                    NMA.SetDestination(KratosLastPostion);
            }else{
                NMA.SetDestination(transform.position);
            }
        }    
        
        // able to hit Kratos ??
        bool NearKratosDistance =Vector3.Distance(transform.position , KratosGO.transform.position)<DistanceToKratosToHit;
        // looking at kratos ??
        Vector3 dir = (KratosGO.transform.position- transform.position).normalized;
        float dot = Vector3.Dot(dir, transform.forward);
        bool lookingAtKratos = Mathf.Abs(dot - 1 )<= 0.1f;
        if(NearKratosDistance ){
            canHit();
            if(!lookingAtKratos){
                Vector3 newDir=  Vector3.RotateTowards(transform.forward,  KratosGO.transform.position-transform.position, RotationSpeed * Time.deltaTime,0.0f);
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }else{
            cannotHit();
        }

    }
	
	public void notifyKratosApproch(){
		Anim.SetBool(KratosNear , true);
		KratosNearB= true;
	}
	void canHit(){
		Anim.SetBool(CanHit, true);
	}
	void cannotHit(){
		Anim.SetBool(CanHit, false);	
	}
	public void damage(int x){
		if(Anim.GetInteger(HP) - x <= 0){
			Anim.SetTrigger("Death");
			Anim.SetInteger(HP,0);
			if(!tookXP){
				GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().currentXP += 50;
				tookXP = true;
			}
		}
		if(!Anim.GetBool(GotHit) && !tookXP){
			Anim.SetInteger(HP, Anim.GetInteger(HP)-x);
			hit();
		}
	}
	public void hit(){
		Anim.SetTrigger(GotHit);
	}
	public void doneGettingHit(){
		Anim.ResetTrigger(GotHit);
	}
	public void playSound(string soundName){
		string soundNameCast = "LightEnemyCast";
		string soundNameImpact = "LightEnemyImpact";
		string soundNameDeath = "LightEnemyDeath";
		string soundNameStep = "LightEnemyStep";
		if(soundNameCast  == soundName ){
			AudioManager.Instance.Play("Enemy_Speech");
		}else if (soundNameImpact  == soundName ){
			AudioManager.Instance.Play("Enemy_hurt");
		}else if (soundNameDeath  == soundName ){
			AudioManager.Instance.Play("Enemy_Die");
		}else if(soundNameStep == soundName){
			AudioManager.Instance.Play("Enemy_footstep");
		}	
	}
	public void activateWeaponCollider(int colliderEnabled){
		enemyWeapon.GetComponent<CapsuleCollider>().enabled = (colliderEnabled==1);
	}

	public void Block(){
		Anim.SetBool("Block",true);
		BackingDownTime = BackingDownTimerGap;
	}

	public void UnBlock(){
		Anim.SetBool("Block",false);
	}

	public void Die(){
		Destroy(this.gameObject);
	}
}
