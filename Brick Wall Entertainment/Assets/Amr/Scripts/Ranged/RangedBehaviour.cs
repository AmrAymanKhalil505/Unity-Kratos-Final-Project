using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BrickWallEntertainment.Managers;



public class RangedBehaviour : MonoBehaviour {
	[Header ("Animation related")]
	public Animator Anim;
	public AnimationClip runningClip;


	//keys used in the notifcation of animation 
	string HP = "HP";
	string KratosNear = "KratosNear";
	string CastingDone = "CastingDone";
	string CanHit = "CanHit";
	string GotHit = "GotHit";
	string TooNear = "TooNear";
	string BackingDownTimer = "BackingDownTimer";

	bool tookXP = false;


	[Header ("Audio related")]
	public AudioClip AudioClipCast ;
	public AudioClip AudioClipImpact ;
	public AudioClip AudioClipDeath ;
	public AudioClip AudioClipStep;
	public float AudioVolume; 
	private AudioSource source;
	

	//against Kratos behaviour related 

	public string KratosTag = "Kratos";
	
	bool KratosTooNear = false ;
	bool KratosNearB = false;
	GameObject KratosGO ;
	Vector3 KratosLastPostion;
	NavMeshAgent NMA;
	public int MaxHP;
	
	public float RotationSpeed = 2;
	public float DistanceToNotify =10;
	public float DistanceToKratosToHit=7;
	public float DistanceToKratosTooClose=2;
	public float BackingDownTime;
	public float BackingDownTimerGap=2;
	public float BackMultiplier = -3f;

	private float gothitTimer = 0;

	public GameObject RightHand;
	void Start () {
		Anim = GetComponent<Animator>();
		Anim.SetInteger(HP, MaxHP);
		Anim.SetBool(KratosNear , false);
		
		Anim.SetBool(CanHit, false);
		Anim.SetBool(GotHit, false);
		Anim.SetBool(TooNear, false);
		
		BackingDownTime= BackingDownTimerGap;

		source = GetComponent<AudioSource>();

		NMA = GetComponent<NavMeshAgent>();	
		KratosGO = GameObject.FindGameObjectsWithTag(KratosTag)[0];
		KratosLastPostion = KratosGO.GetComponent<Transform>().position;
	}
	
	void FixedUpdate () {
		gothitTimer -= Time.deltaTime;
		if(gothitTimer < 0){
			doneGettingHit();
		}
		BackingDownTime-= Time.deltaTime;
		if(Vector3.Distance(transform.position , KratosGO.transform.position) < DistanceToNotify){
			notifyKratosApproch();
		}
		if(BackingDownTime<0 && KratosTooNear ){
			NMA.Stop();
		}else{
			NMA.Resume();
		}
		shouldFollowKratos();
		shouldHitKratos();
	}

	void shouldFollowKratos(){
		if(KratosNearB&&Anim.GetCurrentAnimatorClipInfo(Anim.GetLayerIndex("Base Layer"))[0].clip == runningClip){
			KratosLastPostion = KratosGO.transform.position;
			if(!KratosTooNear ){
				NMA.SetDestination(KratosLastPostion);
			}else{
				Vector3 toPlayer = KratosLastPostion - transform.position;
				Vector3 targetPosition = toPlayer.normalized * BackMultiplier;
				NMA.SetDestination(transform.position+targetPosition);
			}
		}else {
			NMA.SetDestination(transform.position);
		}
	}
	void shouldHitKratos(){
		bool TooNearKratosDistance =Vector3.Distance(transform.position , KratosGO.transform.position)<DistanceToKratosTooClose;
		tooNearSet(TooNearKratosDistance);
		if(KratosNearB){
				bool NearKratosDistance =Vector3.Distance(transform.position , KratosGO.transform.position)<DistanceToKratosToHit;
				
				Vector3 dir = (KratosGO.transform.position- transform.position).normalized;
				float dot = Vector3.Dot(dir, transform.forward);
				bool lookingAtKratos = Mathf.Abs(dot - 1 )<= 0.1f;
				if(TooNearKratosDistance){
					
					if(BackingDownTime<2){
						if(!lookingAtKratos){

							Vector3 newDir=  Vector3.RotateTowards(transform.forward,  KratosGO.transform.position-transform.position, RotationSpeed * Time.deltaTime,0.0f);
							transform.rotation = Quaternion.LookRotation(newDir);
						}else{
							canHit();
						}
					}else{
						cannotHit();
					}
				}else if(NearKratosDistance){
					if(BackingDownTime < 2){
						if(!lookingAtKratos){
							Vector3 newDir=  Vector3.RotateTowards(transform.forward,  KratosGO.transform.position-transform.position, RotationSpeed * Time.deltaTime,0.0f);
							transform.rotation = Quaternion.LookRotation(newDir);
						}else{
							canHit();
						}
					}
				}else{
					cannotHit();
				}
		} 
	}

	public void notifyKratosApproch(){
		Anim.SetBool(KratosNear , true);
		KratosNearB= true;
	}
	public void notifyCastingDone(){
		Anim.SetBool(CastingDone , true);
	}

	void canHit(){
		Anim.SetBool(CanHit, true);
		print(BackingDownTime);
	}
	void cannotHit(){
		Anim.SetBool(CanHit, false);	
	}
	public void damage(int x){
		if(!Anim.GetBool(GotHit)){
			Anim.SetInteger(HP, Anim.GetInteger(HP)-x);
			if(Anim.GetInteger(HP) <= 0){
				if(!tookXP){
					GameObject.FindGameObjectWithTag("Kratos").GetComponent<PlayerController>().currentXP += 50;
					tookXP = true;
				}
			}
			else{
				hit();
			}
		}
	}
	public void hit(){
		Anim.SetBool(GotHit, true);
		gothitTimer = 1;
	}
	public void tooNearSet(bool isTooNear){
		Anim.SetBool(TooNear, isTooNear);
		KratosTooNear= isTooNear;
		//Anim.SetBool(CanHit, !isTooNear || (BackingDownTime < 0));
	}
	
	public void doneGettingHit(){
		Anim.SetBool(GotHit, false);
	}
	public void restBackingDown(){
		BackingDownTime=BackingDownTimerGap;
		cannotHit();
	}
	public void playSound(string soundName){
		string soundNameCast = "RangedEnemyCast";
		string soundNameImpact = "RangedEnemyImpact";
		string soundNameDeath = "RangedEnemyDeath";
		string soundNameStep = "RangedEnemyStep";

		if(soundNameCast  == soundName ){
			AudioManager.Instance.Play("Enemy_Speech");
		}else if (soundNameImpact  == soundName ){
			AudioManager.Instance.Play("Enemy_hurt");
		}else if (soundNameDeath  == soundName ){
			AudioManager.Instance.Play("Enemy_Die");
		}else if (soundNameStep  == soundName ){
			AudioManager.Instance.Play("Enemy_footstep");
		}
	}
	public void attack(){
		RightHand.GetComponent<SpellAttack>().attack();
	}
	
	public void Die(){
		Destroy(this.gameObject);
	}
}
