using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteCurrencyHandler : MonoBehaviour
{

    int noteCurrency;
    [SerializeField] TextMeshProUGUI text;

    public int NoteCurrency
    {
        get { return noteCurrency; }
        set 
        {
            noteCurrency = value;
            PlayerPrefs.SetInt("NoteCurrency", noteCurrency);
        }
    }

    private void Start()
    {
        noteCurrency = PlayerPrefs.GetInt("NoteCurrency");
        SetText(NoteCurrency);
    }

    public void AddNoteCurrency(int amount)
    {
        NoteCurrency += amount;
        SetText(NoteCurrency);
    }

    private void SetText(int amount)
    {
        text.text = amount.ToString("F0");
    }

}
