using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaArrowDisable : MonoBehaviour
{
    private void OnLevelWasLoaded(int level)
    {
        gameObject.SetActive(false);
    }
}
