using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    NotePublisher publisher;
    
    bool collided;

    public bool isMoving;
    
    private CharacterManager characterManager;


    float moveSpeedModifier;
    float moveSpeedMultiplier;


    ///////////////////////////////////----  Raycast  -----/////////////////////////////////////////////////////////
    Vector3 newPosition;
    Vector3 mousePos;
    Vector3 raycastDir;
    float raycastDistance;
    bool hitWall;
    Vector3 hitWallPos;
    [SerializeField] LayerMask layer;

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        mousePos = transform.position;
        publisher = FindObjectOfType<NotePublisher>();
        publisher.noteHit += MovePlayerToMousePos;
    }

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        moveSpeedModifier = characterManager.playerMovementSpeedModifier;
        moveSpeedMultiplier = characterManager.playerMovementSpeedMultiplier;
    }
    private void Update()
    {
        
        if (!collided && !hitWall)
        {
            float distance = (transform.position - mousePos).magnitude;

            ////  This will most likely be used to get the current speed the player is moving. 
            // Example: if(value > 0){ doingDamageIsPossible }
            float MovementValue = (distance* moveSpeedMultiplier * Time.deltaTime) * 10;
            if(MovementValue > Mathf.Epsilon)
            {
                isMoving = true;
            }
            else { isMoving = false; }
            
            ////

            float modifier = (distance + moveSpeedModifier) * moveSpeedMultiplier * Time.deltaTime;
            // move the player to mouse position
            transform.position = Vector3.MoveTowards(transform.position, mousePos, modifier);
        }
        else if(!collided && hitWall)
        {
            
            // if there's a wall between the player and the mouse position, make the player move to the normal point of the wall.
            float distance = (transform.position - hitWallPos).magnitude;
            float modifier = (distance + moveSpeedModifier) * moveSpeedMultiplier * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, hitWallPos, modifier);
        }
    }

    void MovePlayerToMousePos()
    {
        ///////////////////////////////////////////////////////////////////////////
        //Move the Player to Mouse pos.
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, characterManager.LayerToMovement))
        {
            hitWall = false;
            newPosition = hit.point;
            mousePos = newPosition + new Vector3(0, 1, 0);
            raycastDir = (mousePos - transform.position).normalized;
            raycastDistance = (mousePos - transform.position).magnitude;
        }

        /////////////////////////////////////////////////////////////////////////////
        //Move the player to normal point position.
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, raycastDir, out hit2, raycastDistance, layer))
        {
            hitWall = true;
            Vector3 point = new Vector3(hit2.point.x, 1, hit2.point.z);
            Vector3 pointToNormalPos = new Vector3(hit2.normal.x, 0, hit2.normal.z) + point;
            hitWallPos = pointToNormalPos;
        }
        //////////////////////////////////////////////////////////////////////////////
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
            transform.LookAt(hitWallPos);
        }
        else
        {
            transform.LookAt(mousePos);
        }
    }
}