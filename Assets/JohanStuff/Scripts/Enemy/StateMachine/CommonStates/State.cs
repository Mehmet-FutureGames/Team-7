using UnityEngine;
using System.Collections;

public interface IState
{
    void Action();
}
public abstract class State : IState
{
    protected Enemy enemy;
    protected StateMachine stateMachine;
    protected State(Enemy enemy, StateMachine stateMachine)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        enemy.moveDistance = enemy.defaultMoveDistance;
    }

    public virtual void HandleInput()
    {

    }

    public virtual void LogicUpdate()
    {

    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {

    }

    public virtual void NoteEventUpdate()
    {

    }

    public virtual void Action()
    {
        
    }

    public virtual void OnDestroy()
    {

    }
    public virtual void OnDisable()
    {

    }
    
}


