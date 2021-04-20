using UnityEngine;

public class TankCombatP1 : State
{
    public TankCombatP1(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.cyan;
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

