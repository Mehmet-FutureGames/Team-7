using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrenzy : MonoBehaviour
{
    PlayerStats stats;
    EnemyPublisher enemyPublisher;
    [SerializeField] Text text;
    public int maxFrenzy;
    [SerializeField] int minFrenzy;
    [SerializeField] private int currentFrenzy;

    public int CurrentFrenzy
    {
        get { return currentFrenzy; }
        set 
        {
            currentFrenzy = value;
            text.text = "Frenzy: " + currentFrenzy.ToString();
        }
    }

    private void Awake()
    {
        stats = GetComponent<Player>().stats;
        maxFrenzy = (int)stats.maxFrenzy;
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        enemyPublisher.enemyTakeDamage += AddFrenzy;
        currentFrenzy = 0;
    }

    void AddFrenzy()
    {
        currentFrenzy = Mathf.Clamp(currentFrenzy + 1, minFrenzy, maxFrenzy);
        
        text.text = "Frenzy: " + currentFrenzy.ToString();
    }


}
