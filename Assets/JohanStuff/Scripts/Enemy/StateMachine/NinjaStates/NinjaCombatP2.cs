using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCombatP2 : State
{
    public NinjaCombatP2(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    List<GameObject> arrows = new List<GameObject>();
    bool hasarrowList;
    public override void Enter()
    {
        if (!hasarrowList)
        {
            for (int i = 0; i < (int)enemy.attackRange; i++)
            {
                Vector3 pos = new Vector3(enemy.agentObj.transform.localPosition.x, enemy.agentObj.transform.localPosition.y, enemy.agentObj.transform.localPosition.z + i);
                arrows.Add(ObjectPooler.Instance.SpawnFormPool("EnemyDashArrow", pos, Quaternion.Euler(90, 0, 0)));
            }
            hasarrowList = true;
        }
        else
        {
            for (int i = 0; i < arrows.Count; i++)
            {
                arrows[i].SetActive(true);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        for (int i = 0; i < arrows.Count; i++)
        {
            arrows[i].SetActive(false);
        }
    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        stateMachine.ChangeState(enemy.moveState);
    }

    public override void Action()
    {
        base.Action();
    }
}
