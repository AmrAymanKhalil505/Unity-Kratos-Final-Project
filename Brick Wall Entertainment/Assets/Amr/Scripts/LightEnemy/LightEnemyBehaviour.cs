using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class LightEnemyBehaviour : MonoBehaviour {
	[Header ("Animation related")]
	Animator Anim;
	public AnimationClip runningClip;

	public GameObject enemyWeapon;

	//keys used in the notifcation of animation 
	string HP = "HP";
	string KratosNear = "KratosNear";

	string CanHit = "CanHit";
	string GotHit = "GotHit";



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
	void FixedUpdate () {
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
		if(!Anim.GetBool(GotHit)){
			Anim.SetInteger(HP, Anim.GetInteger(HP)-x);
			hit();
		}
	}
	public void hit(){
		Anim.SetBool(GotHit, true);
	}
	public void doneGettingHit(){
		Anim.SetBool(GotHit, false);
	}
	public void playSound(string soundName){
		string soundNameCast = "LightEnemyCast";
		string soundNameImpact = "LightEnemyImpact";
		string soundNameDeath = "LightEnemyDeath";
		string soundNameStep = "LightEnemyStep";
		if(soundNameCast  == soundName ){
			source.PlayOneShot(AudioClipCast ,AudioVolume);
		}else if (soundNameImpact  == soundName ){
			source.PlayOneShot(AudioClipImpact ,AudioVolume);
		}else if (soundNameDeath  == soundName ){
			source.PlayOneShot(AudioClipDeath ,AudioVolume);
		}else if(soundNameStep== soundNameStep){
			source.PlayOneShot(AudioClipStep,AudioVolume);
		}	
	}
	public void activateWeaponCollider(int colliderEnabled){
		enemyWeapon.GetComponent<CapsuleCollider>().enabled = (colliderEnabled==1);
	}
}
