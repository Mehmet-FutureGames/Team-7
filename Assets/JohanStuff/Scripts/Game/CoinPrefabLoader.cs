using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPrefabLoader : MonoBehaviour
{
    public static CoinPrefabLoader Instance;
    [HideInInspector]public GameObject coinPrefab;
    private void Awake()
    {
        Instance = this;
        coinPrefab = Resources.Load("Coin") as GameObject;
    }
}
