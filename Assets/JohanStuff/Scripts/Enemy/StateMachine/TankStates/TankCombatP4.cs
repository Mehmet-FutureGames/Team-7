using UnityEngine;

public class TankCombatP4 : State
{
    public TankCombatP4(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
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
            stateMachine.ChangeState(enemy.combatPhase5);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}


