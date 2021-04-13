using UnityEngine;

public class ChargeAttackState : State
{

    public ChargeAttackState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered ChargeAttackState");
        character.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;

    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(character.attack);
    }

    public override void Action()
    {
        base.Action();
    }
}
