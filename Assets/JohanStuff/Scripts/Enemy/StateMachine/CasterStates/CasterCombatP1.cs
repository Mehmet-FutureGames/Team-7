using UnityEngine;

public class CasterCombatP1 : State
{
    public CasterCombatP1(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    int count;
    public override void Enter()
    {
        base.Enter();
        Vector3 dirToPlayer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
        count = 0;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        if (count > 0)
        {
            if (enemy.distanceToPlayer <= enemy.attackRange)
            {
                stateMachine.ChangeState(enemy.combatPhase2);
                return;
            }
            stateMachine.ChangeState(enemy.moveState);
        }
        count++;
    }
}

