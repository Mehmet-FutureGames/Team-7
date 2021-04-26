using UnityEngine;

public class BaseCombatP2 : State
{

    public BaseCombatP2(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        enemy.area.gameObject.GetComponent<MeshRenderer>().material.color = new Color(1f, 0f, 0f, 0.2f);
        enemy.area.SetActive(true);
        Vector3 dirToPlayer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
        //enemy.area.transform.position = enemy.player.position;
        enemy.area.transform.position = enemy.agentObj.transform.position + (dirToPlayer * enemy.attackAreaScale.z);
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.green;
        
    }

    public override void Exit()
    {
        base.Exit();

    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(enemy.combatPhase3);
    }

    public override void Action()
    {
        base.Action();
    }
}
