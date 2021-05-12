using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour
{
    bool hasStartedFirstTime;

    public PlayerStats stats;

    [SerializeField] int currentSelectedCharacter;

    [SerializeField] TextMeshProUGUI playerHealth;

    [SerializeField] TextMeshProUGUI playerDamage;

    [SerializeField] TextMeshProUGUI playerFrenzy;

    [SerializeField] TextMeshProUGUI playerName;

    [SerializeField] TextMeshProUGUI descCharacter;

    [SerializeField] TextMeshProUGUI costOfCharacter;

    [SerializeField] TextMeshProUGUI nameOfCharacter;

    public bool hasBeenBought = false;

    public int notesToPay = 0;

    public int notesFrenzyCost;

    public int notesCostDamage;

    public int notesCostHealth;

    private void Awake()
    {
        hasBeenBought = PlayerPrefs.GetInt("boughtCharacter" + currentSelectedCharacter) == 1;
        hasStartedFirstTime = PlayerPrefs.GetInt("hasStartedFirstTime") == 1;

        notesCostHealth = PlayerPrefs.GetInt("UpgradeHealth" + currentSelectedCharacter);
        notesCostDamage = PlayerPrefs.GetInt("UpgradeDamage" + currentSelectedCharacter);
        notesFrenzyCost = PlayerPrefs.GetInt("UpgradeFrenzy" + currentSelectedCharacter);

        gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        PlayerPrefs.SetInt("selectedCharacter", currentSelectedCharacter);

        hasBeenBought = PlayerPrefs.GetInt("boughtCharacter" + currentSelectedCharacter) == 1;

        playerName.text = stats.playerName;

        playerHealth.text = stats.health.ToString();

        playerDamage.text = stats.attackDamage.ToString();

        playerFrenzy.text = stats.maxFrenzy.ToString();

        descCharacter.text = stats.descriptionCharacter;

        costOfCharacter.text = notesToPay.ToString();

        nameOfCharacter.text = "Purchase: " + stats.playerName;
    }

    public void UpdateText()
    {
        playerHealth.text = stats.health.ToString();

        playerDamage.text = stats.attackDamage.ToString();

        playerFrenzy.text = stats.maxFrenzy.ToString();

        playerName.text = stats.playerName;
    }
}
