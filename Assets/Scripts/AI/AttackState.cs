using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;

    public override void Enter()
    {
    }

    public override void Exit()
    {
    }

    public override void Perform()
    {
        if(ai.CanSeePlayer())
        {
            ai.Animator.SetBool("CanSeePlayer",true);
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                ai.Animator.SetBool("CanSeePlayer",false);
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
