using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePooler : MonoBehaviour
{

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    public static NotePooler Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    void Start()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach(Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
                obj.transform.parent = this.gameObject.transform;
            }
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFormPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;
        //objectToSpawn.transform.localScale = scale;

        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }

}
