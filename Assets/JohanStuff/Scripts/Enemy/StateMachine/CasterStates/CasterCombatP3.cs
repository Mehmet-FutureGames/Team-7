using UnityEngine;

public class CasterCombatP3 : State
{
    public CasterCombatP3(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.area.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 0.5f);
        enemy.area.SetActive(true);
        //enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        enemy.gameObject.GetComponentInChildren<Animator>().SetTrigger("Attack");

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

    }
    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();

        stateMachine.ChangeState(enemy.combatPhase4);
    }
}

