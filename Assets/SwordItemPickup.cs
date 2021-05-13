using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SwordItemPickup : MonoBehaviour
{
    SwordScript sword;
    PlayerAttack playerAttack;

    public float swordObjSizeMult;
    public float hitAreaMult;
    Player player;
    Scene currentLevel;
    bool buyWithCoins = false;
    [SerializeField] bool canBuy;
    NotePublisher notePublisher;
    ItemParameter itemParameter;
    private void Awake()
    {
        currentLevel = SceneManager.GetActiveScene();
        if (currentLevel.name == "CoinShop")
        {
            buyWithCoins = true;
        }
        else
        {
            buyWithCoins = false;
        }
        notePublisher = FindObjectOfType<NotePublisher>();
    }
    private void Start()
    {
        player = FindObjectOfType<Player>();
        itemParameter = GetComponent<ItemParameter>();
        sword = FindObjectOfType<SwordScript>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canBuy = true;
            ItemCanvas.isInBuyArea = true;
            ItemCanvas.Instance.descriptionText.text = itemParameter.itemDescription;
            ItemCanvas.Instance.itemName.text = itemParameter.itemName;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ItemCanvas.isInBuyArea = false;
            canBuy = false;
        }
    }

    void Purchase()
    {
        if (buyWithCoins && canBuy)
        {
            if(player.GetComponent<PlayerCoinHandler>().coins >= GetComponent<ItemParameter>().coinCost)
            {
                DoTheUpgradeStuff();
                PlayerCoinHandler.Instance.Coins -= GetComponent<ItemParameter>().coinCost;
                ItemCanvas.isInBuyArea = false;
            }
        }
        else if (!buyWithCoins && canBuy)
        { 
            if(player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= GetComponent<ItemParameter>().coinCost)
            {
                DoTheUpgradeStuff();
                NoteCurrencyHandler.Instance.NoteCurrency -= GetComponent<ItemParameter>().coinCost;
                ItemCanvas.isInBuyArea = false;
            }
        }
    }

    public void DoTheUpgradeStuff()
    {
        playerAttack.gameObject.SetActive(true);
        CapsuleCollider cap = playerAttack.GetComponent<CapsuleCollider>();
        cap.height = cap.height * hitAreaMult;
        cap.center *= hitAreaMult;
        sword.IncreaseSwordSize(swordObjSizeMult);
        playerAttack.gameObject.SetActive(false);
        ItemCanvas.isInBuyArea = false;
        RemoveItemFromList();
        gameObject.SetActive(false);
        Destroy(gameObject);
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
        notePublisher.buttonHitAttack += Purchase;
        notePublisher.noteHitAttack += Purchase;
    }
    private void OnDisable()
    {
        notePublisher.buttonHitAttack -= Purchase;
        notePublisher.noteHitAttack -= Purchase;
    }
}
