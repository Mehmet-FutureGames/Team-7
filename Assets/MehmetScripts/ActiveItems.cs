using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveItems : MonoBehaviour
{
    public bool isInBuyArea;

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

    int cost;
    public int itemIndex;

    public ItemCanvas itemCanvas;

    protected virtual void Start()
    {
        itemCanvas = FindObjectOfType<ItemCanvas>();
    }

    public virtual void PerformAction()
    {

    }
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInBuyArea = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsInBuyArea = false;
        }
    }

    public IEnumerator OpenCanvas(ItemCanvas canvas)
    {
        canvas.transform.position = new Vector3(transform.position.x, transform.position.y +5, transform.position.z);
        canvas.transform.localScale = new Vector3(0, 0, 0);
        canvas.gameObject.SetActive(true);
        float i = 0;
        while(i <= 0.1f)
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
        while(i >= 0)
        {
            canvas.transform.localScale = new Vector3(i, i, i);
            yield return new WaitForSeconds(0.003f);
            i -= 0.004f;
        }
        canvas.gameObject.SetActive(false);
        yield return null;
    }
}
