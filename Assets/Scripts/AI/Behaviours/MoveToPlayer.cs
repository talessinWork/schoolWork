using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveToPlayer : StateMachineBehaviour
{
    AIInterface ai;
    Damagable dmg;
    Movement movement;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai = animator.gameObject.GetComponent<AIInterface>();
        dmg = animator.gameObject.GetComponent<Damagable>();
        dmg.bossActive = true;
        ai.TargetPlayer();


    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai.Movement.moveVec = ai.Agent.desiredVelocity.normalized;
        ai.Agent.SetDestination(ai.Player.transform.position);
        ai.Agent.nextPosition = ai.transform.position;

    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ai.UnTarget();
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

    private void UpdateSlider(){

    }
}
