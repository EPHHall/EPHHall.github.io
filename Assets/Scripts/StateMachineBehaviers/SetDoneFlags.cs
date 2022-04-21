using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetDoneFlags : StateMachineBehaviour
{
    //[System.NonSerialized]
    public SS.Animation.AnimationObject animObject;
    public int runHowManyTimes = -1;

    private int timesRun = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("SetDoneFlags.Enter");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("SetDoneFlags.Exit");
        timesRun++;

        if (runHowManyTimes != -1)
        {
            if (animObject != null && timesRun >= runHowManyTimes)
            {
                animator.SetTrigger("End");
                animObject.FinishAnimation();
            }
        }
        else if(animObject != null)
        {
            animObject.FinishAnimation();
        }
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
