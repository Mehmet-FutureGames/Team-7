using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ActiveItems : MonoBehaviour
{
    public int itemIndex;
    public abstract void UseItem(); 
}
