using UnityEngine;

public class TankCombatP6 : State
{
    public TankCombatP6(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        hasAttacked = false;
        timer = 0.08f;
        //Will perform frontal cone attack.
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
    }
    float timer;
    bool hasAttacked;
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0 && !hasAttacked)
        {
            enemy.EnemyConeAttack();
            enemy.area2.SetActive(false);
            hasAttacked = true;
        }
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
            stateMachine.ChangeState(enemy.combatPhase1);
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}