using UnityEngine;

public class CasterCombatP4 : State
{
    bool attackOnce;
    public CasterCombatP4(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        attackOnce = false;
        timer = 0.08f;
        //enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        //enemy.gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");
        AudioManager.PlaySound("FireExplosion2", "EnemySound");
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
        if (timer <= 0 && !attackOnce)
        {
            enemy.EnemyRangedAttack();
            enemy.area.SetActive(false);
            attackOnce = true;
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

