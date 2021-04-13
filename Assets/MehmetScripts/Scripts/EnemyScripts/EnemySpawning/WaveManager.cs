using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
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
       InvokeRepeating("EnemySpawner", 0, 0.1f);

        amountOfEnemiesOnMap = GameObject.FindGameObjectsWithTag("Enemy").Length;
        enemyContainer = GameObject.Find("EnemyContainer").transform;

        spawnPoints = FindObjectsOfType<TypeOfEnemy>();
    }

    private void EnemySpawner()
    {
        if (amountofEnemiesWanted > amountOfEnemies)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                switch (spawnPoints[i].ReturnEnemyType())
                {
                    default:
                        break;
                    case 1:
                        Instantiate(enemyVariants[0], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                        amountOfEnemies++;
                        break;
                    case 2:
                        Instantiate(enemyVariants[1], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                        amountOfEnemies++;
                        break;
                }
            }
        }
        else
        {
            Debug.Log(amountofEnemiesWanted + " has been spawned!");
            CancelInvoke();
        }
    }

    public void FindSpawnPoints()
    {
        spawnPoints = FindObjectsOfType<TypeOfEnemy>();
    }
}
