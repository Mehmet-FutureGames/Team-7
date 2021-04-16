using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Enemy[] enemy;

    bool hasSpawnedPattern = false;

    bool hasCompletedWave = false;

    Transform enemyContainer;

    [SerializeField] List<GameObject> enemyVariants;

    [SerializeField] List<Transform> spawnPointPatterns;

    [Space]

    [SerializeField] TypeOfEnemy[] spawnPoints;
    [Space]
    [SerializeField] int amountofEnemiesWanted;
    [Space]
    [SerializeField] int floorLevel = 0;
    [Space]
    [SerializeField] int waveLevel = 0;

    [SerializeField] int amountOfEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpawnPointPattern();

        enemyContainer = GameObject.Find("EnemyContainer").transform;

        spawnPoints = FindObjectsOfType<TypeOfEnemy>();

        waveLevel++;

    }
    #region BeginWaveSpawnEnemies
    private void BeginWave()
    {
        //Goes through all of the spawn points
        for (int i = 0; i < spawnPoints.Length; i++)
        {
                //Goes through each spawn points and checks which enemy 
                //should be spawned at that specific spawnpoint
                switch (spawnPoints[i].ReturnEnemyType())
                {
                    case 1:
                        Instantiate(enemyVariants[0], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                        amountOfEnemies++;
                        break;
                    case 2:
                        Instantiate(enemyVariants[1], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                        amountOfEnemies++;
                        break;
                    case 3:
                        Instantiate(enemyVariants[2], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                        amountOfEnemies++;
                        break;
                }
        }    
    }
    #endregion


    private void SpawnPointPattern()
    {
            for (int i = 0; i < spawnPointPatterns.Count; i++)
            {
                if (!hasSpawnedPattern)
                {
                Instantiate(spawnPointPatterns[i], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
                }
            }
            amountofEnemiesWanted = spawnPoints.Length;
            BeginWave();
        hasSpawnedPattern = true;
    }

    public void FindSpawnPoints()
    {
        //Checks for new spawnpoints
        spawnPoints = FindObjectsOfType<TypeOfEnemy>();
    }

    public void EnemyDefeated()
    {
        amountOfEnemies--;
        if (amountOfEnemies < 3)
        {
            SpawnPointPattern();
        }
    }
    public void Subscribe(Enemy enemy)
    {
        enemy.enemyDefeated += EnemyDefeated;
    }
    public void UnSubscribe(Enemy enemy) 
    {
        enemy.enemyDefeated -= EnemyDefeated;
    }


}
