using UnityEngine;

public class TypeOfEnemy : MonoBehaviour
{
    [SerializeField] int enemyType;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x,0,transform.position.z);
        ObjectPooler.Instance.SpawnFormPool("EnemySpawn", transform.position);
    }

    public int ReturnEnemyType()
    {
        return enemyType;
    }
}
