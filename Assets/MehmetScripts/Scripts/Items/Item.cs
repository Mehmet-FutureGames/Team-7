using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    public StatItem itemStats;

    [HideInInspector] public string itemName;
    [HideInInspector] public float upgradeAmount;
    [HideInInspector] public ItemType itemType;

    private void Awake()
    {
        if(itemStats.itemModel != null)
        {
            Instantiate(itemStats.itemModel,transform.position,Quaternion.identity,transform);
        }
        else
        {
            Debug.LogError("Item model is missing! Add it in the scriptable object: " + itemStats.name);
        }
        itemName = itemStats.nameItem;
        upgradeAmount = itemStats.upgradeAmount;
        itemType = itemStats.itemType;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UpgradeStats(other);
        }
    }

    private void UpgradeStats(Collider player)
    {
        if (itemType == ItemType.HealthUpgrade) 
        {
            player.GetComponent<PlayerHealth>().UpgradeHealth(upgradeAmount);
            gameObject.SetActive(false);
        }
        else if(itemType == ItemType.AttackUpgrade)
        {
            player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
            gameObject.SetActive(false);
        }
        else if (itemType == ItemType.DashAttackUpgrade)
        {
            player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
            gameObject.SetActive(false);
        }
    }
}
