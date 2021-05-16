using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnm : MonoBehaviour
{
    MovePlayer movePlayer;
    Animator anim;
    Player player;
    bool hasAttacked;
    public static PlayerAnm Instance;
    private int attackNumber;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        movePlayer = FindObjectOfType<MovePlayer>();
        anim = GetComponent<Animator>();
    }

    public void BlockTrigger()
    {
        anim.SetTrigger("TriggerBlock");
    }

    public void DashTrigger()
    {
        anim.SetTrigger("TriggerDash");
    }

    public void AttackTrigger()
    {       
        anim.SetTrigger("TriggerAttack");
        attackNumber++;
        if (attackNumber > 2)
            attackNumber = 0;
        anim.SetInteger("AttackNumber", attackNumber);
    }
    public void StartAttacking() 
    {
        player.StartAttacking();
    }
    public void StopAttacking()
    {
        player.StopAttacking();
        player.isAttacking = false;
    }

    private void Update()
    {
        anim.SetFloat("MovementSpeed", movePlayer.MovementValue);
    }
}
