using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFrenzy : MonoBehaviour
{
    EnemyPublisher enemyPublisher;
    int maxFrenzy;
    int minFrenzy;
    int currentFrenzy;

    private void Awake()
    {
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        enemyPublisher.enemyTakeDamage += AddFrenzy;
    }

    void AddFrenzy()
    {
        currentFrenzy += 1;
    }


}
