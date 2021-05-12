using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class PlayerStatsMenu : MonoBehaviour
{
    [SerializeField] int amountOfHealthUpgrades;
    [SerializeField] int amountofDamageUpgrades;
    [SerializeField] int amountOfFrenzyUpgrades;


    public static bool hasStartedFirstTime = false;

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
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.sources.Clear();
        AudioManager.audioClips.Clear();
        hasStartedFirstTime = PlayerPrefs.GetInt("hasStartedFirstTime") == 1;
        characters[0].SetActive(true);
        UpdateTextUpgrade();
        if (characters[PlayerPrefs.GetInt("currentSelectedCharacter")].GetComponent<CharacterStats>().hasBeenBought)
        {
            cantBuyCharacter.gameObject.SetActive(false);
            lockScreen.SetActive(false);
        }
        else
        {
            cantBuyCharacter.gameObject.SetActive(true);
            lockScreen.SetActive(true);
        }


        ChangeCharacters();

        notes = PlayerPrefs.GetInt("NoteCurrency");
        notesText = GameObject.Find("NotesAmount").GetComponent<TextMeshProUGUI>();
        UpdateTextUpgrade();
        if (!hasStartedFirstTime)
        {
            hasStartedFirstTime = true;
            PlayerPrefs.SetInt("hasStartedFirstTime",hasStartedFirstTime? 1:0);
            notes = startingNotes;
            PlayerPrefs.SetInt("NoteCurrency", notes);
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
    }

    private void UpdateTextUpgrade()
    {
        frenzyText.text = characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost.ToString();
        damageText.text = characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage.ToString();
        healthText.text = characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth.ToString();
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
                    PlayerPrefs.SetInt("boughtCharacter" + currentCharacterSelected, characters[i].GetComponent<CharacterStats>().hasBeenBought ? 1 : 0);
                    PlayerPrefs.SetInt("NoteCurrency", notes);
                }
            }
        }
    }

    public void ChangeCharacters()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            if (characters[i].activeSelf == true)
            {
                stats = characters[i].GetComponent<CharacterStats>().stats;
            }
        }
    }

    public void SelectCharacter()
    {
        for (int i = 0; i < characters.Count; i++)
        {
            //Checks if the character has been bought.
            if (characters[currentCharacterSelected].GetComponent<CharacterStats>().hasBeenBought)
            {
                lockScreen.SetActive(false);
                characters[i].SetActive(i == currentCharacterSelected);
            }
            else
            {
                //set the lock icon active and the character active
                //Needs two instances because unity is a very nice program.
                characters[i].SetActive(i == currentCharacterSelected);
                lockScreen.SetActive(true);
                characters[i].SetActive(i == currentCharacterSelected);
            }
        }
        PlayerPrefs.SetInt("currentSelectedCharacter", currentCharacterSelected);
        ChangeCharacters();
        UpdateTextUpgrade();
        savedPlayerName = stats.playerName;
    }
    public void SelectCharacterMinus()
    {
        if (!hasUpgraded)
        {
            currentCharacterSelected = Mathf.Clamp(--currentCharacterSelected, 0, 2);
            SelectCharacter();
        }
    }
    public void SelectCharacterPlus()
    {
        if (!hasUpgraded)
        {
            currentCharacterSelected = Mathf.Clamp(++currentCharacterSelected, 0, 2);
            SelectCharacter();
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
                    stats.health -= 5;
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
                    stats.attackDamage -= 5;
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
                    stats.maxFrenzy -= 5;
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
                        stats.health += 5;
                        notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth;
                        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth += upgradeNotesAmount;
                        RemoveUpgrade(0);
                        amountOfHealthUpgrades++;
                    }
                }
                if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage)
                {
                    if (statsToUpgrade == 1)
                    {
                        stats.attackDamage += 5;
                        notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage;
                        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage += upgradeNotesAmount;
                        RemoveUpgrade(1);
                        amountofDamageUpgrades++;
                    }
                }
                if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost)
                {
                    if (statsToUpgrade == 2)
                    {
                        stats.maxFrenzy += 5;
                        notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost;
                        characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost += upgradeNotesAmount;
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

            var currentCharacter = playerStatsJson["character-" + PlayerPrefs.GetInt("currentSelectedCharacter")];
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

    public IEnumerator cantbuyChar()
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
            stats.health -= 5;
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
            stats.maxFrenzy -= 5;
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
            stats.attackDamage -= 5;
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

