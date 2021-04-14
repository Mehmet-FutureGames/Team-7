using UnityEngine;

public class ChargeAttackState : State
{

    public ChargeAttackState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.area.SetActive(true);
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        Vector3 dirToPlayer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(enemy.attack);
    }

    public override void Action()
    {
        base.Action();
    }
}
