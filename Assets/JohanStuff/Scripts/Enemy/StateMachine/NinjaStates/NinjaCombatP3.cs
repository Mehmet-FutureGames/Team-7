using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCombatP3 : State
{
    public NinjaCombatP3(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    int counter;
    public override void Enter()
    {
        counter = 0;
        if (enemy.area.activeSelf)
        {
            enemy.area.SetActive(false);
        }
        //Stunned, do nothing for 3 beats
        if (enemy.trailRenderer.enabled == true)
        {
            enemy.trailRenderer.enabled = false;
        }
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        counter++;
        if(counter > 2)
        {
            if (enemy.distanceToPlayer <= enemy.attackRange)
            {
                stateMachine.ChangeState(enemy.combatPhase1);
                return;
            }
            stateMachine.ChangeState(enemy.moveState);
        }
    }

    public override void Action()
    {
        base.Action();
    }
}
