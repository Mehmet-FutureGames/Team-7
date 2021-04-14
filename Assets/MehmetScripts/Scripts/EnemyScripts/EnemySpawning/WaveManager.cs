using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Enemy[] enemy;


    Transform enemyContainer;

    [SerializeField] List<GameObject> enemyVariants;

    [Space]

    [SerializeField] TypeOfEnemy[] spawnPoints;
    [Space]
    [SerializeField] int amountofEnemiesWanted;
    [Space]
    [SerializeField] int amountOfEnemiesOnMapWished = 0;
    [Space]
    [SerializeField] int floorLevel = 0;
    [Space]
    [SerializeField] int waveLevel = 0;

    int amountOfEnemiesOnMap = 0;

    [SerializeField] int amountOfEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("BeginFirstWave", 0, 0.1f);

        enemyContainer = GameObject.Find("EnemyContainer").transform;

        spawnPoints = FindObjectsOfType<TypeOfEnemy>();

        StartCoroutine(ReferenceEnemies());

    }

    void Update()
    {
        if(amountOfEnemies <= 0)
        {
            InvokeRepeating("BeginFirstWave", 0, 0.1f);
        }
    }
    private void BeginFirstWave()
    {
        //Goes through all of the spawn points
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (amountofEnemiesWanted > amountOfEnemies)
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
            else
            {
                //Stops the first wave
                CancelInvoke();
            }
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
    }
    IEnumerator ReferenceEnemies()
    {
        yield return new WaitForSeconds(1f);
        enemy = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemy.Length; i++)
        {
            enemy[i].enemyDefeated += EnemyDefeated;
        }
        
    }
}
