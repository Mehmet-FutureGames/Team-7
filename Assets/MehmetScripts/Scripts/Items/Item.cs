using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Item : MonoBehaviour
{
    public StatItem itemStats;

    Item[] itemsInGame;

    [HideInInspector] public string itemName;
    [HideInInspector] public float upgradeAmount;
    [HideInInspector] public ItemType itemType;
    [HideInInspector] public int itemCost;

    private void Awake()
    {
        //Adds the instaniated items into list and sets the box collider trigger to true
        GetComponent<BoxCollider>().isTrigger = true;
        #region spawnItemModel
        if (itemStats.itemModel != null)
        {
           ItemList.items.Add(Instantiate(itemStats.itemModel,transform.position,Quaternion.identity,transform));
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
            }
            else if (itemType == ItemType.AttackUpgrade)
            {
                player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
            }
            else if (itemType == ItemType.DashAttackUpgrade)
            {
                player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
            }
            else if (itemType == ItemType.FrenzyUpgrade)
            {
                int upgrade = (int)upgradeAmount;
                player.GetComponent<PlayerFrenzy>().maxFrenzy += upgrade;
            }
        itemsInGame = FindObjectsOfType<Item>();
        for (int i = 0; i < itemsInGame.Length; i++)
        {
            itemsInGame[i].gameObject.SetActive(false);
        }
    }
}
