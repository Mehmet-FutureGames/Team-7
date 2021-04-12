using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] List<GameObject> enemyVariants;

    [SerializeField] int amountofEnemiesWanted;
    [SerializeField] int amountOfEnemies = 0;
    // Start is called before the first frame update
    void Start()
    {
       InvokeRepeating("DevelopmentSpawner", 0, 0.1f);
    }

    private void DevelopmentSpawner()
    {
        if (amountofEnemiesWanted > amountOfEnemies)
        {
            Instantiate(enemyVariants[0]);
            amountOfEnemies++;
        }
        else
        {
            Debug.Log(amountofEnemiesWanted + " has been spawned!");
            CancelInvoke();
        }
    }
}
