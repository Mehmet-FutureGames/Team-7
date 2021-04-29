using UnityEngine;

public class TankCombatP6 : State
{
    public TankCombatP6(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //Will perform frontal cone attack.
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
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