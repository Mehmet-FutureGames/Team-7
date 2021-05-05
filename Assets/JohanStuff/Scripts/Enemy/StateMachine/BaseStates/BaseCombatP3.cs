using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCombatP3 : State
{
    public BaseCombatP3(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        Debug.Log("attack!");
        base.Enter();
        hasAttacked = false;
        timer = 0.07f;

        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;

    }
    float timer = 0.2f;
    bool hasAttacked;
    public override void LogicUpdate()
    {
        base.LogicUpdate();
        timer -= Time.deltaTime;
        if (timer <= 0 && hasAttacked == false)
        {
            enemy.EnemyAttack();
            hasAttacked = true;
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
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase1);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
}