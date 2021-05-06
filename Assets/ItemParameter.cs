using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemParameter : MonoBehaviour
{
    public int noteCost;
    public int coinCost;
    public string itemName;
    public bool buyWithCoins;
    private void OnLevelWasLoaded(int level)
    {
        if (level == 2)
        {
            buyWithCoins = true;
        }
        else
        {
            buyWithCoins = false;
        }

    }
}
