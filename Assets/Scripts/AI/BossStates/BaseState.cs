using UnityEngine.Animations;

public abstract class BaseState 
{
    public AIInterface ai;
    // instance of statemachine 
    public StateMachine stateMachine;
    public Movement movement;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();
}