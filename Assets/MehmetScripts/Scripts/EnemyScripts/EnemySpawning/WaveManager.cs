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
    #region StartFunction
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnPointPattern", 0.1f);

        manager = FindObjectOfType<LevelManager>();

        enemyContainer = FindObjectOfType<WaveManager>().transform;

        spawnPoints = FindObjectsOfType<TypeOfEnemy>();

        InvokeRepeating("AmountOfEnemiesOnMap", 0, 5);
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
                    Instantiate(enemyVariants[spawnPoints[i].ReturnEnemyType()], spawnPoints[i].GetComponent<Transform>().position, Quaternion.identity, enemyContainer);
                    amountOfEnemies++;
                }
            }
            Invoke("DestroySpawnPattern", 1f);
        }
    }
    #endregion

    #region spawnPatterns
    private void SpawnPointPattern()
    {
        if (!hasSpawnedPattern)
        {
            if (waveLevel < numberOfWavesForEasyLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsEasy.Count);
                Instantiate(spawnPointPatternsEasy[randomPattern], transform.position, Quaternion.identity, transform);
            }
            else if (waveLevel < numberOfWavesForMediumLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsMedium.Count);
                Instantiate(spawnPointPatternsMedium[randomPattern], transform.position, Quaternion.identity, transform);
            }
            else if (waveLevel < numberOfWavesForHardLevel)
            {
                randomPattern = Random.Range(0, spawnPointPatternsHard.Count);
                Instantiate(spawnPointPatternsHard[randomPattern], transform.position, Quaternion.identity, transform);
            }
            ProgressWave();
            FindSpawnPoints();
        }
        amountofEnemiesWanted = spawnPoints.Length;
        if (hasCompleted)
        {
            hasCompleted = false;
            BeginWave();
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
        floorLevel++;
        Instantiate(door);
        PlayerPrefs.SetInt("floorLevel", floorLevel);
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
        if (amountOfEnemies <= 3 && waveLevel < waveMaximum)
        {
            hasCompleted = true;
            hasSpawnedPattern = false;
            SpawnPointPattern();
        }
        if (amountOfEnemies <= 0 && waveLevel >= waveMaximum)
        {
            FinishFloor();
        }
    }
    public void AmountOfEnemiesOnMap()
    {

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
