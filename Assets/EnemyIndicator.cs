using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    [SerializeField]GameObject indicatorPrefab;
    GameObject obj;
    Transform player;
    float distance;
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        obj = Instantiate(indicatorPrefab);
        obj.SetActive(false);
        StartCoroutine(IndicatorCheck());
    }
    
    IEnumerator IndicatorCheck()
    {
        while (true)
        {
            distance = (player.position - transform.position).magnitude;
            if(distance > 45)
            {
                if (!obj.activeSelf)
                {
                    obj.SetActive(true);
                }
            }
            else if(distance < 45 && obj.activeSelf)
            {
                obj.SetActive(false);
            }
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }
    private void Update()
    {
        if (obj.activeSelf)
        {
            obj.transform.LookAt(transform.position);
            obj.transform.position = player.position + (transform.position - player.transform.position).normalized * 2.5f;
        }
        
    }
}
