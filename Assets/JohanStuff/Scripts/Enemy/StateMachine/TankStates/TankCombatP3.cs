using UnityEngine;

public class TankCombatP3 : State
{
    public TankCombatP3(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        hasAttacked = false;
        timer = 0.08f;
        enemy.animator.SetTrigger("Attack");
        //enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }
    float timer;
    bool hasAttacked;
    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0 && !hasAttacked)
        {
            enemy.EnemyAttack();
            enemy.area.SetActive(false);
            hasAttacked = true;
        }
    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase4);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}

