using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlock : MonoBehaviour
{
    NotePublisher publisher;
    MeshRenderer mesh;
    Transform childObj;
    PlayerFrenzy playerFrenzy;
    bool cooldownReady = true;
    float cooldwonTimer;
    [SerializeField]int blockCost;
    public static bool isBlocking;
    void Start()
    {
        playerFrenzy = GetComponentInParent<PlayerFrenzy>();
        mesh = GetComponent<MeshRenderer>();
        childObj = gameObject.transform.GetChild(0);
        childObj.GetComponent<MeshRenderer>().enabled = false;
        mesh.enabled = false;
        publisher = FindObjectOfType<NotePublisher>();
        publisher.noteHitBlock += Block;
    }

    private void Block()
    {
        if (playerFrenzy.CurrentFrenzy >= blockCost)
        {
            if (cooldownReady)
            {

                mesh.enabled = true;
                childObj.GetComponent<MeshRenderer>().enabled = true;
                cooldownReady = false;
                StartCoroutine(Cooldown());
                playerFrenzy.CurrentFrenzy -= blockCost;
                isBlocking = true;
            }
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(0.4f);
        mesh.enabled = false;
        isBlocking = false;
        childObj.GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(cooldwonTimer - 1f);
        cooldownReady = true;
    }
}
