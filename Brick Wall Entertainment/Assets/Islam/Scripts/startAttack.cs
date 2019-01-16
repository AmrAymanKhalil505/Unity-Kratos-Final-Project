using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startAttack : StateMachineBehaviour {
    float time=0;
    public float delay = 3;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
        
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool follow = animator.GetBool("Follow");
        bool rotate = animator.GetBool("Rotate");
        bool roared = animator.GetBool("Roared");
        if(roared)
            time += Time.deltaTime;
        bool rightArmInjured = animator.GetBool("RightArmInjured");
        bool leftArmInjured = animator.GetBool("LeftArmInjured");
        bool legsInjured = animator.GetBool("LegsInjured");

        bool closeTarget = animator.GetBool("CloseTarget");
        if (time>=delay&& roared && !follow && !rotate)
        {
            animator.SetBool("Attack", true);

            // animator.ResetTrigger("AxeAttack");
            // animator.ResetTrigger("MagicAttack");
            // animator.ResetTrigger("StompAttack");
            // animator.ResetTrigger("HornsAttack");
            // animator.ResetTrigger("SwipeAttack");
            // animator.ResetTrigger("KickAttack");
            int choice = -1;
            //int choice = 3;
            //animator.SetTrigger("HornsAttack");
            while (choice<0)
            {
                if(!closeTarget)
                    choice = Random.Range(0, 4);
                else
                    choice = Random.Range(0, 8);

                if (choice == 0)
                {
                    if (!rightArmInjured)
                        animator.SetBool("AxeAttack",true);
                    else
                        choice = -1;
                }
                else if (choice == 1)
                {
                    if (!leftArmInjured)
                    {
                        animator.SetTrigger("MagicAttack");
                    }
                    else
                        choice = -1;
                }
                else if (choice == 2)
                {
                    if (!legsInjured)
                        animator.SetTrigger("StompAttack");
                    else
                        choice = -1;
                }else if (choice == 3)
                    animator.SetTrigger("HornsAttack");
                else if (choice == 4 || choice == 5)
                {
                    if(!legsInjured)
                        animator.SetBool("KickAttack",true);
                    else
                        choice = -1;
                }else if (choice == 6 || choice == 7)
                {
                    if (!leftArmInjured)
                        animator.SetBool("SwipeAttack",true);
                    else
                        choice = -1;
                }
            }
            time = 0;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{

    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
