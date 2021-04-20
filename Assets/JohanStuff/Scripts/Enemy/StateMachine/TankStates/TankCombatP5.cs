using UnityEngine;

public class TankCombatP5 : State
{
    public TankCombatP5(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //will prepare frontal Cone Attack and add marker
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(enemy.combatPhase6);
    }
}