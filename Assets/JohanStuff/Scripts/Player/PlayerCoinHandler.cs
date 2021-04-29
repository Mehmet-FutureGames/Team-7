using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCoinHandler : MonoBehaviour
{
    public float coins;
    [SerializeField] TextMeshProUGUI text;
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
        text.text = amount.ToString("F0");
    }
}
