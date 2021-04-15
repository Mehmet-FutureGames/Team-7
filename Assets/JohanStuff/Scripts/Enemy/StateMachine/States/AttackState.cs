using UnityEngine;

public class AttackState : State
{
    public AttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.EnemyAttack();
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        enemy.area.SetActive(false);
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
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}