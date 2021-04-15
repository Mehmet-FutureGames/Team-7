using UnityEngine;

public class IdleState : State
{
    public IdleState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
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
        if(enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.chargeAttackState);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}
