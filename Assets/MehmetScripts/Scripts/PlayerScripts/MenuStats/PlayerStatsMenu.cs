using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class PlayerStatsMenu : MonoBehaviour
{
    int amountOfHealthUpgrades;
    int amountofDamageUpgrades;
    int amountOfFrenzyUpgrades;

    [SerializeField] AudioClip[] boughtCharacterClips;
    [SerializeField] AudioSource boughtCharacterSound;


    public bool hasStartedFirstTime = false;

    [SerializeField] PlayerStats stats;

    TextMeshProUGUI notesText;

    [SerializeField] int currentCharacterSelected;

    JSONObject playerStatsJson;

    [SerializeField] GameObject lockScreen;

    string savedPlayerName;

    [SerializeField] public static bool hasUpgraded;

    bool hasSaved;

    int notes;

    [SerializeField] int upgradeNotesAmount;
    [Space]
    [SerializeField] int healthUpgrade;
    [SerializeField] int damageUpgrade;
    [SerializeField] int frenzyUpgrade;

    [Space]
    [Space]

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] TextMeshProUGUI damageText;

    [SerializeField] TextMeshProUGUI frenzyText;

    [SerializeField] TextMeshProUGUI cantBuyCharacter;

    [SerializeField] int startingNotes;

    [SerializeField] Button button;

    [SerializeField] GameObject healthMinusButton;
    [SerializeField] GameObject DamageMinusButton;
    [SerializeField] GameObject FrenzyMinusButton;
    [SerializeField] GameObject saveTextBox;

    [SerializeField] GameObject confirmUpgrade;

    [SerializeField] List<GameObject> characters = new List<GameObject>();

    List<Material> unlockedMaterials = new List<Material>();
    [SerializeField] Material lockedMaterial;
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            unlockedMaterials.Add(characters[i].GetComponentInChildren<SkinnedMeshRenderer>().material);
        }
    }
    void Start()
    {
        AudioManager.sources.Clear();
        AudioManager.audioClips.Clear();
        currentCharacterSelected = PlayerPrefs.GetInt("currentSelectedCharacter");

        if (!characters[currentCharacterSelected].GetComponent<CharacterStats>().hasBeenBought)
        {
            characters[currentCharacterSelected].GetComponentInChildren<SkinnedMeshRenderer>().material = lockedMaterial;
        }

        //Gets the amount of notes you have stored.
        notes = PlayerPrefs.GetInt("NoteCurrency");
        notesText = GameObject.Find("NotesAmount").GetComponent<TextMeshProUGUI>();
        hasStartedFirstTime = PlayerPrefs.GetInt("hasStartedFirstTime") == 1;
        characters[currentCharacterSelected].SetActive(true);
        RetrieveStats(currentCharacterSelected);

        if (!characters[0].GetComponent<CharacterStats>().hasBeenBought)
        {
            //incase it's the first time that the player starts.
            //Buy the first character but don't play the sound.
            BuyCharacter();
            boughtCharacterSound.Stop();
        }
        if (characters[currentCharacterSelected].GetComponent<CharacterStats>().hasBeenBought)
        {
            cantBuyCharacter.gameObject.SetActive(false);
            lockScreen.SetActive(false);
        }
        else
        {
            cantBuyCharacter.gameObject.SetActive(true);
            lockScreen.SetActive(true);
        }

        UpdateTextUpgrade();
        if (!hasStartedFirstTime)
        {
            PlayerPrefs.SetInt("hasStartedFirstTime",hasStartedFirstTime? 1:0);
            notes = startingNotes;
            PlayerPrefs.SetInt("NoteCurrency", notes);
            PlayerPrefs.SetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
            PlayerPrefs.SetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
            PlayerPrefs.SetInt("UpgradeFrenzy" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost);
        }

        //If file doesn't exist. Create empty JSONObject.

        if (!File.Exists(Application.persistentDataPath + "/PlayerData.json"))
        {
            playerStatsJson = new JSONObject();
        }
        else
        {
            LoadData();
        }
        notesText.text = notes.ToString();
        UpdateTextUpgrade();

        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth = PlayerPrefs.GetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage = PlayerPrefs.GetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost = PlayerPrefs.GetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);

    }

    private void UpdateTextUpgrade()
    {
        frenzyText.text = PlayerPrefs.GetInt("UpgradeFrenzy" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost).ToString();
        damageText.text = PlayerPrefs.GetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage).ToString();
        healthText.text = PlayerPrefs.GetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth).ToString();
    }

    public void BuyCharacter()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            var pay = characters[i].GetComponent<CharacterStats>().notesToPay;
            if (characters[i].activeSelf == true)
            {
                if (notes >= pay && !characters[i].GetComponent<CharacterStats>().hasBeenBought)
                {
                    characters[i].GetComponent<CharacterStats>().hasBeenBought = true;
                    notes -= pay;
                    notesText.text = notes.ToString();
                    lockScreen.SetActive(false);
                    cantBuyCharacter.gameObject.SetActive(false);
                    PlayerPrefs.SetInt("boughtCharacter" + currentCharacterSelected, characters[i].GetComponent<CharacterStats>().hasBeenBought ? 1 : 0);
                    PlayerPrefs.SetInt("NoteCurrency", notes);
                    SelectCharacter();
                    boughtCharacterSound.clip = boughtCharacterClips[1];
                    boughtCharacterSound.Play();
                }
            }
        }
    }
    public void BuyCharacterWithMoney()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            characters[i].GetComponent<CharacterStats>().hasBeenBought = true;
            notesText.text = notes.ToString();
            lockScreen.SetActive(false);
            cantBuyCharacter.gameObject.SetActive(false);
            PlayerPrefs.SetInt("boughtCharacter" + currentCharacterSelected, characters[i].GetComponent<CharacterStats>().hasBeenBought ? 1 : 0);
            SelectCharacter();
            boughtCharacterSound.clip = boughtCharacterClips[0];
            boughtCharacterSound.Play();
        }
    }

    public void RetrieveStats(int i)
    {
        stats = characters[i].GetComponent<CharacterStats>().stats;        
    }

    public void SelectCharacter()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            //Checks if the character has been bought.
            if (characters[currentCharacterSelected].GetComponent<CharacterStats>().hasBeenBought)
            {
                lockScreen.SetActive(false);
                cantBuyCharacter.gameObject.SetActive(false);
                characters[i].SetActive(i == currentCharacterSelected);
                characters[currentCharacterSelected].GetComponentInChildren<SkinnedMeshRenderer>().material = unlockedMaterials[currentCharacterSelected];

                characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth = PlayerPrefs.GetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
                characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage = PlayerPrefs.GetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
                characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost = PlayerPrefs.GetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
            }
            else
            {
                //set the lock icon active and the character active
                //Needs two instances because unity is a very nice program.
                characters[currentCharacterSelected].GetComponentInChildren<SkinnedMeshRenderer>().material = lockedMaterial;
                characters[i].SetActive(i == currentCharacterSelected);
                lockScreen.SetActive(true);
                cantBuyCharacter.gameObject.SetActive(true);
                characters[i].SetActive(i == currentCharacterSelected);
            }
        }
        PlayerPrefs.SetInt("currentSelectedCharacter", currentCharacterSelected);
        RetrieveStats(currentCharacterSelected);
        UpdateTextUpgrade();
        savedPlayerName = stats.playerName;
    }
    public void SelectCharacterMinus()
    {
        if (!hasUpgraded)
        {
            currentCharacterSelected = Mathf.Clamp(--currentCharacterSelected, 0, 2);
            SelectCharacter();
            PlayerPrefs.SetInt("currentSelectedCharacter", currentCharacterSelected);
        }
    }
    public void SelectCharacterPlus()
    {
        if (!hasUpgraded)
        {
            currentCharacterSelected = Mathf.Clamp(++currentCharacterSelected, 0, 2);
            SelectCharacter();
            PlayerPrefs.SetInt("currentSelectedCharacter", currentCharacterSelected);
        }
    }

    private void RemoveUpgrade(int upgradedStat)
    {
        switch (upgradedStat)
        {
            default:
                healthMinusButton.SetActive(true);
                break;
            case 0:
                healthMinusButton.SetActive(true);
                break;
            case 1:
                DamageMinusButton.SetActive(true);
                break;
            case 2:
                FrenzyMinusButton.SetActive(true);
                break;
        }
    }
    private void RemoveButton()
    {
        if (amountOfHealthUpgrades <= 0)
        {
            healthMinusButton.SetActive(false);
        }
        if (amountOfFrenzyUpgrades <= 0)
        {
            FrenzyMinusButton.SetActive(false);
        }
        if (amountofDamageUpgrades <= 0)
        {
            DamageMinusButton.SetActive(false);
        }
        if (hasSaved)
        {
            healthMinusButton.SetActive(false);
            DamageMinusButton.SetActive(false);
            FrenzyMinusButton.SetActive(false);
            amountofDamageUpgrades = 0;
            amountOfFrenzyUpgrades = 0;
            amountOfHealthUpgrades = 0;
        }
    }
    public void RevertUpgrades(int statsToUpgrade)
    {
        if (characters[currentCharacterSelected].GetComponent<CharacterStats>().hasBeenBought)
        {
            if (amountOfHealthUpgrades > 0)
            {
                //Checks which upgrade you want to buy.
                //0 is health, 1 is damage, 2 is frenzy.
                if (statsToUpgrade == 0)
                {
                    stats.health -= healthUpgrade;
                    notes += characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth - upgradeNotesAmount;
                    characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth -= upgradeNotesAmount;
                    PlayerPrefs.SetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
                    --amountOfHealthUpgrades;
                    RemoveButton();
                }
            }
            if (amountofDamageUpgrades > 0)
            {
                if (statsToUpgrade == 1)
                {
                    stats.attackDamage -= damageUpgrade;
                    notes += characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage - upgradeNotesAmount;
                    characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage -= upgradeNotesAmount;
                    PlayerPrefs.SetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
                    --amountofDamageUpgrades;
                    RemoveButton();
                }
            }
            if (amountOfFrenzyUpgrades > 0)
            {
                if (statsToUpgrade == 2)
                {
                    stats.maxFrenzy -= frenzyUpgrade;
                    notes += characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost - upgradeNotesAmount;
                    characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost -= upgradeNotesAmount;
                    PlayerPrefs.SetInt("UpgradeFrenzy" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost);
                    --amountOfFrenzyUpgrades;
                    RemoveButton();
                }
            }
        }
        if (amountofDamageUpgrades <= 0 && amountOfFrenzyUpgrades <= 0 && amountOfHealthUpgrades <= 0)
        {
            confirmUpgrade.SetActive(false);
            hasUpgraded = false;
        }
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
        notesText.text = notes.ToString();
        UpdateTextUpgrade();
    }

    public void ChangeName(string name)
    {
        stats.playerName = name;

        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }

    public void UpgradeStats(int statsToUpgrade)
    {
        //statsToUpgrade checks which button is pressed and upgrades
        //according to the number!
        notesText.text = notes.ToString();
        //Checks if notes are above 0 so we don't get any negative values.
        if (notes > 0)
        {
            if (characters[currentCharacterSelected].GetComponent<CharacterStats>().hasBeenBought)
            {
                if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth)
                {
                    //Checks which upgrade you want to buy.
                    //0 is health, 1 is damage, 2 is frenzy.
                    if (statsToUpgrade == 0)
                    {
                        stats.health += healthUpgrade;
                        notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth;
                        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth += upgradeNotesAmount;
                        PlayerPrefs.SetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
                        RemoveUpgrade(0);
                        amountOfHealthUpgrades++;
                    }
                }
                if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage)
                {
                    if (statsToUpgrade == 1)
                    {
                        stats.attackDamage += damageUpgrade;
                        notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage;
                        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage += upgradeNotesAmount;
                        PlayerPrefs.SetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
                        RemoveUpgrade(1);
                        amountofDamageUpgrades++;
                    }
                }
                if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost)
                {
                    if (statsToUpgrade == 2)
                    {
                        stats.maxFrenzy += frenzyUpgrade;
                        notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost;
                        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost += upgradeNotesAmount;
                        PlayerPrefs.SetInt("UpgradeFrenzy" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost);
                        RemoveUpgrade(2);
                        amountOfFrenzyUpgrades++;
                    }
                }
                confirmUpgrade.SetActive(true);
            }
        }
        if (amountofDamageUpgrades > 0 || amountOfFrenzyUpgrades > 0 || amountOfHealthUpgrades > 0)
        {
            hasUpgraded = true;
        }
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
        notesText.text = notes.ToString();
        UpdateTextUpgrade();
    }
    #region SavingAndLoadingStats
    public void SaveData()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";

        JSONObject playerStats = new JSONObject();

        playerStats.Add("Health", stats.health);
        playerStats.Add("Damage", stats.attackDamage);
        playerStats.Add("Frenzy", stats.maxFrenzy);

        playerStatsJson.Add("character-" + currentCharacterSelected, playerStats);

        confirmUpgrade.SetActive(false);
        StartCoroutine(StatsSaved());

        //Saves the current selected characters health,damage,frenzy and notes.
        PlayerPrefs.SetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
        PlayerPrefs.SetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
        PlayerPrefs.SetInt("UpgradeFrenzy" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost);
        PlayerPrefs.SetInt("currentSelectedCharacter", currentCharacterSelected);
        PlayerPrefs.SetInt("NoteCurrency", notes);

        File.WriteAllText(path, playerStatsJson.ToString());
        hasUpgraded = false;
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";
        string jsonString = File.ReadAllText(path);
        try
        {
            playerStatsJson = (JSONObject)JSON.Parse(jsonString);

            var currentCharacter = playerStatsJson["character-" + currentCharacterSelected];
            if (currentCharacter == null)
                return;
            stats.health = currentCharacter["Health"];
            stats.attackDamage = currentCharacter["Damage"];
            stats.maxFrenzy = currentCharacter["Frenzy"];
            
        }
        catch (System.Exception)
        {
            //Incase there is no save file we throw a base value into stats.
            stats.health = 100;
            stats.attackDamage = 20;
            stats.maxFrenzy = 10;

            playerStatsJson = new JSONObject();
        }
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }

    public void DeleteSaveFile()
    {
        stats.playerName = savedPlayerName;
        stats.health = 100;
        stats.attackDamage = 20;
        stats.maxFrenzy = 10;
        SaveData();

        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }
    #endregion

    public IEnumerator CantBuyChar()
    {
        cantBuyCharacter.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        cantBuyCharacter.gameObject.SetActive(false);
    }
    IEnumerator StatsSaved()
    {
        saveTextBox.SetActive(true);
        hasSaved = true;
        RemoveButton();
        yield return new WaitForSeconds(4);
        hasSaved = false;
        saveTextBox.SetActive(false);
    }
    public void RevertStatUpgrade()
    {
        notesText.text = notes.ToString();
        #region ifCity
        for (int i = 0; i < amountOfHealthUpgrades; i++)
        {
            stats.health -= healthUpgrade;
            --amountOfHealthUpgrades;
            characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth -= upgradeNotesAmount;
            characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
            confirmUpgrade.SetActive(false);
            UpdateTextUpgrade();
            notes += characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth;
            notesText.text = notes.ToString();
            if (amountOfHealthUpgrades <= 0)
            {
                hasUpgraded = false;
            }
        }
        for (int i = 0; i < amountOfFrenzyUpgrades; i++)
        {
            stats.maxFrenzy -= damageUpgrade;
            --amountOfFrenzyUpgrades;
            characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost -= upgradeNotesAmount;
            characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
            UpdateTextUpgrade();
            confirmUpgrade.SetActive(false);
            notes += characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost;
            notesText.text = notes.ToString();
            if (amountOfFrenzyUpgrades <= 0)
            {
                hasUpgraded = false;
            }
        }

        for (int i = 0; i < amountofDamageUpgrades; i++)
        {
            stats.attackDamage -= frenzyUpgrade;
            --amountofDamageUpgrades;
            characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage -= upgradeNotesAmount;
            characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
            confirmUpgrade.SetActive(false);
            UpdateTextUpgrade();
            notes += characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage;
            notesText.text = notes.ToString();
            if (amountofDamageUpgrades <= 0)
            {
                hasUpgraded = false;
            }
        }
        RemoveButton();
    }
        #endregion
}

