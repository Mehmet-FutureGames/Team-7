using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Enemy[] enemy;

    bool hasSpawnedPattern = false;

    Transform enemyContainer;

    [SerializeField] List<GameObject> enemyVariants;

    [SerializeField] List<Transform> spawnPointPatterns;

    [Space]

    [SerializeField] TypeOfEnemy[] spawnPoints;
    [Space]
    [SerializeField] int amountofEnemiesWanted;
    [Space]
    [SerializeField] int floorLevel = 0;
    int waveLevel = 0;
    [Space]
    [SerializeField] int numberOfWaves = 0;
    [Space]
    [SerializeField] int waveMaximum = 0;
    [Space]
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
            Instantiate(enemyVariants[spawnPoints[i].ReturnEnemyType()], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
            amountOfEnemies++;
        }    
    }
    #endregion


    private void SpawnPointPattern()
    {
        int randomPattern = UnityEngine.Random.Range(0, spawnPointPatterns.Count);
        if (!hasSpawnedPattern)
        {
            Instantiate(spawnPointPatterns[randomPattern], transform.position, Quaternion.identity, transform);
            FindSpawnPoints();
        }            
            amountofEnemiesWanted = spawnPoints.Length;
            BeginWave();
        hasSpawnedPattern = true;
    }

    public int ProgressWave()
    {
        waveMaximum = waveLevel - 1;
        return waveLevel++;
    }
    private void FinishFloor()
    {
        if(waveLevel >= numberOfWaves)
        {
            //Add behaviour for what happens when you finish a level.
            floorLevel++;
        }
    }

    private void DestroySpawnPoints()
    {
        
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Destroy(spawnPoints[i].gameObject);
        }
        
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
            DestroySpawnPoints();
            ProgressWave();
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
