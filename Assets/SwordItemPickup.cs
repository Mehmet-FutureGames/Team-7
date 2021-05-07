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
        
        sword = FindObjectOfType<SwordScript>();
        playerAttack = FindObjectOfType<PlayerAttack>();
    }

    private void OnTriggerEnter(Collider other)
    {
        canBuy = true;
        ItemCanvas.isInBuyArea = true;
    }
    private void OnTriggerExit(Collider other)
    {
        ItemCanvas.isInBuyArea = false;
        canBuy = false;
    }

    void Purchase()
    {
        if (buyWithCoins && canBuy)
        {
            if(player.GetComponent<PlayerCoinHandler>().coins >= GetComponent<ItemParameter>().coinCost)
            {
                DoTheUpgradeStuff();
                PlayerCoinHandler.Instance.Coins -= GetComponent<ItemParameter>().coinCost;
            }
        }
        else if (!buyWithCoins && canBuy)
        { 
            if(player.GetComponent<NoteCurrencyHandler>().NoteCurrency >= GetComponent<ItemParameter>().coinCost)
            {
                DoTheUpgradeStuff();
                NoteCurrencyHandler.Instance.NoteCurrency -= GetComponent<ItemParameter>().coinCost;
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
