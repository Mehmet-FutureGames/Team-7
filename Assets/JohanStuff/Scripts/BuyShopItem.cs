using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BuyShopItem : MonoBehaviour
{
    ActiveItems activeItems;
    ItemParameter itemParameter;
    ItemCanvas itemCanvas;
    UseItem useItem;
    NotePublisher notePublisher;
    bool hasPurchasedItem;
    bool isInBuyArea;
    NoteCurrencyHandler noteCurrency;
    private void Awake()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
    }
    void Start()
    {
        hasPurchasedItem = false;
        itemParameter = GetComponent<ItemParameter>();
        noteCurrency = FindObjectOfType<NoteCurrencyHandler>();
        activeItems = GetComponent<ActiveItems>();
        
        itemCanvas = ItemCanvas.Instance;
    }
    private void OnEnable()
    {
        notePublisher.buttonHitAttack += BuyItem;
        notePublisher.noteHitAttack += BuyItem;
    }
    private void OnDisable()
    {
        notePublisher.buttonHitAttack -= BuyItem;
        notePublisher.noteHitAttack -= BuyItem;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            useItem = other.GetComponent<UseItem>();
            ItemCanvas.isInBuyArea = true;
            isInBuyArea = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            useItem = null;
            ItemCanvas.isInBuyArea = false;
            isInBuyArea = false;
        }
    }

    public void BuyItem()
    {
        if (itemParameter.buyWithCoins)
        {
            if (isInBuyArea && PlayerCoinHandler.Instance.Coins >= itemParameter.coinCost)
            {
                PlayerCoinHandler.Instance.Coins -= itemParameter.coinCost;
                RemoveItemFromList();
                useItem.OnPickUpItem(activeItems.itemIndex, activeItems);
                ItemCanvas.isInBuyArea = false;
                useItem = null;
                gameObject.SetActive(false);
                hasPurchasedItem = true;
            }
        }
        else
        {
            if (isInBuyArea && noteCurrency.NoteCurrency >= itemParameter.noteCost)
            {
                noteCurrency.NoteCurrency -= itemParameter.noteCost;
                RemoveItemFromList();
                useItem.OnPickUpItem(activeItems.itemIndex, activeItems);
                ItemCanvas.isInBuyArea = false;
                useItem = null;
                gameObject.SetActive(false);
                hasPurchasedItem = true;
            }
        }

    }

    private void RemoveItemFromList()
    {
        for (int i = 0; i < ShopHandler.Instance.items.Count; i++)
        {
            string name = ShopHandler.Instance.items[i].gameObject.name;
            if(gameObject.name == ShopHandler.Instance.items[i].gameObject.name)
            {
                ShopHandler.Instance.items.RemoveAt(i);
                break;
            }
        }
    }
}
