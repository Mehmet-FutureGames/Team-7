using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCombatP1 : State
{
    public NinjaCombatP1(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }
    List<GameObject> arrows = new List<GameObject>();
    bool hasarrowList;
    Vector3 dirToPlayer;
    public override void Enter()
    {
        if (enemy.area.activeSelf)
        {
            enemy.area.SetActive(false);
        }
        if (enemy.trailRenderer.enabled == true)
        {
            enemy.trailRenderer.enabled = false;
        }
        dirToPlayer = (new Vector3(enemy.player.position.x, 0, enemy.player.position.z) - enemy.agentObj.transform.position).normalized;
        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
        if (!hasarrowList)
        {
            for (int i = 0; i < (int)enemy.attackRange; i++)
            {
                //Vector3 pos = new Vector3(enemy.agentObj.transform.localPosition.x, enemy.agentObj.transform.localPosition.y, enemy.agentObj.transform.localPosition.z + i);
                arrows.Add(ObjectPooler.Instance.SpawnFormPool2("EnemyDashArrow", enemy.agentObj.transform.position, Quaternion.Euler(0, 0, 0)));
                arrows[i].SetActive(false);
            }
            hasarrowList = true;
        }
        enemy.ninjaTarget = enemy.agentObj.transform.position + dirToPlayer * enemy.attackRange * 2;
        CoroutineRunner.Instance.SCouroutine(SpawnArrows());
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
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase2);
            return;
        }
        stateMachine.ChangeState(enemy.moveState);
    }

    public override void Action()
    {
        base.Action();
    }
    IEnumerator ActivateArrows()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            yield return new WaitForSeconds(0.01f);
            arrows[i].SetActive(true);
        }
    }
    IEnumerator SpawnArrows()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            yield return new WaitForSeconds(0.05f);
            arrows[i].transform.position = enemy.agentObj.transform.position + (dirToPlayer * i);
            arrows[i].transform.localRotation = Quaternion.LookRotation(dirToPlayer);
            arrows[i].SetActive(true);
            if (i >= 4)
            {
                break;
            }
        }
        
    }
    public override void OnDisable()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (arrows[i].activeSelf)
            {
                arrows[i].SetActive(false);
            }
        }
    }
    public override void OnDestroy()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (arrows[i].activeSelf)
            {
                arrows[i].SetActive(false);
            }
        }
    }
}
