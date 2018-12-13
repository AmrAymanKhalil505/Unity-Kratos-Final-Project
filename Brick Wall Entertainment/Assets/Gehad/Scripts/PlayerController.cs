using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public int maxHealth = 100;

	public int currentHealth = 100;

	private bool immune = false;

	public int currentXP = 0;

	public int requiredXP = 500;

	public int level = 1;

	public int RageMeter = 100;

	public int currentRage = 0;

	public bool RageMode = false;

	public int lightAttackDamage = 10;

	public int heavyAttackDamage = 30;

	float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	public float currentSpeed = 5;
	private float currentSpeed1 ;

	Animator animator;
	public GameObject camera;
	Transform cameraT;

	private AxePos a;

	private bool running = false;

	private int noOfClicks; //Determines Which Animation Will Play
    private bool canClick; //Locks ability to click during animation event
	private bool inCombo = false;
    private bool Rolling = false;
	private bool inAir = false;

	private bool canDoubleJump = false;

	private bool[] lightAttack = new bool[3]{false,false,false};

	public GameObject AxeParticles;
	private bool Dead = false;

	void Start () {
		animator = GetComponent<Animator>();
		a = GetComponent<AxePos>();
		cameraT = camera.transform;
		canClick = true;
		noOfClicks = 0;
		currentSpeed1 = currentSpeed;
	}

	void Update () {

		// if(animator.GetCurrentAnimatorStateInfo(0).IsName("Idle")){
		// 	a.putOnSpine();
		// }
		if(!Dead){

			if(Input.GetKeyDown(KeyCode.R) && currentRage >= RageMeter){
				RageMode = true;
				currentRage = 0;
				animator.SetTrigger("enrage");
				immune = true;
				animator.SetInteger("HeavyAttack", 0);
				noOfClicks = 0;
				AxeParticles.SetActive(true);
				Invoke("endRage",25);
			}

			if(animator.GetInteger("HeavyAttack") == 0){
				a.putOnSpine();
			}

			Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
			Vector2 inputDir = input.normalized;

			if (inputDir != Vector2.zero) {
				float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
				transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
			}

			// if(Input.GetMouseButtonDown(0) && !animator.GetBool("LightAttack")){
			// 	a.putInHand();
			// 	animator.SetBool("LightAttack",true);
			// }

			if(Input.GetKeyDown(KeyCode.Space) && inAir && canDoubleJump && !inCombo){
				animator.SetBool("DoubleJump",true);
				animator.SetBool("jumping", false);
				this.GetComponent<Rigidbody>().AddForce(new Vector3(0,200,0),ForceMode.Impulse);
				canDoubleJump = false;
			}

			if (Input.GetKeyDown(KeyCode.Space) && !inAir && !animator.GetBool("blocking") && !inCombo)
			{
				animator.SetBool("jumping", true);
				this.GetComponent<Rigidbody>().AddForce(new Vector3(0,200,0),ForceMode.Impulse);
				inAir = true;
				animator.SetBool("landing",false);
				canDoubleJump = true;
			}

			if (animator.GetBool("jumping"))
			{
				// transform.Translate(new Vector3(0, 0.2f, 0));
			}

			if (animator.GetBool("DoubleJump"))
			{
				// transform.Translate(new Vector3(0, 0.4f, 0));
			}

            if (inAir)
            {
                currentSpeed1 = 1;
            }
            //if (!inAir)
            //{
            //    currentSpeed1 = currentSpeed;
            //}

			// if (animator.GetCurrentAnimatorStateInfo(0).IsName("Falling 0"))
			// {
			//     transform.Translate(new Vector3(0, -0.1f, 0));
			//     if(transform.position.y < 0.1f)
			//     {
			//         animator.SetBool("landing", true);
			//     }
			// }

			if (Input.GetMouseButtonDown(1)) {
				// a.putInHand();
				ComboStarter(1);
			}

			if(Input.GetMouseButtonDown(0)){
				// a.putInHand();
				ComboStarter(0);
			}

			if(Input.GetKeyDown(KeyCode.LeftShift)){
				running = true;
			}
			if(Input.GetKeyUp(KeyCode.LeftShift)){
				running = false;
			}

			if(Input.GetKey(KeyCode.LeftControl) && input == Vector2.zero && !animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_1") && !animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_2") && !animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_3")){
				animator.SetBool("blocking",true);
				immune = true;
				currentSpeed1 = 0f;
				//input = Vector2.zero;
				animator.SetInteger("HeavyAttack", 0);
				a.putInHand();
				noOfClicks = 0;
				canClick = true;
				if(a.Axe.GetComponent<MeshCollider>().enabled){
					a.deactivateHitbox();
				}
			}
			if(Input.GetKeyUp(KeyCode.LeftControl)){
				animator.SetBool("blocking",false);
				immune = false;
				a.putOnSpine();
				currentSpeed1 = currentSpeed;
			}

			if(Input.GetKey(KeyCode.LeftControl) && input != Vector2.zero && !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll in place"))
			{
				animator.SetBool("Rolling", true);
				float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
				transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
				animator.SetInteger("HeavyAttack", 0);
				noOfClicks = 0;
				canClick = true;
				if(a.Axe.GetComponent<MeshCollider>().enabled){
					a.deactivateHitbox();
				}
			}

			if (Rolling)
			{
				transform.Translate(transform.forward * 5f * currentSpeed1 * Time.deltaTime, Space.World);
			}

			if(input != Vector2.zero && !running && !animator.GetBool("Rolling")){
				transform.Translate (transform.forward * currentSpeed1 * Time.deltaTime, Space.World);
			}

			if(input != Vector2.zero && running && !animator.GetBool("Rolling"))
			{
				transform.Translate (transform.forward * 4f * currentSpeed1 * Time.deltaTime, Space.World);
			}

			if(input != Vector2.zero && !running){
				animator.SetBool("walking",true);
				animator.SetBool("running",false);
			}
			else if(input != Vector2.zero && running){
				animator.SetBool("walking",false);
				animator.SetBool("running",true);
			}
			else{
				animator.SetBool("walking",false);
				animator.SetBool("running",false);
			}

			// if(animator.GetBool("blocking")){
			// 	a.putInHand();
			// }
			// else{
			// 	a.putOnSpine();
			// }

			if(noOfClicks > 0){
				inCombo = true;
			}
			else{
				inCombo = false;
			}

			if(inCombo){
				// animator.SetBool("walking",false);
				// running = false;
				currentSpeed1 = 0;
			}
			else{
				currentSpeed1 = currentSpeed;
			}
			// if(animator.GetCurrentAnimatorStateInfo(0).IsName("Block")){
			// 	a.putInHand();
			// }
			// else{
			// 	a.putOnSpine();
			// }
		}
	}

	 void ComboStarter(int type)
    {      
        if (canClick && !animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_3") && !animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_3"))           
        {           
            noOfClicks++;
			if(noOfClicks == 1 && type == 0){
				lightAttack[0] = true;
			}
			if(noOfClicks == 1 && type == 1){
				lightAttack[0] = false;
			}
			if(noOfClicks == 2 && type == 0){
				lightAttack[1] = true;
			}
			if(noOfClicks == 2 && type == 1){
				lightAttack[1] = false;
			}
			if(noOfClicks >= 3 && type == 0){
				lightAttack[2] = true;
			}
			if(noOfClicks >= 3 && type == 1){
				lightAttack[2] = false;
			}
        }
                
        if (noOfClicks == 1 && type == 1)
        {
			a.putInHand();           
            animator.SetInteger("HeavyAttack", 1);
			animator.SetBool("lightAttack",false);
        }

		if(noOfClicks == 1 && type == 0){
			// print("light");
			a.putInHand();
			animator.SetInteger("HeavyAttack", 1);
			animator.SetBool("LightAttack",true);
		}          
    }

	public void ComboCheck() {
        canClick = false;
         
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_1") && noOfClicks == 1 )
        {//If the first animation is still playing and only 1 click has happened, return to idle
            animator.SetInteger("HeavyAttack", 0);
			animator.SetBool("LightAttack",false);
			lightAttack = new bool[3]{false,false,false};
            canClick = true;
            noOfClicks = 0;
        }
		else if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_1") && noOfClicks == 1){
			animator.SetInteger("HeavyAttack", 0);
			animator.SetBool("LightAttack",false);
			lightAttack = new bool[3]{false,false,false};
            canClick = true;
            noOfClicks = 0;
		}
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_1") &&  noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo          
            animator.SetInteger("HeavyAttack", 2);
			a.putInHand();
			if(lightAttack[1]){
				animator.SetBool("LightAttack",true);
			}
			else{
				animator.SetBool("LightAttack",false);
			}
            canClick = true;
        }
		else if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_1") && noOfClicks >= 2){
			animator.SetInteger("HeavyAttack", 2);
			a.putInHand();
			if(lightAttack[1]){
				animator.SetBool("LightAttack",true);
			}
			else{
				animator.SetBool("LightAttack",false);
			}
            canClick = true;
		}
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_2") && noOfClicks == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle         
            animator.SetInteger("HeavyAttack", 0);
			animator.SetBool("LightAttack",false);
			lightAttack = new bool[3]{false,false,false};
            canClick = true;
            noOfClicks = 0;
        }
		else if(animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_2") && noOfClicks == 2){           
            animator.SetInteger("HeavyAttack", 0);
			animator.SetBool("LightAttack",false);
			lightAttack = new bool[3]{false,false,false};
            canClick = true;
            noOfClicks = 0;
        }
       else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_2") && noOfClicks >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo         
            animator.SetInteger("HeavyAttack", 3);
			a.putInHand();
			if(lightAttack[2]){
				animator.SetBool("LightAttack",true);
			}
			else{
				animator.SetBool("LightAttack",false);
			}
            canClick = true;           
        }
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_2") && noOfClicks >= 3){           
            animator.SetInteger("HeavyAttack", 3);
			a.putInHand();
			if(lightAttack[2]){
				animator.SetBool("LightAttack",true);
			}
			else{
				animator.SetBool("LightAttack",false);
			}
            canClick = true;           
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_3"))
        { //Since this is the third and last animation, return to idle          
            animator.SetInteger("HeavyAttack", 0);
			animator.SetBool("LightAttack",false);
			lightAttack = new bool[3]{false,false,false};
            canClick = true;
            noOfClicks = 0;
        }
		else if (animator.GetCurrentAnimatorStateInfo(0).IsName("LightAttack_3")){
            animator.SetInteger("HeavyAttack", 0);
			animator.SetBool("LightAttack",false);
			lightAttack = new bool[3]{false,false,false};
            canClick = true;
            noOfClicks = 0;
        }      
    }

	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.tag == "Ground"){
			if(inAir){
				animator.SetBool("landing",true);
				inAir = false;
			}
		}
		if(collision.gameObject.tag == "LightEnemy"){
			if(!immune){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
				}
			}
		}
        if(collision.gameObject.tag == "lava" || collision.gameObject.tag == "PitSpikes")
        {
            animator.SetTrigger("Dead");
            Dead = true;
        }

        if (collision.gameObject.tag == "Rock" )
        {
            if (!immune && !Dead)
            {
                currentHealth -= 10;
                if (currentHealth == 0)
                {
                    animator.SetTrigger("Dead");
                    Dead = true;
                }
                else
                {
                    animator.SetTrigger("hit");
                    animator.SetInteger("HeavyAttack", 0);
                    noOfClicks = 0;
                    canClick = true;
                }
            }
        }
    }

	void OnCollisionExit(Collision collision){
		if(collision.gameObject.tag == "Ground" && !animator.GetBool("jumping")){
			animator.SetBool("landing",false);
			inAir = true;
			canDoubleJump =true;
		}
	}

	void OnTriggerEnter(Collider collider){
		if(collider.tag == "BossAxe" && GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().GetBool("AxeAttack")){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}			
		}
		if(collider.tag == "BossLeftHand" && GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().GetBool("SwipeAttack")){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}			
		}
		if(collider.tag == "BossLeg" && GameObject.FindGameObjectWithTag("Boss").GetComponent<Animator>().GetBool("KickAttack")){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}			
		}
		if(collider.tag == "Ring"){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}
		}
		if(collider.tag == "MagicAttackBall"){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}
		}
		if(collider.tag == "AttackWave"){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}
		}
		if(collider.tag == "Rock" || collider.tag == "Spikes"){
			if(!immune && !Dead){
				currentHealth -= 10;
				if(currentHealth == 0){
					animator.SetTrigger("Dead");
					Dead = true;
				}
				else{
					animator.SetTrigger("hit");
					animator.SetInteger("HeavyAttack", 0);
					noOfClicks = 0;
					canClick = true;
				}
			}
		}
	}

	// void OnTriggerEnter(Collider collider){
	// 	if(collider.tag == "plane"){
	// 		animator.SetBool("landing",true);
	// 	}
	// }

    public void Unroll()
    {
        animator.SetBool("Rolling", false);
    }

    public void Roll()
    {
        Rolling = true;
    }

    public void notRoll()
    {
        Rolling = false;
    }

    public void notJump()
    {
        animator.SetBool("jumping", false);
    }

    public void notLand()
    {
        animator.SetBool("landing", false);
    }

	public void notDoubleJump(){
		animator.SetBool("DoubleJump",false);
	}

	public void notEnrage(){
		immune = false;
	}

	void endRage(){
		RageMode = false;
		AxeParticles.SetActive(false);
	}
}