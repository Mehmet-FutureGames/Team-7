using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemPurchase : MonoBehaviour
{
    bool buyWithCoins = false;
    public StatItem itemStats;

    [HideInInspector] public string itemName;
    [HideInInspector] public float upgradeAmount;
    [HideInInspector] public ItemType itemType;
    [HideInInspector] public int itemCost;
    [HideInInspector] public int itemCostNotes;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        #region spawnItemModel
        if (itemStats.itemModel != null)
        {
            ItemList.items.Add(Instantiate(itemStats.itemModel, transform.position, Quaternion.identity, transform));
        }
        else
        {
            Debug.LogError("Item model is missing! Add it in the scriptable object: " + itemStats.name);
        }
        #endregion
        itemName = itemStats.nameItem;
        upgradeAmount = itemStats.upgradeAmount;
        itemType = itemStats.itemType;
        itemCost = itemStats.itemCostCoins;
        itemCostNotes = itemStats.itemCostNotes;
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
        if (buyWithCoins )
        {
            if (itemType == ItemType.HealthUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                player.GetComponent<PlayerHealth>().UpgradeHealth(upgradeAmount);
                Debug.Log("Upgraded Health!");
            }
            else if (itemType == ItemType.AttackUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
                Debug.Log("Upgraded Melee Attack!");
            }
            else if (itemType == ItemType.DashAttackUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
                Debug.Log("Upgraded Dash Attack!");
            }
            else if (itemType == ItemType.FrenzyUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                int upgrade = (int)upgradeAmount;
                player.GetComponent<PlayerFrenzy>().maxFrenzy += upgrade;
                Debug.Log("Upgraded Max Frenzy!");
            }
            else
            {
                Debug.Log("You don't have enough coins, item cost coins: " + itemCost);
            }
        }
        else
        {
            if (itemType == ItemType.HealthUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                player.GetComponent<PlayerHealth>().UpgradeHealth(upgradeAmount);
                Debug.Log("Upgraded Health!");
            }
            else if (itemType == ItemType.AttackUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
                Debug.Log("Upgraded Melee Attack!");
            }
            else if (itemType == ItemType.DashAttackUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
                Debug.Log("Upgraded Dash Attack!");
            }
            else if (itemType == ItemType.FrenzyUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                int upgrade = (int)upgradeAmount;
                player.GetComponent<PlayerFrenzy>().maxFrenzy += upgrade;
                Debug.Log("Upgraded Max Frenzy!");
            }
            else
            {
                Debug.Log("You don't have enough notes, item cost notes: " + itemCostNotes);
            }
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 4)
        {
            buyWithCoins = true;
        }
    }
}
