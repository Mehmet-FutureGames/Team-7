using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    Player playerStats;

    float health;

    [SerializeField] GameObject deadPanel;

    private void Start()
    {
        playerStats = GetComponentInParent<Player>();

        StartCoroutine(ReferenceHealth());

        deadPanel.SetActive(false);
        if(Time.timeScale < 1)
        {
            Time.timeScale = 1;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        Debug.Log(health);
        if(health < 0)
        {
            Dead();
        }
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
