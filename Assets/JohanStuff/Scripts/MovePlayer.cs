using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    NotePublisher publisher;
    
    bool collided;
    
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
            transform.position = Vector3.MoveTowards(transform.position, mousePos, (distance + moveSpeedModifier) * moveSpeedMultiplier * Time.deltaTime);
        }
        else if(!collided && hitWall)
        {
            float distance = (transform.position - hitWallPos).magnitude;
            transform.position = Vector3.MoveTowards(transform.position, hitWallPos, (distance + moveSpeedModifier) * moveSpeedMultiplier * Time.deltaTime);
        }
    }

    void MovePlayerToMousePos()
    {
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
        RaycastHit hit2;
        if (Physics.Raycast(transform.position, raycastDir, out hit2, raycastDistance, layer))
        {
            hitWall = true;
            Vector3 point = new Vector3(hit2.point.x, 1, hit2.point.z);
            Vector3 pointToNormalPos = new Vector3(hit2.normal.x, 0, hit2.normal.z) + point;
            hitWallPos = pointToNormalPos;
        }
            collided = false;
    }
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Collision");
    }


    private void OnCollisionStay(Collision other)
    {

    }

}
