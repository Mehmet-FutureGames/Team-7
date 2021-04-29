using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopHandler : MonoBehaviour
{
    public List<GameObject> expensiveItem = new List<GameObject>();
    public List<GameObject> normalItem = new List<GameObject>();
    public List<GameObject> cheapItem = new List<GameObject>();
    public List<GameObject> randomItem = new List<GameObject>();
    public static List<ShopItemHolder> shopItemHolders = new List<ShopItemHolder>();

    private void Start()
    {


        if(expensiveItem != null && normalItem != null && cheapItem != null && randomItem != null)
        {
            int e = Random.Range(0, expensiveItem.Count);
            int n = Random.Range(0, normalItem.Count);
            int c = Random.Range(0, cheapItem.Count);
            int a = Random.Range(0, randomItem.Count);
            int b = Random.Range(0, randomItem.Count);
            Instantiate(expensiveItem[e], shopItemHolders[0].transform.position, shopItemHolders[0].transform.rotation);
            Instantiate(normalItem[n], shopItemHolders[1].transform.position, shopItemHolders[1].transform.rotation);
            Instantiate(cheapItem[c], shopItemHolders[2].transform.position, shopItemHolders[2].transform.rotation);
            Instantiate(randomItem[a], shopItemHolders[3].transform.position, shopItemHolders[3].transform.rotation);
            Instantiate(randomItem[b], shopItemHolders[4].transform.position, shopItemHolders[4].transform.rotation);
        }


    }
}
