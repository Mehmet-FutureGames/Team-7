using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    Player playerStats;
    [HideInInspector] public float currentHealth;
    float defaultMaxHealth = 100f;
    float timerTillAdGone;
    bool timeDone = false;

    GameObject deadScreen;
    Image healthBar;

    MovePlayer movePlayer;

    ComboHandler comboHandler;

    private void Start()
    {
        playerStats = GetComponentInParent<Player>();
        movePlayer = FindObjectOfType<MovePlayer>();
        StartCoroutine(ReferenceHealth());
        comboHandler = FindObjectOfType<ComboHandler>();
        deadScreen = UIManager.deathScreen;
        Debug.Log(deadScreen);
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
        Respawn();
    }
    private void Update()
    {

    }

    public void Respawn()
    {
        Time.timeScale = 1;
        deadScreen.SetActive(false);
        RefillHealth();
    }

    public void TakeDamage(float damage)
    {
        if (!PlayerBlock.isBlocking)
        {
            comboHandler.SetCombo(0);
            currentHealth -= damage;
            if (movePlayer.MovementValue < 10)
            {
                if (playerStats.playerDamageText)
                {
                    ShowFloatingText(damage);
                }
                if (currentHealth <= 0)
                {
                    Dead();
                }
            }
        }
        else
        {
            comboHandler.AddToCombo();
        }
    }
    public void TakeUnblockableDamage(float damage)
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
        healthBar.fillAmount = currentHealth / playerStats.maxHealth;
    }

    private void Dead()
    {
        UIManager.deadSlider.GetComponent<Image>().fillAmount = 0.5f * 10;
        deadScreen.SetActive(true);
        timerTillAdGone = Time.realtimeSinceStartup - UIManager.timer + 5;
        StartCoroutine(StartTimer());
        Time.timeScale = 0;
        Player.EnemyTransforms.Clear();        
    }

    IEnumerator StartTimer()
    {
        while (timerTillAdGone > 0)
        {
            timerTillAdGone -= 0.050f;
            UIManager.timerDead.text = timerTillAdGone.ToString("F0");
            UIManager.deadSlider.GetComponent<Image>().fillAmount -= 0.01f;
            if (timerTillAdGone <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    IEnumerator ReferenceHealth()
    {
        yield return new WaitForSeconds(0.1f);
        currentHealth = playerStats.maxHealth;
        healthBar.transform.parent.localScale = new Vector2(healthBar.transform.parent.localScale.x * (playerStats.maxHealth / defaultMaxHealth), healthBar.transform.parent.localScale.y);
        healthBar.fillAmount = currentHealth / playerStats.maxHealth;
    }
    private void RefillHealth()
    {
        healthBar.fillAmount = currentHealth / playerStats.maxHealth;
    }

    public void UpgradeHealth(float upgradedHealth)
    {
        playerStats.maxHealth += upgradedHealth;
        GetComponent<PlayerHealth>().currentHealth = playerStats.maxHealth;
        healthBar.transform.parent.localScale = new Vector2(healthBar.transform.parent.localScale.x * (playerStats.maxHealth / defaultMaxHealth), healthBar.transform.parent.localScale.y);
        healthBar.fillAmount = currentHealth / playerStats.maxHealth;
        RefillHealth();
    }

    public void RecoverHealth(float healthRecovered)
    {
        currentHealth += healthRecovered;
        if(currentHealth > playerStats.maxHealth)
        {
            currentHealth = playerStats.maxHealth;
        }
        healthBar.fillAmount = currentHealth / playerStats.maxHealth;
    }
}
