using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;
    public override void Enter()
    {
        ai.Agent.SetDestination(ai.LastKnownPos);
    }
    public override void Perform()
    {
        if(ai.CanSeePlayer())
            stateMachine.ChangeState(new AttackState());
        if(ai.Agent.remainingDistance < ai.Agent.stoppingDistance)
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;
            if(moveTimer > Random.Range(3,5))
            {
                ai.Agent.SetDestination(ai.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }
            if(searchTimer > Random.Range(3,6))
        {
            stateMachine.ChangeState(new PatrolState());
        }
        }
        

        
    }
    
    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

}
