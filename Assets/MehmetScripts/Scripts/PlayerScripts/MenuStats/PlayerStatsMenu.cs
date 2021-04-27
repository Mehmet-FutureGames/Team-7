using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;
using TMPro;

public class PlayerStatsMenu : MonoBehaviour
{

    [SerializeField] PlayerStats stats;

    TextMeshProUGUI notesText;

    [SerializeField] int currentCharacterSelected;

    JSONObject playerStatsJson;

    [SerializeField] GameObject lockScreen;

    string savedPlayerName;

    int notes;

    [SerializeField] int upgradeNotesAmount;

    [Space]
    [Space]

    [SerializeField] TextMeshProUGUI healthText;

    [SerializeField] TextMeshProUGUI damageText;

    [SerializeField] TextMeshProUGUI frenzyText;

    [SerializeField] List<GameObject> characters = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        currentCharacterSelected = 0;
        PlayerPrefs.SetInt("currentSelectedCharacter", currentCharacterSelected);
        characters[0].SetActive(true);
        ChangeCharacters();
        notes = PlayerPrefs.GetInt("NoteCurrency");
        notes = 999;
        notesText = GameObject.Find("NotesAmount").GetComponent<TextMeshProUGUI>();
        lockScreen.SetActive(false);
        BuyCharacter();
        savedPlayerName = stats.playerName;
        UpdateTextUpgrade();

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
                if(notes >= pay && !characters[i].GetComponent<CharacterStats>().hasBeenBought)
                {
                    characters[i].GetComponent<CharacterStats>().hasBeenBought = true;
                    notes -= pay;
                    notesText.text = notes.ToString();
                    lockScreen.SetActive(false);
                    PlayerPrefs.SetInt("boughtCharacter" + currentCharacterSelected, characters[i].GetComponent<CharacterStats>().hasBeenBought ? 1 : 0);
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
        currentCharacterSelected = Mathf.Clamp(--currentCharacterSelected, 0, 2);
        SelectCharacter();
    }
    public void SelectCharacterPlus()
    {
        currentCharacterSelected = Mathf.Clamp(++currentCharacterSelected, 0, 2);
        SelectCharacter();
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
            PlayerPrefs.SetInt("NoteCurrency", notes);
            notesText.text = notes.ToString();
        //Checks if notes are above 0 so we don't get any negative values.
        if (notes > 0)
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
                    PlayerPrefs.SetInt("UpgradeHealth" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostHealth);
                    Debug.Log("Upgraded health!");
                }
            }
            if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage)
            {
                if (statsToUpgrade == 1)
                {
                    stats.attackDamage += 5;
                    notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage;
                    characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage += upgradeNotesAmount;
                    PlayerPrefs.SetInt("UpgradeDamage" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesCostDamage);
                    Debug.Log("Upgraded damage!");
                }
            }
            if (notes >= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost)
            {
                if (statsToUpgrade == 2)
                {
                    stats.maxFrenzy += 5;
                    notes -= characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost;
                    characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost += upgradeNotesAmount;
                    PlayerPrefs.SetInt("UpgradeFrenzy" + currentCharacterSelected, characters[currentCharacterSelected].GetComponent<CharacterStats>().notesFrenzyCost);
                    Debug.Log("Upgraded frenzy!");
                }
            }
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

        File.WriteAllText(path, playerStatsJson.ToString());
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";
        string jsonString = File.ReadAllText(path);
        try
        {
            playerStatsJson = (JSONObject)JSON.Parse(jsonString);

            var currentCharacter = playerStatsJson["character-" + currentCharacterSelected];
            stats.health = currentCharacter["Health"];
            stats.attackDamage = currentCharacter["Damage"];
            stats.maxFrenzy = currentCharacter["Frenzy"];
        }
        catch (System.Exception)
        {
            //Incase there is no save file we throw a base value into stats.
            stats.playerName = savedPlayerName;
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
}
