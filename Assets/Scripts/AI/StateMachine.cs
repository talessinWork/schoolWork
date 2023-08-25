using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    AIInterface ai;
    
    // property for patrol state
    public void Initialize()
    {
        
        ChangeState(new PatrolState());
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
        {
            activeState.Perform();
        }
    }
    public void ChangeState(BaseState newState)
    {
        // checking if we're running a state
        if(activeState != null)
        {
            //run cleanup
            activeState.Exit();
        }
        activeState = newState;
        // null check
        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.ai = GetComponent<AIInterface>();
            activeState.Enter();
        }
    }
}
