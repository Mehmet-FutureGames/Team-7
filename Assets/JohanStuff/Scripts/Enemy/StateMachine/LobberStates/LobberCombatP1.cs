using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobberCombatP1 : State
{
    public LobberCombatP1(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //PrepareThrow
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase2);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}
