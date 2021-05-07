using UnityEngine;

public class TankCombatP5 : State
{
    public TankCombatP5(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.area2.SetActive(true);
        Vector3 dirToPlayer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
        //enemy.area.transform.position = enemy.player.position;
        enemy.area2.transform.position = enemy.agentObj.transform.position + (dirToPlayer * enemy.attackAreaScale2.z);
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