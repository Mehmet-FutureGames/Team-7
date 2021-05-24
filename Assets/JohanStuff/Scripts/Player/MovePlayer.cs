using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MovePlayer : MonoBehaviour
{
    MoveToPointByDistance moveToPointByDistance;

    NotePublisher publisher;

    public AudioClip movingsound;

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
    Vector3 lookDir;
    [HideInInspector] public Vector3 mousePos;
    Vector3 raycastDir;
    float raycastDistance;
    bool hitWall;
    [SerializeField] LayerMask obstaclLayer;
    Coroutine lastRoutine = null;
    Player player;

    Camera camera;
    bool isInShop;
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        mousePos = transform.position;
        publisher = FindObjectOfType<NotePublisher>();
        moveToPointByDistance = new MoveToPointByDistance();
        player = GetComponent<Player>();
        camera = Camera.main;
    }

    private void Start()
    {
        characterManager = FindObjectOfType<CharacterManager>();
        moveSpeedModifier = characterManager.playerMovementSpeedModifier;
        moveSpeedMultiplier = characterManager.playerMovementSpeedMultiplier;
    }
    private void Update()
    {
        if (isInShop)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (OverGUICheck.Instance.IsPointerOverUIObject())
                {

                }
                else 
                {
                    MovePlayerToMousePos();
                }
                
            }
        }
    }
    private void MoveCharacter()
    {
        transform.position = moveToPointByDistance.Move(transform.position, mousePos, (moveSpeedMultiplier + moveSpeedModifier) * Time.deltaTime, ref MovementValue);
        if (MovementValue > Mathf.Epsilon)
        {

            gameObject.GetComponent<NavMeshObstacle>().enabled = false;
        }
        else { gameObject.GetComponent<NavMeshObstacle>().enabled = true; }
    }
    private void DelayMove()
    {
        if (OverGUICheck.Instance.IsPointerOverUIObject())
        {

        }
        else { Invoke("MovePlayerToMousePos", 0.001f); }
        
    }

    private void MovePlayerToMousePos()
    {
        ///////////////////////////////////////////////////////////////////////////
        //Move the Player to Mouse pos.
        ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out this.hit, Mathf.Infinity, characterManager.LayerToMovement))
        {
            hitWall = false;
            newPosition = this.hit.point;
            mousePos = newPosition;
            lookDir = newPosition + new Vector3(0, 1, 0);
            raycastDir = (mousePos - transform.position).normalized;
            raycastDistance = (mousePos - transform.position).magnitude;
        }
        /////////////////////////////////////////////////////////////////////////////
        //Move the player to normal point position.
        //RaycastHit hit;
        if (Physics.Raycast(transform.position, raycastDir, out hit, raycastDistance, obstaclLayer))
        {
            hitWall = true;
            Vector3 point = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            Vector3 pointToNormalPos = new Vector3(hit.normal.x, hit.normal.y, hit.normal.z) + point;
            mousePos = pointToNormalPos;
        }
        if(!hitWall && TargetEnemy.hasTarget)
        {
            mousePos = TargetEnemy.stopPos;
        }
        //////////////////////////////////////////////////////////////////////////////
        TurnPlayerTowardsDir();
        SendPlayerRegMove();
        if(lastRoutine != null)
        {
            StopCoroutine(lastRoutine);
        }
        lastRoutine = StartCoroutine(Move());
        
        
    }
    void SendPlayerRegMove()
    {
        if (playerRegMove != null)
        {
            playerRegMove();
        }
    }
    void TurnPlayerTowardsDir()
    {
        if (!player.isAttacking)
        {
            transform.LookAt(new Vector3(lookDir.x, transform.position.y, lookDir.z));
        }
    }
    IEnumerator Move()
    {
        isMoving = true;
        float distance = (transform.position - mousePos).magnitude;
        while (distance > 0.08f)
        {
            distance = (transform.position - mousePos).magnitude;
            MoveCharacter();
            yield return new WaitForEndOfFrame();
        }
        isMoving = false;

        yield return null;
    }
    private void OnLevelWasLoaded(int level)
    {
        if (publisher.noteHit != null)
        {
            publisher.noteHit -= DelayMove;
        }
        if (level == SceneManager.GetSceneByName("Shop").buildIndex || level == SceneManager.GetSceneByName("CoinShop").buildIndex)
        {
            isInShop = true;
        }
        else
        {
            isInShop = false;
            publisher.noteHit += DelayMove;
        }

        
        MovementValue = 0;
        player = GetComponent<Player>();
    }
}

