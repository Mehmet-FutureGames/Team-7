using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerCoinHandler : MonoBehaviour
{
    public float coins;
    public float coinBundle;
    private bool coroutineRunning;
    public float CoinBundle
    {
        get { return coinBundle; }
        set
        {
            coinBundle = value;
            if(coinBundle > 0f)
            {
                coinGainText.enabled = true;
            }
        }
    }
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
    [SerializeField] TextMeshProUGUI coinGainText;
    [SerializeField] GameObject textGainPrefab;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        coinGainText.enabled = false;
        text = GameObject.Find("CoinsText").GetComponent<TextMeshProUGUI>();
        Coins = 0;
    }

    public void AddCoins(float amount)
    {
        //Coins += amount;
        CoinBundle += amount;
        coinGainText.text = "+" + CoinBundle.ToString("F0");
        if (!coroutineRunning)
        {
            StartCoroutine(AddCoinsAfterDelay());
        }
    }

    public void SetText(float amount)
    {
        text.text = amount.ToString("F0");
    }
    IEnumerator AddCoinsAfterDelay()
    {
        coroutineRunning = true;
        yield return new WaitForSeconds(1f);
        Coins += CoinBundle;
        GameObject obj = Instantiate(textGainPrefab, coinGainText.transform);
        obj.GetComponent<TextMeshProUGUI>().text = "+" + CoinBundle.ToString("F0");
        CoinBundle = 0;
        coinGainText.enabled = false;
        coroutineRunning = false;
    }
}
