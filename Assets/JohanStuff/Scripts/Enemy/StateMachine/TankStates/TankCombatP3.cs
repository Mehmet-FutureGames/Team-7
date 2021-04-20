using UnityEngine;

public class TankCombatP3 : State
{
    public TankCombatP3(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0.07f;

        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }
    float timer;

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            enemy.EnemyAttack();
            enemy.area.SetActive(false);
        }
    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase1);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}

