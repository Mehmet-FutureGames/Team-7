using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCoinHandler : MonoBehaviour
{
    public float coins;

    public float Coins
    {
        get { return coins; }
        set
        {
            coins = value;
            SetText(coins);
        }
    }
    public static PlayerCoinHandler Instance;
    [SerializeField] TextMeshProUGUI text;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        Coins = 0;
    }

    public void AddCoins(float amount)
    {
        Coins += amount;
    }

    private void SetText(float amount)
    {
        text.text = amount.ToString("F0");
    }
}
