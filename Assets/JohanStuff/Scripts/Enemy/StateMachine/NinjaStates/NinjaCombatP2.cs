using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCombatP2 : State
{
    public NinjaCombatP2(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    Vector3 dirToPlayer;
    Vector3 target;
    bool hasAttacked;
    public override void Enter()
    {
        base.Enter();
        hasAttacked = false;
        dirToPlayer = (new Vector3(enemy.player.position.x, 0, enemy.player.position.z) - enemy.agentObj.transform.position).normalized;
        target = enemy.ninjaTarget;
        enemy.trailRenderer.enabled = true;
        enemy.area.SetActive(true);
        enemy.agent.enabled = false;
        enemy.agentObj.GetComponent<TrailRenderer>().enabled = true;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
        float distance = (target - enemy.agentObj.transform.position).magnitude;
        enemy.agentObj.transform.position = Vector3.MoveTowards(enemy.agentObj.transform.position, target, distance * 2 * Time.deltaTime);
        if (enemy.playerIsInAttackArea && PlayerBlock.isBlocking)
        {
            stateMachine.ChangeState(enemy.combatPhase3);
        }
        else if((enemy.playerIsInAttackArea && !PlayerBlock.isBlocking) && !hasAttacked)
        {
            enemy.EnemyAttack();
            hasAttacked = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        hasAttacked = false;
    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        enemy.agent.enabled = true;
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase1);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }
    
}
