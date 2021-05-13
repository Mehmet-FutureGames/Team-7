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
    private void Reference()
    {
        playerStats = GetComponentInParent<Player>();
        movePlayer = FindObjectOfType<MovePlayer>();
        StartCoroutine(ReferenceHealth());
        comboHandler = FindObjectOfType<ComboHandler>();
#if UNITY_STANDALONE
        deadScreen = UIManager.deadPanelPC;
#endif
#if UNITY_ANDROID
deadScreen = UIManager.gameOverPanel;
#endif
        healthBar = GameObject.Find("HealthBar").GetComponent<Image>();
    }

    public void Respawn()
    {
        Time.timeScale = 0;
#if UNITY_ANDROID
        deadScreen.SetActive(false);
#endif
        RefillHealth();
    }


    #region TakeDamage
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
    public void TakeDamage(float damage, string damageSound)
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
                    if(damageSound == null || damageSound == "")
                    {
                        AudioManager.PlaySound("NormalSwings", "PlayerSound");
                    }
                    else
                    {
                        AudioManager.PlaySound(damageSound, "PlayerSound");
                    }
                    
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
        AudioManager.PlaySound("Monster Takes Damage 10", "PlayerSound");
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
    public void TakeUnblockableDamage(float damage, string damageSound)
    {
        CameraFollowPlayer.Instance.CameraShake();
        currentHealth -= damage;
        if (damageSound == null || damageSound == "")
        {
            AudioManager.PlaySound("Monster Takes Damage 10", "PlayerSound");
        }
        else
        {
            AudioManager.PlaySound(damageSound, "PlayerSound");
        }
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
    #endregion
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
            UIManager.noRetryScreen.SetActive(true);
            Time.timeScale = 0;
            Player.EnemyTransforms.Clear();
        }
#endif
#if UNITY_STANDALONE
        if (!UIManager.hasRestartedPC)
        {
            Time.timeScale = 0;
            UIManager.deadPanelPC.SetActive(true);
            UIManager.deadSlider.GetComponent<Image>().fillAmount = 0.5f * 10;
            deadScreen.SetActive(true);
            timerTillAdGone = Time.realtimeSinceStartup - UIManager.timer + 5;
            StartCoroutine(StartTimerPC());
        }
        else
        {
            UIManager.noRetryScreen.SetActive(true);
            Time.timeScale = 0;
            Player.EnemyTransforms.Clear();
        }
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
    IEnumerator StartTimerPC()
    {
        while (timerTillAdGone > 0 && !AdsManager.hasWatchedAd)
        {
            timerTillAdGone -= 0.050f;
            UIManager.sliderTimerTextPC.text = timerTillAdGone.ToString("F0");
            UIManager.timerOverSliderPC.GetComponent<Image>().fillAmount -= 0.01f;
            if (timerTillAdGone <= 0)
            {
                PauseMenu.LoadMenu();
            }
            if (UIManager.hasRestartedPC)
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
