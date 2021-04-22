using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterStats : MonoBehaviour
{
    [SerializeField] PlayerStats stats;

    [SerializeField] Text playerHealth;

    [SerializeField] Text playerDamage;

    [SerializeField] Text playerFrenzy;

    private void Start()
    {
        playerHealth.text = stats.health.ToString();

        playerDamage.text = stats.attackDamage.ToString();

        playerFrenzy.text = stats.maxFrenzy.ToString();
    }


    public void UpgradeStats()
    {
        if(gameObject.activeInHierarchy == true)
        {

        }
    }
}
