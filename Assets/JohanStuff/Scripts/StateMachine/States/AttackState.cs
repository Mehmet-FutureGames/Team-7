using UnityEngine;

public class AttackState : State
{

    public AttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.red;
        character.area.SetActive(false);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(character.moving);
    }
}