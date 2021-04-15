using UnityEngine;

public class MoveState : State
{
    
    public MoveState(Enemy enemy, StateMachine stateMachine) : base(enemy, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
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
            stateMachine.ChangeState(enemy.idleState);
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
            Vector3 dirToPlayer;
            Vector3 randomVector;
            Vector3 agentToRandom;
            Vector3 dir;
            switch (enemy.movePattern)
            {
                case MovePattern.TowardsPlayer:
                    if (enemy.moveCounter == enemy.notesToMove)
                    {
                        dirToPlayer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
                        if (enemy.distanceToPlayer < enemy.moveDistance)
                        {
                            enemy.agent.SetDestination(enemy.player.position);
                        }
                        else
                        {
                            enemy.agent.SetDestination(enemy.agentObj.transform.position + dirToPlayer * enemy.moveDistance);
                        }
                        enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);

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
                    dirToPlayer = (enemy.player.position - enemy.agentObj.transform.position).normalized;
                    if (enemy.moveCounter == enemy.notesToMove)
                    {
                        
                        if (enemy.distanceToPlayer <= enemy.detectionRange)
                        {
                            enemy.agent.SetDestination(enemy.agentObj.transform.position + dirToPlayer * enemy.moveDistance);
                            enemy.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);
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
