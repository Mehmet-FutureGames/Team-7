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
    bool isInBuyArea;
    NoteCurrencyHandler noteCurrency;
    void Start()
    {
        itemParameter = GetComponent<ItemParameter>();
        noteCurrency = FindObjectOfType<NoteCurrencyHandler>();
        activeItems = GetComponent<ActiveItems>();
        
        itemCanvas = ItemCanvas.Instance;
    }
    private void OnEnable()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
        //NotePublisher.Instance.buttonHitAttack += BuyItem;
        //NotePublisher.Instance.noteHitAttack += BuyItem;
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
        if (other.CompareTag("Player") && !PauseMenu.GameIsPaused)
        {
            useItem = other.GetComponent<UseItem>();
            ItemCanvas.isInBuyArea = true;
            isInBuyArea = true;
            ItemCanvas.Instance.descriptionText.text = itemParameter.itemDescription;
            ItemCanvas.Instance.itemName.text = itemParameter.itemName;
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
                useItem.OnPickUpItem(activeItems);
                ItemCanvas.isInBuyArea = false;
                useItem = null;
                AudioManager.PlaySound("SingeCoinSound", "VFXSound");
                gameObject.SetActive(false);
            }
        }
        else
        {
            if (isInBuyArea && noteCurrency.NoteCurrency >= itemParameter.noteCost)
            {
                noteCurrency.NoteCurrency -= itemParameter.noteCost;
                RemoveItemFromList();
                useItem.OnPickUpItem(activeItems);
                ItemCanvas.isInBuyArea = false;
                useItem = null;
                AudioManager.PlaySound("SingeCoinSound", "VFXSound");
                gameObject.SetActive(false);
            }
        }

    }

    private void RemoveItemFromList()
    {
        if(ShopHandler.Instance != null)
        {
            for (int i = 0; i < ShopHandler.Instance.items.Count; i++)
            {
                string name = ShopHandler.Instance.items[i].gameObject.name;
                if (gameObject.name == ShopHandler.Instance.items[i].gameObject.name)
                {
                    ShopHandler.Instance.items.RemoveAt(i);
                    break;
                }
            }
        }

    }
}
