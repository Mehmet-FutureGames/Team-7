using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class ShopHandler : MonoBehaviour
{
    public List<GameObject> itemPool = new List<GameObject>();
    public List<GameObject> items = new List<GameObject>();
    public static List<ShopItemHolder> shopItemHolders = new List<ShopItemHolder>();

    public GameObject priceCanvasPrefab;
    public GameObject itemNameCanvasPrefab;

    public static ShopHandler Instance;

    private void Awake()
    {
        shopItemHolders.Clear();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.1f);


        if ((itemPool != null && shopItemHolders != null) && itemPool.Count > 0)
        {
            for (int i = 0; i < 5; i++)
            {
                Debug.Log("2: " + i);
                int x = Random.Range(0, itemPool.Count);
                items.Add(itemPool[x]);
                itemPool.RemoveAt(x);

                if (itemPool.Count == 0)
                {
                    break;
                }
            }
            Debug.Log(shopItemHolders.Count);
            for (int i = 0; i < items.Count; i++)
            {
                Debug.Log("3: " + i);
                GameObject obj = Instantiate(items[i], shopItemHolders[i].transform.position, shopItemHolders[i].transform.rotation, shopItemHolders[i].transform);
                GameObject priceCanvas = Instantiate(priceCanvasPrefab, shopItemHolders[i].transform);
                GameObject itemNameCanvas = Instantiate(itemNameCanvasPrefab, shopItemHolders[i].transform);
                itemNameCanvas.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetComponent<ItemParameter>().itemName;
                priceCanvas.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetComponent<ItemParameter>().coinCost.ToString();
                obj.name = obj.name.Replace("(Clone)", "");
                //items.RemoveAt(e);
            }
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        shopItemHolders.Clear();
        if (items.Count > 0)
        {
            int x = items.Count;
            for (int i = 0; i < x; i++)
            {
                Debug.Log("1: " + i);
                itemPool.Add(items[0]);
                items.RemoveAt(0);
            }
        }
        if (level == 2 && (items.Count > 0 || itemPool.Count > 0))
        {
            StartCoroutine(Wait());
        }
        else if(level != 3 && items != null)
        {

        }
        if(level == 4 && (items.Count > 0 || itemPool.Count > 0))
        {
            StartCoroutine(Wait());
        }
        if(level == 0)
        {
            Destroy(gameObject);
        }
    }
}
