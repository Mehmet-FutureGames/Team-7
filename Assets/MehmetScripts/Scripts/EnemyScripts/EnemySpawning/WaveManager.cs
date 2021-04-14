using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Enemy[] character;


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

        StartCoroutine(ReferenceStuff());

    }
    private void BeginFirstWave()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (amountofEnemiesWanted > amountOfEnemies)
            {
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
                Debug.Log(amountofEnemiesWanted + " has been spawned!");
                CancelInvoke();
            }
        }
    
    }

    public void FindSpawnPoints()
    {
        spawnPoints = FindObjectsOfType<TypeOfEnemy>();
    }

    public void EnemyDefeated()
    {
        amountOfEnemies--;
    }
    IEnumerator ReferenceStuff()
    {
        yield return new WaitForSeconds(1f);
        character = FindObjectsOfType<Enemy>();
        Debug.Log(character.Length);
        for (int i = 0; i < character.Length; i++)
        {
            character[i].enemyDefeated += EnemyDefeated;
        }
        
    }
}
