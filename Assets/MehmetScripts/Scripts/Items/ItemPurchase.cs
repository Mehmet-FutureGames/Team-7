using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ItemPurchase : MonoBehaviour
{
    bool buyWithCoins = false;
    [SerializeField] bool canBuy;
    NotePublisher notePublisher;
    public StatItem itemStats;

    [HideInInspector] public string itemName;
    [HideInInspector] public float upgradeAmount;
    [HideInInspector] public ItemType itemType;
    [HideInInspector] public int itemCost;
    [HideInInspector] public int itemCostNotes;
    GameObject player;

    private void Awake()
    {
        player = FindObjectOfType<Player>().gameObject;
        GetComponent<BoxCollider>().isTrigger = true;
        notePublisher = FindObjectOfType<NotePublisher>();
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
        GetComponent<ItemParameter>().noteCost = itemCostNotes;
        GetComponent<ItemParameter>().coinCost = itemCost;
        GetComponent<ItemParameter>().itemName = itemName;
    }
    private void OnTriggerEnter(Collider other)
    {
        canBuy = true;
        ItemCanvas.isInBuyArea = true;
        Debug.Log(canBuy);
    }
    private void OnTriggerExit(Collider other)
    {
        ItemCanvas.isInBuyArea = false;
        canBuy = false;
        Debug.Log(canBuy);
    }
    private void UpgradeStats()
    {
        if (buyWithCoins && canBuy)
        {
            if (itemType == ItemType.HealthUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                player.GetComponent<PlayerHealth>().UpgradeHealth(upgradeAmount);
                player.GetComponent<PlayerCoinHandler>().coins -= itemCost;
                RemoveItemFromList();
            }
            else if (itemType == ItemType.AttackUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
                player.GetComponent<PlayerCoinHandler>().coins -= itemCost;
                RemoveItemFromList();
            }
            else if (itemType == ItemType.DashAttackUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
                player.GetComponent<PlayerCoinHandler>().coins -= itemCost;
                RemoveItemFromList();
            }
            else if (itemType == ItemType.FrenzyUpgrade && player.GetComponent<PlayerCoinHandler>().coins >= itemCost)
            {
                int upgrade = (int)upgradeAmount;
                player.GetComponent<PlayerFrenzy>().maxFrenzy += upgrade;
                player.GetComponent<PlayerCoinHandler>().coins -= itemCost;
                RemoveItemFromList();
            }
            else
            {
                Debug.Log("You don't have enough coins, item cost coins: " + itemCost);
            }
            gameObject.SetActive(false);
        }
        else if(!buyWithCoins && canBuy)
        {
            if (itemType == ItemType.HealthUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                player.GetComponent<PlayerHealth>().UpgradeHealth(upgradeAmount);
                player.GetComponent<NoteCurrencyHandler>().NoteCurrency -= itemCostNotes;
                RemoveItemFromList();
            }
            else if (itemType == ItemType.AttackUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                player.GetComponent<Player>().UpgradeDamageMelee(upgradeAmount);
                player.GetComponent<NoteCurrencyHandler>().NoteCurrency -= itemCostNotes;
                RemoveItemFromList();
            }
            else if (itemType == ItemType.DashAttackUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                player.GetComponent<Player>().UpgradeDamageDash(upgradeAmount);
                player.GetComponent<NoteCurrencyHandler>().NoteCurrency -= itemCostNotes;
                RemoveItemFromList();
            }
            else if (itemType == ItemType.FrenzyUpgrade && player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= itemCostNotes)
            {
                int upgrade = (int)upgradeAmount;
                player.GetComponent<PlayerFrenzy>().maxFrenzy += upgrade;
                player.GetComponent<NoteCurrencyHandler>().NoteCurrency -= itemCostNotes;
                RemoveItemFromList();
            }
            else
            {
                Debug.Log("You don't have enough notes, item cost notes: " + itemCostNotes);
            }
            gameObject.SetActive(false);
        }
    }
    private void OnLevelWasLoaded(int level)
    {
        if (level == 4)
        {
            buyWithCoins = true;
        }
    }
    private void RemoveItemFromList()
    {
        if (ShopHandler.Instance != null)
        {
            for (int i = 0; i < ShopHandler.Instance.items.Count; i++)
            {
                string name = ShopHandler.Instance.items[i].gameObject.name;
                if (gameObject.name == ShopHandler.Instance.items[i].gameObject.name)
                {
                    ShopHandler.Instance.items.RemoveAt(i);
                    break;
                }
            }
        }

    }
    private void OnEnable()
    {
        notePublisher.buttonHitAttack += UpgradeStats;
        notePublisher.noteHitAttack += UpgradeStats;
    }
    private void OnDisable()
    {
        notePublisher.buttonHitAttack -= UpgradeStats;
        notePublisher.noteHitAttack -= UpgradeStats;
    }
}
