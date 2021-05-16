using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIndicator : MonoBehaviour
{
    // A script that is put on every enemy model.
    // Instantiates an arrow on the player position and puts the direction of it towards the enemy model.
    [SerializeField]GameObject indicatorPrefab;
    GameObject obj;
    Transform player;
    float distance;
    private void Start()
    {
        player = FindObjectOfType<Player>().transform;
        obj = Instantiate(indicatorPrefab, transform);
        obj.transform.localScale = new Vector3(obj.transform.localScale.x / transform.localScale.x, obj.transform.localScale.y / transform.localScale.y, obj.transform.localScale.z / transform.localScale.z);
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
    private void LateUpdate()
    {

        obj.transform.LookAt(transform.position);
        obj.transform.position = player.position + (transform.position - player.transform.position).normalized * 2.5f;
        
    }


}
