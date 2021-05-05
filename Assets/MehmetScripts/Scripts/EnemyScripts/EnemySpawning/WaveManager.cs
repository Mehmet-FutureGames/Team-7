using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    Enemy[] enemy;

    LevelManager manager;

    bool hasSpawnedPattern = false;

    int randomPattern;

    Transform enemyContainer;

    [SerializeField] List<GameObject> enemyVariants;
    [Space]

    [SerializeField] List<Transform> spawnPointPatternsEasy;
    [Space]

    [SerializeField] List<Transform> spawnPointPatternsMedium;
    [Space]

    [SerializeField] List<Transform> spawnPointPatternsHard;
    [Space]

    [SerializeField] List<Transform> itemSpawnLocations;
    [Space]

    [SerializeField] GameObject door;
    [Space]

    static Enemy[] enemies;

    TypeOfEnemy[] spawnPoints;
    int amountofEnemiesWanted;
    int floorLevel = 0;
    public int waveLevel = 0;
    [Space]
    [SerializeField] int numberOfWavesForEasyLevel = 0;
    [Space]
    [SerializeField] int numberOfWavesForMediumLevel = 0;
    [Space]
    [SerializeField] int numberOfWavesForHardLevel = 0;
    [Space]
    public int waveMaximum = 0;
    int amountOfEnemies = 0;

    bool hasCompleted = true;

    ItemList statItems;
    #region StartFunction
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPointPattern", 0.1f);

        manager = FindObjectOfType<LevelManager>();

        statItems = FindObjectOfType<ItemList>();

        enemyContainer = GameObject.Find("EnemyContainer").transform;

        spawnPoints = FindObjectsOfType<TypeOfEnemy>();

        spawnItems();
    }
    #endregion
    #region BeginWaveSpawnEnemies
    private void BeginWave()
    {
        if (spawnPoints != null)
        {
            //Goes through all of the spawn points
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (waveLevel <= waveMaximum)
                {
                    FindSpawnPoints();
                    Instantiate(enemyVariants[spawnPoints[i].ReturnEnemyType()], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                    amountOfEnemies++;
                }
            }
        }
        DestroySpawnPattern();
    }
    #endregion

    #region spawnPatterns
    private void SpawnPointPattern()
    {
        if (!hasSpawnedPattern)
        {
            ProgressWave();
            if (waveLevel < numberOfWavesForEasyLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsEasy.Count);
                Instantiate(spawnPointPatternsEasy[randomPattern], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
            }
            else if (waveLevel < numberOfWavesForMediumLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsMedium.Count);
                Instantiate(spawnPointPatternsMedium[randomPattern], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
            }
            else if (waveLevel < numberOfWavesForHardLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsHard.Count);
                Instantiate(spawnPointPatternsHard[randomPattern], transform.position, Quaternion.identity, transform);
                FindSpawnPoints();
            }
        }
        amountofEnemiesWanted = spawnPoints.Length;
        if (hasCompleted)
        {
            BeginWave();
            hasCompleted = false;
        }
        hasSpawnedPattern = true;        
    }
    #endregion

    public int ProgressWave()
    {
        return waveLevel++;
    }
    public void FinishFloor()
    {
        //Add behaviour for what happens when you finish a level.
        spawnItems();
        floorLevel++;
        Instantiate(door);
        PlayerPrefs.SetInt("floorLevel", floorLevel);
    }

    public static void DestroyAllEnemies()
    {
        enemies = FindObjectsOfType<Enemy>();
    }

    private void DestroySpawnPattern()
    {
        var spawnPattern = GameObject.FindGameObjectWithTag("PatternSpawner");
        Destroy(spawnPattern);
    }

    private void FindSpawnPoints()
    {
        //Checks for new spawnpoints
        spawnPoints = FindObjectsOfType<TypeOfEnemy>();
    }

    public void EnemyDefeated()
    {
        amountOfEnemies--;
        if (amountOfEnemies < 3 && waveLevel < waveMaximum)
        {
            hasSpawnedPattern = false;
            SpawnPointPattern();
            hasCompleted = true;
        }
        else if (amountOfEnemies <= 0 && waveLevel >= waveMaximum)
        {
            FinishFloor();
            spawnItems();
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

    private void spawnItems()
    {
        for (int i = 0; i < itemSpawnLocations.Count; i++)
        {
            int randomItem = Random.Range(0, statItems.statItems.Count);
            Instantiate(statItems.statItems[randomItem], itemSpawnLocations[i]);
            statItems.statItems.RemoveAt(randomItem);
        }
    }

}
