using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class NinjaMoveState : State
{
    public NinjaMoveState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        if (enemy.area.activeSelf)
        {
            enemy.area.SetActive(false);
        }
        base.Enter();
        if(enemy.trailRenderer.enabled == true)
        {
            enemy.trailRenderer.enabled = false;
        }
        //enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
    }
    public override void LogicUpdate()
    {
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        if (enemy.distanceToPlayer <= enemy.attackRange)
        {
            stateMachine.ChangeState(enemy.combatPhase1);
            return;
        }
        Action();

    }

    public override void Action()
    {
        base.Action();
        enemy.moveCounter++;
        if (enemy.moveCounter == enemy.notesToMove)
        {
            Vector3 dirToSteer;
            Vector3 randomVector;
            Vector3 agentToRandom;
            Vector3 dir;
            NavMeshPath path = new NavMeshPath();
            switch (enemy.movePattern)
            {
                case MovePattern.TowardsPlayer:
                    if (enemy.moveCounter == enemy.notesToMove)
                    {

                        if (enemy.distanceToPlayer < enemy.moveDistance)
                        {
                            enemy.agent.CalculatePath(enemy.player.position, path);
                            enemy.agent.SetPath(path);
                            dirToSteer = (enemy.agent.steeringTarget - enemy.agentObj.transform.position).normalized;
                            enemy.agent.SetDestination(enemy.agentObj.transform.position + dirToSteer * enemy.moveDistance);
                        }
                        else
                        {
                            enemy.agent.CalculatePath(enemy.player.position, path);
                            enemy.agent.SetPath(path);
                            dirToSteer = (enemy.agent.steeringTarget - enemy.agentObj.transform.position).normalized;
                            enemy.agent.SetDestination(enemy.agentObj.transform.position + dirToSteer * enemy.moveDistance);
                        }
                        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToSteer);

                    }
                    if (enemy.moveCounter == enemy.notesToMove) { enemy.moveCounter = 0; }
                    break;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                case MovePattern.RandomDirection:

                    randomVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    agentToRandom = enemy.agentObj.transform.position + randomVector;
                    dir = (agentToRandom - enemy.agentObj.transform.position).normalized;
                    enemy.agent.SetDestination(enemy.agentObj.transform.position + dir * enemy.moveDistance);

                    enemy.agentObj.transform.rotation = Quaternion.LookRotation(dir);

                    if (enemy.moveCounter == enemy.notesToMove) { enemy.moveCounter = 0; }
                    break;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                case MovePattern.ProximityDetection:
                    dirToSteer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
                    if (enemy.moveCounter == enemy.notesToMove)
                    {

                        if (enemy.distanceToPlayer <= enemy.detectionRange)
                        {
                            enemy.agent.CalculatePath(enemy.player.position, path);
                            enemy.agent.SetPath(path);
                            dirToSteer = (enemy.agent.steeringTarget - enemy.agentObj.transform.position).normalized;
                            enemy.agent.SetDestination(enemy.agentObj.transform.position + dirToSteer * enemy.moveDistance);
                            enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToSteer);
                        }
                        else
                        {
                            randomVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                            agentToRandom = enemy.agentObj.transform.position + randomVector;
                            dir = (agentToRandom - enemy.agentObj.transform.position).normalized;
                            enemy.agent.SetDestination(enemy.agentObj.transform.position + dir * enemy.moveDistance);
                            enemy.agentObj.transform.rotation = Quaternion.LookRotation(dir);
                        }

                    }
                    if (enemy.moveCounter == enemy.notesToMove) { enemy.moveCounter = 0; }
                    break;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


                default:
                    break;
            }
        }

    }
}