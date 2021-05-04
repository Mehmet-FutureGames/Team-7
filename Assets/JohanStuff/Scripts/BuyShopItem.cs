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
    private void Awake()
    {
        
    }
    void Start()
    {
        hasPurchasedItem = false;
        itemParameter = GetComponent<ItemParameter>();
        activeItems = GetComponent<ActiveItems>();
        notePublisher = FindObjectOfType<NotePublisher>();
        notePublisher.buttonHitAttack += BuyItem;
        notePublisher.noteHitAttack += BuyItem;
        itemCanvas = ItemCanvas.Instance;
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            useItem = other.GetComponent<UseItem>();
            ItemCanvas.isInBuyArea = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            useItem = null;
            ItemCanvas.isInBuyArea = false;
        }
    }

    public void BuyItem()
    {
        if (useItem != null)
        {
            if (ItemCanvas.isInBuyArea && PlayerCoinHandler.Instance.Coins >= itemParameter.cost)
            {
                PlayerCoinHandler.Instance.Coins -= itemParameter.cost;
                useItem.OnPickUpItem(activeItems.itemIndex, activeItems);
                ItemCanvas.isInBuyArea = false;
                useItem = null;
                //gameObject.SetActive(false);
                hasPurchasedItem = true;
                notePublisher.buttonHitAttack -= BuyItem;
                notePublisher.noteHitAttack -= BuyItem;
            }
        }

    }
    /*
    public IEnumerator OpenCanvas(ItemCanvas canvas)
    {
        canvas.gameObject.SetActive(true);
        canvas.transform.localScale = new Vector3(0, 0, 0);
        canvas.text.text = "Press attack to buy";
        canvas.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        float i = 0;
        while (i <= 0.08f)
        {
            canvas.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.003f);
            i += 0.004f;
        }
        yield return null;
    }
    public IEnumerator CloseCanvas(ItemCanvas canvas)
    {
        float i = canvas.transform.localScale.x;
        while (i >= 0)
        {
            canvas.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.003f);
            i -= 0.004f;
        }
        canvas.gameObject.SetActive(false);
        if (hasPurchasedItem)
        {
            Destroy(gameObject);
        }
        yield return null;
    }
    */

}
