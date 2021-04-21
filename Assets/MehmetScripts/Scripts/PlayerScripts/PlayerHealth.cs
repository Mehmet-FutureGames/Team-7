using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Player playerStats;
    float currentHealth;

    GameObject deadPanel;

    MovePlayer movePlayer;

    private void Start()
    {
        playerStats = GetComponentInParent<Player>();
        movePlayer = FindObjectOfType<MovePlayer>();
        StartCoroutine(ReferenceHealth());

        deadPanel = GameObject.Find("DeadPanel");
        deadPanel.SetActive(false);
        RespawnDEV();
    }

    private void RespawnDEV()
    {
        if (Time.timeScale < 1 && currentHealth > 0)
        {
            Time.timeScale = 1;
        }
    }

    public void TakeDamage(float damage)
    {
        if (!PlayerBlock.isBlocking)
        {
            currentHealth -= damage;
            if (movePlayer.MovementValue < 10)
            {
                if (playerStats.playerDamageText)
                {
                    ShowFloatingText(damage);
                }
                if (currentHealth < 0)
                {
                    Dead();
                }
            }
        }
    }
    public void TakeRangedDamage(float damage)
    {
        currentHealth -= damage;
        if (movePlayer.MovementValue < 10)
        {
            if (playerStats.playerDamageText)
            {
                ShowFloatingText(damage);
            }
            if (currentHealth < 0)
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
        currentHealth = playerStats.health;
    }

    public void UpgradeHealth(float upgradedHealth)
    {
        currentHealth += upgradedHealth;
    }
}
