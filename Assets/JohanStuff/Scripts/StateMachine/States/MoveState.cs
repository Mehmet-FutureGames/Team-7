using UnityEngine;

public class MoveState : State
{
    
    public MoveState(Character character, StateMachine stateMachine) : base(character, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.gameObject.GetComponentInChildren<MeshRenderer>().material.color = Color.yellow;
    }


    public override void Exit()
    {
        base.Exit();
    }

    public override void NoteEventUpdate()
    {
        base.NoteEventUpdate();
        
        if (character.distanceToPlayer < character.attackRange)
        {   
            stateMachine.ChangeState(character.chargeAttack);
            return;
        }
        Action();
    }

    public override void Action()
    {
        base.Action();
        character.moveCounter++;
        if (character.moveCounter == character.notesToMove)
        {
            switch (character.movePattern)
            {
                case MovePattern.TowardsPlayer:
                    if (character.moveCounter == character.notesToMove)
                    {
                        Vector3 dirToPlayer = (character.player.position - character.agentObj.transform.position).normalized;
                        float distance = (character.player.position - character.agentObj.transform.position).magnitude;

                        if (distance < character.moveDistance + 1)
                        {
                            character.agent.SetDestination(character.player.position);
                            Vector3 corner = character.agent.steeringTarget;
                            character.agent.SetDestination(corner);
                        }
                        else
                        {
                            character.agent.SetDestination(character.agentObj.transform.position + dirToPlayer * character.moveDistance);
                        }
                        character.agentObj.transform.rotation = Quaternion.LookRotation(dirToPlayer);

                    }
                    if (character.moveCounter == character.notesToMove) { character.moveCounter = 0; }
                    break;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                case MovePattern.RandomDirection:
                    
                    Vector3 randomVector = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));
                    Vector3 agentToRandom = character.agentObj.transform.position + randomVector;
                    Vector3 dir = (agentToRandom - character.agentObj.transform.position).normalized;
                    character.agent.SetDestination(character.agentObj.transform.position + dir * character.moveDistance);

                    if (character.agent.velocity.sqrMagnitude > Mathf.Epsilon)
                    {
                        character.agentObj.transform.rotation = Quaternion.LookRotation(dir);
                    }
                    if (character.moveCounter == character.notesToMove) { character.moveCounter = 0; }
                    break;
                ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                case MovePattern.ProximityDetection:

                    break;


                default:
                    break;
            }
        }

    }
}
