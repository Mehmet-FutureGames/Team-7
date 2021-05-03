using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public List<GameObject> items = new List<GameObject>();
    public static List<ShopItemHolder> shopItemHolders = new List<ShopItemHolder>();

    public static ShopHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    private void Start()
    {
        if(items != null)
        {
            for (int i = 0; i < shopItemHolders.Count; i++)
            {
                int e = Random.Range(0, items.Count);
                Instantiate(items[e], shopItemHolders[i].transform.position, shopItemHolders[i].transform.rotation);
                items.RemoveAt(e);
            }
        }
    }
}
