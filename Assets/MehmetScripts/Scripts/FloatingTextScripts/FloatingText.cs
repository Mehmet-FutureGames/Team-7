using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class FloatingText : MonoBehaviour
{
    public float destroyTime = 1.5f;
    float value;
    public float initialPopSpeed;
    public float downScaleSpeed;
    TextMesh text;
    float XVal;
    private void Awake()
    {
        text = GetComponent<TextMesh>();
    }
    private void OnEnable()
    {
        transform.localScale = new Vector3(0, 0, 0);
        text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
        value = 0;
        XVal = Random.Range(-1f, 1f);
        StartCoroutine(PopText());
    }

    IEnumerator PopText()
    {
        while (true)
        {
            transform.Translate((new Vector3(XVal, 0, 0) + Vector3.up) * initialPopSpeed * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
            value = value + 0.1f;
            transform.localScale = new Vector3(value, value, value);
            if (value >= 1.5f)
            {
                break;
            }
        }
        value = 1.5f;
        yield return new WaitForSeconds(0.1f);
        while (true)
        {
            if (value > 0)
            {
                yield return new WaitForFixedUpdate();
                transform.Translate((new Vector3(XVal, 0, 0) + Vector3.down) * downScaleSpeed * Time.fixedDeltaTime);
                value = value - 0.1f;
                transform.localScale = new Vector3(value, value, value);
                text.color = new Color(text.color.r, text.color.g, text.color.b, value);
            }
            if (value <= 0)
            {
                gameObject.SetActive(false);
                break;
            }
        }

    }
}