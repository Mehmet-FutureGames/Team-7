using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Player playerStats;

    float health;

    GameObject deadPanel;

    MovePlayer movePlayer;

    private void Start()
    {
        playerStats = GetComponentInParent<Player>();
        movePlayer = FindObjectOfType<MovePlayer>();
        StartCoroutine(ReferenceHealth());

        deadPanel = GameObject.Find("DeadPanel");
        deadPanel.SetActive(false);
        if(Time.timeScale < 1 && health > 0)
        {
            Time.timeScale = 1;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if(movePlayer.MovementValue < 10) 
        {
            if (playerStats.playerDamageText)
            {
                ShowFloatingText(damage);
            }
            if (health < 0)
            {
                Dead();
            }
        }


    }

    private void ShowFloatingText(float damage)
    {
        var text = Instantiate(playerStats.playerDamageText, transform.position, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = "Damage: " + damage.ToString();
    }

    private void Dead()
    {
        deadPanel.SetActive(true);
        Time.timeScale = 0;
    }

    IEnumerator ReferenceHealth()
    {
        yield return new WaitForSeconds(0.1f);
        health = playerStats.health;
    }
}
