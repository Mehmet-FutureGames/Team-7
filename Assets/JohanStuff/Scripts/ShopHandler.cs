using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
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
        //if (level == 3)
        //{
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
                    GameObject obj = Instantiate(items[i], shopItemHolders[i].transform.position, shopItemHolders[i].transform.rotation, shopItemHolders[i].transform);
                    GameObject priceCanvas = Instantiate(priceCanvasPrefab, shopItemHolders[i].transform);
                    GameObject itemNameCanvas = Instantiate(itemNameCanvasPrefab, shopItemHolders[i].transform);
                    itemNameCanvas.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetComponent<ItemParameter>().itemName;
                    priceCanvas.GetComponentInChildren<TextMeshProUGUI>().text = obj.GetComponent<ItemParameter>().coinCost.ToString();
                    //items.RemoveAt(e);
                }

            }
        //}
    }

    private void OnLevelWasLoaded(int level)
    {


    }
}
