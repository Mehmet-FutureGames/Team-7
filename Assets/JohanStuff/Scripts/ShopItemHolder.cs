using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItemHolder : MonoBehaviour
{

    GameObject prefab;

    private void Start()
    {
        ShopHandler.shopItemHolders.Add(this);
    }

}
