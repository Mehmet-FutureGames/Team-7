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
    private void Awake()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
    }
    void Start()
    {
        hasPurchasedItem = false;
        itemParameter = GetComponent<ItemParameter>();
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
        if (isInBuyArea && PlayerCoinHandler.Instance.Coins >= itemParameter.cost)
        {
            PlayerCoinHandler.Instance.Coins -= itemParameter.cost;
            useItem.OnPickUpItem(activeItems.itemIndex, activeItems);
            ItemCanvas.isInBuyArea = false;
            useItem = null;
            gameObject.SetActive(false);
            hasPurchasedItem = true;
        }

    }


}
