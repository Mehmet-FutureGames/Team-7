using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Enemy[] enemy;

    bool hasSpawnedPattern = false;

    int randomPattern;

    Transform enemyContainer;

    [SerializeField] List<GameObject> enemyVariants;
    [Space]

    [SerializeField] List<Transform> spawnPointPatterns;
    [Space]

    [SerializeField] List<Transform> spawnPointPatternsMedium;
    [Space]

    [SerializeField] List<Transform> spawnPointPatternsHard;
    [Space]

    [SerializeField] TypeOfEnemy[] spawnPoints;
    [Space]
    [SerializeField] int amountofEnemiesWanted;
    [Space]
    [SerializeField] int floorLevel = 0;
    [Space]
    [SerializeField]int waveLevel = 0;
    [Space]
    [SerializeField] int numberOfWavesForEasyLevel = 0;
    [Space]
    [SerializeField] int numberOfWavesForMediumLevel = 0;
    [Space]
    [SerializeField] int numberOfWavesForHardLevel = 0;
    [Space]
    [SerializeField] int waveMaximum = 0;
    [Space]
    [SerializeField] int amountOfEnemies = 0;
    #region StartFunction
    // Start is called before the first frame update
    void Start()
    {
        SpawnPointPattern();

        enemyContainer = GameObject.Find("EnemyContainer").transform;

        spawnPoints = FindObjectsOfType<TypeOfEnemy>();

        waveLevel++;

    }
    #endregion
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
        if (!hasSpawnedPattern)
        {
            if (waveLevel < numberOfWavesForEasyLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatterns.Count);
                Instantiate(spawnPointPatterns[randomPattern], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
                Debug.Log("Spawned new pattern");
                Debug.Log("Now spawning: Easy Enemies");
            }
            else if (waveLevel < numberOfWavesForMediumLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsMedium.Count);
                Instantiate(spawnPointPatternsMedium[randomPattern], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
                Debug.Log("Spawned new pattern");
                Debug.Log("Now spawning: Medium Enemies");
            }
            else if (waveLevel < numberOfWavesForHardLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsHard.Count);
                Instantiate(spawnPointPatternsHard[randomPattern], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
                Debug.Log("Spawned new pattern");
                Debug.Log("Now spawning: Hard Enemies");
            }
        }
            amountofEnemiesWanted = spawnPoints.Length;
            BeginWave();
        hasSpawnedPattern = true;
    }

    public int ProgressWave()
    {
        return waveLevel++;
    }
    private void FinishFloor()
    {
        if(waveLevel >= waveMaximum)
        {
            //Add behaviour for what happens when you finish a level.
            floorLevel++;
        }
    }

    private void DestroySpawnPattern()
    {
        var spawnPattern = GameObject.FindGameObjectWithTag("PatternSpawner");
        spawnPattern.SetActive(false);
        Destroy(spawnPattern, 0.1f);
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
            DestroySpawnPattern();
            hasSpawnedPattern = false;
            SpawnPointPattern();
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
