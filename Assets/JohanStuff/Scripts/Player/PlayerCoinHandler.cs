using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCoinHandler : MonoBehaviour
{
    public float coins;
    [SerializeField] Text text;
    private void Start()
    {
        coins = 0;
        SetText(coins);
    }

    public void AddCoins(float amount)
    {
        coins += amount;
        SetText(coins);
    }

    private void SetText(float amount)
    {
        text.text = "Coins: " + amount.ToString("F0");
    }
}
