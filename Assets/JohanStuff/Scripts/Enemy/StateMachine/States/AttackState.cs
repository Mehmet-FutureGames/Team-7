using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    public AttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        timer = 0.07f;

        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        
    }
    float timer = 0.2f;

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            enemy.EnemyAttack();
            enemy.area.SetActive(false);
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
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}