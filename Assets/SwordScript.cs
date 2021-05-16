using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SwordScript : MonoBehaviour
{
    [ContextMenu("IncreaseSwordSize")]
    public void IncreaseSwordSize(float amount)
    {
        transform.localScale *= amount;
    }

}
