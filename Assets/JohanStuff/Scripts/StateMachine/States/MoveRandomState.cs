﻿using UnityEngine;

public class MoveRandomState : State
{
    
    public MoveRandomState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void HandleInput()
    {
        base.HandleInput();
        
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        
    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        character.Move();
    }
}
