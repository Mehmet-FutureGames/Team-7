using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    public PlayerStats stats;

    [SerializeField] int currentSelectedCharacter;

    [SerializeField] Text playerHealth;

    [SerializeField] Text playerDamage;

    [SerializeField] Text playerFrenzy;

    [SerializeField] Text playerName;
    private void OnEnable()
    {
        PlayerPrefs.SetInt("selectedCharacter", currentSelectedCharacter);

        playerName.text = stats.playerName;

        playerHealth.text = stats.health.ToString();

        playerDamage.text = stats.attackDamage.ToString();

        playerFrenzy.text = stats.maxFrenzy.ToString();
    }

    public void UpdateText()
    {
        playerHealth.text = stats.health.ToString();

        playerDamage.text = stats.attackDamage.ToString();

        playerFrenzy.text = stats.maxFrenzy.ToString();

        playerName.text = stats.playerName;
    }
}
