using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StartCountDown : MonoBehaviour
{
    [SerializeField] GameObject textObj;
    TextMeshProUGUI text;
    float scaleVal;
    private void Start()
    {
        text = textObj.GetComponent<TextMeshProUGUI>();
    }
    public void CountThree()
    {
        textObj.SetActive(true);
        scaleVal = 1;
        textObj.transform.localScale = new Vector3(1, 1, 1);
        text.text = "3";
    }
    public void CountTwo()
    {
        text.text = "2";
    }
    public void CountOne()
    {
        text.text = "1";
    }
    public void CountGo()
    {
        text.text = "GO!";
        Invoke("RemoveText", 1f);
    }
    void RemoveText()
    {
        StartCoroutine(Shrink());
    }
    IEnumerator Shrink()
    {
        while (true)
        {
            scaleVal = scaleVal - 0.05f;
            yield return new WaitForFixedUpdate();
            textObj.transform.localScale = new Vector3(scaleVal, scaleVal, scaleVal);
            if(scaleVal <= 0)
            {
                break;
            }
        }
        textObj.SetActive(false);
    }
}
