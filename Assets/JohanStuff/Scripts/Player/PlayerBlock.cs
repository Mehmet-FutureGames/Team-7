using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    MeshRenderer mesh;
    Transform childObj;
    PlayerFrenzy playerFrenzy;
    bool cooldownReady = true;
    float cooldwonTimer;
    int blockCost;
    void Start()
    {
        playerFrenzy = GetComponentInParent<PlayerFrenzy>();
        mesh = GetComponent<MeshRenderer>();
        childObj = gameObject.transform.GetChild(0);
        Debug.Log(childObj);
        childObj.GetComponent<MeshRenderer>().enabled = false;
        mesh.enabled = false;
    }
    void Update()
    {
        if (playerFrenzy.CurrentFrenzy >= blockCost)
        {
            if (cooldownReady)
            {
                if (Input.GetKeyDown(KeyCode.Mouse1))
                {
                    mesh.enabled = true;
                    childObj.GetComponent<MeshRenderer>().enabled = true;
                    cooldownReady = false;
                    StartCoroutine(Cooldown());
                    playerFrenzy.CurrentFrenzy -= blockCost;
                }
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        mesh.enabled = false;
        childObj.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(cooldwonTimer - 1f);
        cooldownReady = true;
    }
}
