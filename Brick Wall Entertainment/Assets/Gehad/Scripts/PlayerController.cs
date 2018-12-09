using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float walkSpeed = 2;
	public float runSpeed = 6;

	public float turnSmoothTime = 0.2f;
	float turnSmoothVelocity;

	public float speedSmoothTime = 0.1f;
	float speedSmoothVelocity;
	public float currentSpeed;

	Animator animator;
	public GameObject camera;
	Transform cameraT;

	private AxePos a;

	private bool running = false;

	private int noOfClicks; //Determines Which Animation Will Play
    private bool canClick; //Locks ability to click during animation event
	private bool inCombo = false;
    private bool Rolling = false;

	void Start () {
		animator = GetComponent<Animator> ();
		a = GetComponent<AxePos>();
		cameraT = camera.transform;
		canClick = true;
		noOfClicks = 0;
	}

	void Update () {

		Vector2 input = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
		Vector2 inputDir = input.normalized;

		if (inputDir != Vector2.zero) {
			float targetRotation = Mathf.Atan2 (inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
			transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
		}

		if (Input.GetMouseButtonDown(1)) {
			ComboStarter();
		}

		if(Input.GetKeyDown(KeyCode.LeftShift)){
			running = true;
		}
		if(Input.GetKeyUp(KeyCode.LeftShift)){
			running = false;
		}

		if(Input.GetKey(KeyCode.LeftControl) && input == Vector2.zero){
			animator.SetBool("blocking",true);
			currentSpeed = 0f;
			//input = Vector2.zero;
			animator.SetInteger("HeavyAttack", 0);
			noOfClicks = 0;
		}
		if(Input.GetKeyUp(KeyCode.LeftControl)){
			animator.SetBool("blocking",false);
			currentSpeed = 5f;
		}

        if(Input.GetKey(KeyCode.LeftControl) && input != Vector2.zero & !animator.GetCurrentAnimatorStateInfo(0).IsName("Roll in place"))
        {
            animator.SetBool("Rolling", true);
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + cameraT.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
            animator.SetInteger("HeavyAttack", 0);
            noOfClicks = 0;
        }

        if (Rolling)
        {
            transform.Translate(transform.forward * 5f * currentSpeed * Time.deltaTime, Space.World);
        }

		if(input != Vector2.zero && !running && !animator.GetBool("Rolling")){
			transform.Translate (transform.forward * currentSpeed * Time.deltaTime, Space.World);
		}

		if(input != Vector2.zero && running && !animator.GetBool("Rolling"))
        {
			transform.Translate (transform.forward * 4f * currentSpeed * Time.deltaTime, Space.World);
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

		if(animator.GetBool("blocking") || noOfClicks > 0){
			a.putInHand();

		}
		else{
			a.putOnSpine();
		}

		if(noOfClicks > 0){
			inCombo = true;
		}
		else{
			inCombo = false;
		}

		if(inCombo){
			animator.SetBool("walking",false);
			running = false;
			currentSpeed = 0;
		}
		else{
			currentSpeed = 5f;
		}
		// if(animator.GetCurrentAnimatorStateInfo(0).IsName("Block")){
		// 	a.putInHand();
		// }
		// else{
		// 	a.putOnSpine();
		// }

	}

	 void ComboStarter()
    {      
        if (canClick)           
        {           
            noOfClicks++;
        }
                
        if (noOfClicks == 1)
        {           
            animator.SetInteger("HeavyAttack", 1);
        }          
    }

	public void ComboCheck() {
        canClick = false;
         
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_1") && noOfClicks == 1 )
        {//If the first animation is still playing and only 1 click has happened, return to idle
            animator.SetInteger("HeavyAttack", 0);
            canClick = true;
            noOfClicks = 0;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_1") &&  noOfClicks >= 2)
        {//If the first animation is still playing and at least 2 clicks have happened, continue the combo          
            animator.SetInteger("HeavyAttack", 2);
            canClick = true;
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_2") && noOfClicks == 2)
        {  //If the second animation is still playing and only 2 clicks have happened, return to idle         
            animator.SetInteger("HeavyAttack", 0);
            canClick = true;
            noOfClicks = 0;
        }
       else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_2") && noOfClicks >= 3)
        {  //If the second animation is still playing and at least 3 clicks have happened, continue the combo         
            animator.SetInteger("HeavyAttack", 3);
            canClick = true;           
        }
       else if (animator.GetCurrentAnimatorStateInfo(0).IsName("heavy_attack_3"))
        { //Since this is the third and last animation, return to idle          
            animator.SetInteger("HeavyAttack", 0);
            canClick = true;
            noOfClicks = 0;
        }      
    }

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
}