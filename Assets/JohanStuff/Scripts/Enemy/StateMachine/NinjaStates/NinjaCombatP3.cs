using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCombatP3 : State
{
    public NinjaCombatP3(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        //Stunned, do nothing for 3 beats
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(enemy.moveState);
    }

    public override void Action()
    {
        base.Action();
    }
}
