using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class CoinGainFloat : MonoBehaviour
{
    TextMeshProUGUI tmPro;
    float value;
    private void Start()
    {
        value = 1;
        tmPro =  gameObject.GetComponent<TextMeshProUGUI>();
        Destroy(this.gameObject, 0.7f);
    }
    private void FixedUpdate()
    {
        value -= 0.01f;
        tmPro.color = tmPro.color * new Color(1, 1, 1, 1 * value);
        transform.Translate(Vector3.up *10 * Time.fixedDeltaTime);
    }
}
