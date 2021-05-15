using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var sword = Instantiate(GetComponentInParent<PlayerModels>().models[3], transform.position,Quaternion.identity, transform);
        sword.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        sword.transform.localRotation = Quaternion.Euler(new Vector3(270, 0, 0));
        sword.transform.localPosition = new Vector3(0.01f, 0.005f, -0.003f);
    }
}
