using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteCurrencyHandler : MonoBehaviour
{

    int noteCurrency;
    [SerializeField] Text text;


    private void Start()
    {
        noteCurrency = PlayerPrefs.GetInt("NoteCurrency");
        SetText(noteCurrency);
    }

    public void AddNoteCurrency(int amount)
    {
        noteCurrency += amount;
        PlayerPrefs.SetInt("NoteCurrency", noteCurrency);
        SetText(noteCurrency);
    }

    private void SetText(int amount)
    {
        text.text = "Notes: " + amount.ToString("F0");
    }

}
