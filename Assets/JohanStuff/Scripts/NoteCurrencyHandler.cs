using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NoteCurrencyHandler : MonoBehaviour
{

    public static NoteCurrencyHandler Instance;
    int noteCurrency;
    [SerializeField] TextMeshProUGUI text;

    public int NoteCurrency
    {
        get { return noteCurrency; }
        set 
        {
            noteCurrency = value;
            PlayerPrefs.SetInt("NoteCurrency", noteCurrency);
            text.text = noteCurrency.ToString("F0");
        }
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        text = GameObject.Find("NoteCurrencyText").GetComponent<TextMeshProUGUI>();
        noteCurrency = PlayerPrefs.GetInt("NoteCurrency");
        SetText(NoteCurrency);
        text.text = noteCurrency.ToString("F0");
    }

    public void AddNoteCurrency(int amount)
    {
        NoteCurrency += amount;
        SetText(NoteCurrency);
    }

    public void SetText(int amount)
    {
        text.text = amount.ToString("F0");
    }

}
