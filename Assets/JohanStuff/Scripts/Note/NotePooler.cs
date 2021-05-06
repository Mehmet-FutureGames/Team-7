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
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
    }
    public List<NoteObject> noteObj;
    public Pool pool;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    Queue<GameObject> objectPool;
    void Start()
    {

        objectPool = new Queue<GameObject>();

    }

    public GameObject SpawnFormPool(string tag, Vector3 position)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
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
        poolDictionary[tag].Enqueue(objectToSpawn);
        return objectToSpawn;
    }
    private void OnLevelWasLoaded(int level)
    {
        GameObject obj = Instantiate(pool.prefab, transform.position, Quaternion.identity);
        noteObj.Add(obj.GetComponent<NoteObject>());
        obj.SetActive(false);
        objectPool.Enqueue(obj);
        obj.transform.parent = this.gameObject.transform;
        if (level == 2)
        {
            poolDictionary.Add(pool.tag, objectPool);
        }
    }
}
