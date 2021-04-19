using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovePlayer : MonoBehaviour
{
    NotePublisher publisher;
    
    bool collided;

    public bool isMoving;

    public Action playerRegMove;
    
    private CharacterManager characterManager;


    float moveSpeedModifier;
    float moveSpeedMultiplier;

    float distance;
    public float MovementValue;
    ///////////////////////////////////----  Raycast  -----/////////////////////////////////////////////////////////
    public RaycastHit hit;
    public Ray ray;
    Vector3 newPosition;
    Vector3 mousePos;
    Vector3 raycastDir;
    float raycastDistance;
    bool hitWall;
    [SerializeField] LayerMask layer;

    Player player;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        mousePos = transform.position;
        publisher = FindObjectOfType<NotePublisher>();
        publisher.noteHit += MovePlayerToMousePos1;
        player = GetComponent<Player>();
    }

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        moveSpeedModifier = characterManager.playerMovementSpeedModifier;
        moveSpeedMultiplier = characterManager.playerMovementSpeedMultiplier;
    }
    private void Update()
    {
        //MoveCharacter();
    }

    private void MoveCharacter()
    {
        if (!player.isAttacking)
        {
            if (!collided && !hitWall)
            {
                distance = (transform.position - mousePos).magnitude;

                ////  This will most likely be used to get the current speed the player is moving. 
                // Example: if(value > 0){ doingDamageIsPossible }
                MovementValue = (distance * moveSpeedMultiplier * Time.deltaTime) * 10;
                if (MovementValue > Mathf.Epsilon)
                {

                    gameObject.GetComponent<NavMeshObstacle>().enabled = false;
                }
                else { gameObject.GetComponent<NavMeshObstacle>().enabled = true; }

                ////

                float modifier = (distance + moveSpeedModifier) * moveSpeedMultiplier * Time.deltaTime;
                // move the player to mouse position
                transform.position = Vector3.MoveTowards(transform.position, mousePos, modifier);
            }
            else if (!collided && hitWall)
            {

                // if there's a wall between the player and the mouse position, make the player move to the normal point of the wall.
                distance = (transform.position - mousePos).magnitude;
                float modifier = (distance + moveSpeedModifier) * moveSpeedMultiplier * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, mousePos, modifier);
            }
        }
    }
    private void MovePlayerToMousePos1()
    {
        Invoke("MovePlayerToMousePos", 0.001f);
    }

    private void MovePlayerToMousePos()
    {
        if (!player.isAttacking)
        {
            ///////////////////////////////////////////////////////////////////////////
            //Move the Player to Mouse pos.
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out this.hit, Mathf.Infinity, characterManager.LayerToMovement))
            {
                hitWall = false;
                newPosition = this.hit.point;
                mousePos = newPosition + new Vector3(0, 1, 0);
                raycastDir = (mousePos - transform.position).normalized;
                raycastDistance = (mousePos - transform.position).magnitude;
            }
        }

            /////////////////////////////////////////////////////////////////////////////
            //Move the player to normal point position.
            //RaycastHit hit;
            if (Physics.Raycast(transform.position, raycastDir, out hit, raycastDistance, layer))
            {
                hitWall = true;
                Vector3 point = new Vector3(hit.point.x, 1, hit.point.z);
                Vector3 pointToNormalPos = new Vector3(hit.normal.x, 0, hit.normal.z) + point;
                mousePos = pointToNormalPos;
            }
            //////////////////////////////////////////////////////////////////////////////
            StartCoroutine(Move());

            if (playerRegMove != null)
            {
                playerRegMove();
            }
            collided = false;
        
            TurnPlayerTowardsDir();
        
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
    }


    private void OnCollisionStay(Collision other)
    {

    }
    void TurnPlayerTowardsDir()
    {
        if (hitWall)
        {
            transform.LookAt(mousePos);
        }
        else
        {
            transform.LookAt(mousePos);
        }
    }
    IEnumerator Move()
    {
        if (!player.isAttacking)
        {
            isMoving = true;
            while (transform.position != mousePos)
            {
                MoveCharacter();
                yield return new WaitForEndOfFrame();
            }
            isMoving = false;

            yield return null;
        }
    }
}

