using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrenzy : MonoBehaviour
{
    EnemyPublisher enemyPublisher;
    [SerializeField] Text text;
    [SerializeField]int maxFrenzy;
    [SerializeField]int minFrenzy;
    int currentFrenzy;

    private void Awake()
    {
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        enemyPublisher.enemyTakeDamage += AddFrenzy;
    }

    void AddFrenzy()
    {
        currentFrenzy = Mathf.Clamp(currentFrenzy + 1, minFrenzy, maxFrenzy);
        
        text.text = "Frenzy: " + currentFrenzy.ToString();
    }


}
