using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public List<GameObject> itemPool = new List<GameObject>();
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
    private void OnLevelWasLoaded(int level)
    {
        if(level == 3)
        {
            if (itemPool != null)
            {
                for (int i = 0; i < 5; i++)
                {
                    int x = Random.Range(0, itemPool.Count);
                    items.Add(itemPool[x]);
                    itemPool.RemoveAt(x);
                    if (itemPool.Count == 0)
                    {
                        break;
                    }
                }

                for (int i = 0; i < items.Count; i++)
                {
                    int e = Random.Range(0, items.Count);
                    Instantiate(items[e], shopItemHolders[i].transform.position, shopItemHolders[i].transform.rotation, shopItemHolders[i].transform);
                    //items.RemoveAt(e);
                }

            }
        }

    }
}
