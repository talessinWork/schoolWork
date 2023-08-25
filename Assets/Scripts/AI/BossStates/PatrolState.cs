using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    public int waypointIndex;
    public float waitTimer;
    
    public override void Enter()
    {

    }
    public override void Perform()
    {
        PatrolCycle();
        if(ai.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }
    public override void Exit()
    {

    }
    public void PatrolCycle()
    {
        if(ai.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if(waitTimer > 3)
            {
                if(waypointIndex < ai.path.waypoints.Count -1)
                {
                    waypointIndex++;
                }
                else
                    waypointIndex = 0;
                ai.Movement.moveVec = ai.Agent.desiredVelocity.normalized;
                ai.Agent.SetDestination(ai.path.waypoints[waypointIndex].position);
                ai.Agent.nextPosition = ai.transform.position;
                waitTimer = 0;
            }
        }
    }
}
