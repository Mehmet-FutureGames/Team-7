﻿using UnityEngine;


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
}

