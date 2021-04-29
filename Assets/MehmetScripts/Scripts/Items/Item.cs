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
        #region spawnItemModel
        if (itemStats.itemModel != null)
        {
            Instantiate(itemStats.itemModel,transform.position,Quaternion.identity,transform);
        }
        else
        {
            Debug.LogError("Item model is missing! Add it in the scriptable object: " + itemStats.name);
        }
        #endregion
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
            Debug.Log("Upgraded Health!");
            gameObject.SetActive(false);
        }
        else if(itemType == ItemType.AttackUpgrade)
        {
            player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
            Debug.Log("Upgraded Melee Attack!");
            gameObject.SetActive(false);
        }
        else if (itemType == ItemType.DashAttackUpgrade)
        {
            player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
            Debug.Log("Upgraded Dash Attack!");
            gameObject.SetActive(false);
        }
        else if(itemType == ItemType.FrenzyUpgrade)
        {
            int upgrade = (int)upgradeAmount;
            player.GetComponent<PlayerFrenzy>().maxFrenzy += upgrade;
            Debug.Log("Upgraded Max Frenzy!");
            gameObject.SetActive(false);
        }
    }
}
