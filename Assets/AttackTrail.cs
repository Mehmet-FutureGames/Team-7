using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrail : MonoBehaviour
{
    [Range(0,0.8f)]
    [SerializeField]float trailPosMult;
    CapsuleCollider capCol;
    private void Awake()
    {
        capCol = transform.parent.GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    { 
        transform.localPosition = new Vector3(transform.parent.localPosition.x, transform.parent.localPosition.y, transform.parent.localPosition.z + (capCol.height) * trailPosMult);
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.01f);
        GetComponent<TrailRenderer>().enabled = true;
        yield return null;
    }
    private void OnDisable()
    {
        GetComponent<TrailRenderer>().enabled = false;
    }
}
