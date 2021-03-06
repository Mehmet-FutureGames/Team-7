using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemParameter : MonoBehaviour
{
    Scene currentScene;
    public int noteCost;
    public int coinCost;
    public string itemName;
    [TextArea(10,10)]
    public string itemDescription;
    public bool buyWithCoins;
    private void Awake()
    {
        currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "CoinShop")
        {
            buyWithCoins = true;
        }
        else
        {
            buyWithCoins = false;
        }
    }
}
