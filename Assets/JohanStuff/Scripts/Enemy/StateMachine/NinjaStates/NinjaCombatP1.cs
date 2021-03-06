using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaCombatP1 : State
{
    public NinjaCombatP1(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine) // Prepare Dash attack
    {
    }
    List<GameObject> arrows = new List<GameObject>();
    bool hasarrowList;
    Vector3 dirToPlayer;
    RaycastHit hit;
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
                arrows.Add(ObjectPooler.Instance.SpawnFormPool2("EnemyDashArrow", enemy.agentObj.transform.position, Quaternion.Euler(0, 0, 0)));
                arrows[i].SetActive(false);
            }
            hasarrowList = true;
        }
        enemy.ninjaTarget = enemy.agentObj.transform.position + dirToPlayer * enemy.attackRange * 2;
        if(Physics.Raycast(enemy.agentObj.transform.position, dirToPlayer, out hit, enemy.attackRange *2, enemy.obstacleLayer))
        {
            Vector3 dirToAgent = (hit.point - enemy.agentObj.transform.position).normalized;
            enemy.ninjaTarget = hit.point + dirToAgent * 5;
        }
        CoroutineRunner.Instance.SCouroutine(SpawnArrows());
        enemy.gameObject.GetComponentInChildren<Animator>().SetTrigger("Idle");
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

        stateMachine.ChangeState(enemy.combatPhase2);

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
            if (arrows[i] != null)
            {
                if (arrows[i].activeSelf)
                {
                    arrows[i].SetActive(false);
                }
            }
        }
    }
    public override void OnDestroy()
    {
        for (int i = 0; i < arrows.Count; i++)
        {
            if (arrows[i] != null)
            {
                if (arrows[i].activeSelf)
                {
                    arrows[i].SetActive(false);
                }
            }
        }
    }
}
