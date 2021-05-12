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

    GameObject deadScreen;
    Image healthBar;

    MovePlayer movePlayer;

    ComboHandler comboHandler;

    private void Start()
    {
        Invoke("Reference", 0.1f);
    }
    private void Update()
    {

    }
    private void Reference()
    {
        playerStats = GetComponentInParent<Player>();
        movePlayer = FindObjectOfType<MovePlayer>();
        StartCoroutine(ReferenceHealth());
        comboHandler = FindObjectOfType<ComboHandler>();
        deadScreen = UIManager.deathScreen;
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    public void Respawn()
    {
        Time.timeScale = 1;
    #if UNITY_ANDROID
        deadScreen.SetActive(false);
    #endif
        RefillHealth();
    }

    public void TakeDamage(float damage)
    {
        if (!PlayerBlock.isBlocking)
        {
            CameraFollowPlayer.Instance.CameraShake();
            comboHandler.SetCombo(0);
            currentHealth -= damage;
            if (movePlayer.MovementValue < 10)
            {
                if (playerStats.playerDamageText)
                {
                    ShowFloatingText(damage);
                    AudioManager.PlaySound("NormalSwings", "PlayerSound");
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
        CameraFollowPlayer.Instance.CameraShake();
        currentHealth -= damage;
        AudioManager.PlaySound("Monster Takes Damage 10","PlayerSound");
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
#if UNITY_ANDROID
        if (!AdsManager.hasWatchedAd)
        {
            UIManager.deadSlider.GetComponent<Image>().fillAmount = 0.5f * 10;
            deadScreen.SetActive(true);
            timerTillAdGone = Time.realtimeSinceStartup - UIManager.timer + 5;
            StartCoroutine(StartTimer());
            Time.timeScale = 0;
            Player.EnemyTransforms.Clear();
        }
        else
        {
            UIManager.gameOverPanel.SetActive(true);
            Time.timeScale = 0;
            Player.EnemyTransforms.Clear();
        }
#endif
#if UNITY_STANDALONE
        Time.timeScale = 0;
        UIManager.deadPanel.SetActive(true);
#endif
    }

    IEnumerator StartTimer()
    {
            while (timerTillAdGone > 0 && !AdsManager.hasWatchedAd)
            {
                timerTillAdGone -= 0.050f;
                UIManager.timerDead.text = timerTillAdGone.ToString("F0");
                UIManager.deadSlider.GetComponent<Image>().fillAmount -= 0.01f;
                if (timerTillAdGone <= 0)
                {
                    PauseMenu.LoadMenu();
                }
            if (AdsManager.startedWatchingAd)
            {
                break;
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
