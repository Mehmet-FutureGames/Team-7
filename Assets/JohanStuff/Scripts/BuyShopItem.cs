using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShopItem : MonoBehaviour
{
    private bool isInBuyArea;

    public bool IsInBuyArea
    {
        get { return isInBuyArea; }
        set
        {
            isInBuyArea = value;
            if (isInBuyArea)
            {
                StartCoroutine(OpenCanvas(itemCanvas));
            }
            else
            {
                StartCoroutine(CloseCanvas(itemCanvas));
            }
        }
    }
    ActiveItems activeItems;
    ItemCanvas itemCanvas;
    UseItem useItem;
    NotePublisher notePublisher;
    bool hasPurchasedItem;
    void Start()
    {
        hasPurchasedItem = false;
        itemCanvas = FindObjectOfType<ItemCanvas>();
        activeItems = GetComponent<ActiveItems>();
        notePublisher = FindObjectOfType<NotePublisher>();
        notePublisher.buttonHitAttack += BuyItem;
        notePublisher.noteHitAttack += BuyItem;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            useItem = other.GetComponent<UseItem>();
            IsInBuyArea = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            useItem = null;
            IsInBuyArea = false;
        }
    }

    public void BuyItem()
    {
        if (useItem != null)
        {
            if (IsInBuyArea && PlayerCoinHandler.Instance.Coins >= activeItems.cost)
            {
                PlayerCoinHandler.Instance.Coins -= activeItems.cost;
                useItem.OnPickUpItem(activeItems.itemIndex, activeItems);
                IsInBuyArea = false;
                notePublisher.buttonHitAttack -= BuyItem;
                notePublisher.noteHitAttack -= BuyItem;
                gameObject.SetActive(false);
                hasPurchasedItem = true;
            }
        }

    }
    public IEnumerator OpenCanvas(ItemCanvas canvas)
    {
        canvas.text.text = "Cost: " + activeItems.cost.ToString();
        canvas.transform.position = new Vector3(transform.position.x, transform.position.y + 10, transform.position.z);
        canvas.transform.localScale = new Vector3(0, 0, 0);
        canvas.gameObject.SetActive(true);
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
}
