using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using SimpleJSON;

public class PlayerStatsMenu : MonoBehaviour
{

    [SerializeField] PlayerStats stats;

    [SerializeField] int notesCost;

    Text notesText;

    int currentCharacterSelected;

    int notes;

    [SerializeField] List<GameObject> characters = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacters();
        notes = PlayerPrefs.GetInt("NoteCurrency");
        notesText = GameObject.Find("NotesAmount").GetComponent<Text>();
        if(File.Exists(Application.persistentDataPath + "/PlayerData.json")) LoadData();
        notesText.text = notes.ToString();
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

    public void SelectCharacter(int currentCharacter)
    {
        characters[currentCharacter].SetActive(true);
        #region weirdIfStatementsTOBEFIXED
        if (currentCharacter >= 2)
        {
            characters[2].SetActive(true);
            characters[1].SetActive(false);
            characters[0].SetActive(false);
        }
        else if(currentCharacter <= 0)
        {
            characters[0].SetActive(true);
            characters[2].SetActive(false);
            characters[1].SetActive(false);
        }
        else if(currentCharacter >= 1)
        {
            characters[2].SetActive(false);
            characters[1].SetActive(true);
            characters[0].SetActive(false);
        }
        else
        {
            characters[--currentCharacter].SetActive(false);
        }
        #endregion
        ChangeCharacters();
        currentCharacterSelected = currentCharacter;
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
        if (notes >= notesCost)
        {
            notes -= notesCost;
            PlayerPrefs.SetInt("NoteCurrency", notes);
            notesText.text = notes.ToString();
            if (statsToUpgrade == 0)
            {
                stats.health += 5;
                Debug.Log("Upgraded health!");
            }
            else if (statsToUpgrade == 1)
            {
                stats.attackDamage += 5;
                Debug.Log("Upgraded damage!");
            }
            else if (statsToUpgrade == 2)
            {
                stats.maxFrenzy += 5;
                Debug.Log("Upgraded frenzy!");
            }
        }
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }
    #region SavingAndLoadingStats
    public void SaveData()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";

        JSONObject playerStats = new JSONObject();
        playerStats.Add("Name", stats.playerName);
        playerStats.Add("Health", stats.health);
        playerStats.Add("Damage", stats.attackDamage);
        playerStats.Add("Frenzy", stats.maxFrenzy);

        File.WriteAllText(path, playerStats.ToString());
        Debug.Log(playerStats);
    }
    public void LoadData()
    {
        string path = Application.persistentDataPath + "/PlayerData.json";
        string jsonString = File.ReadAllText(path);
        JSONObject playerStatsJson = (JSONObject)JSON.Parse(jsonString);

        stats.playerName = playerStatsJson["Name"];
        stats.health = playerStatsJson["Health"];
        stats.attackDamage = playerStatsJson["Damage"];
        stats.maxFrenzy = playerStatsJson["Frenzy"];

        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }

    public void DeleteSaveFile()
    {
        File.Delete(Application.persistentDataPath + "/PlayerData.json");

        stats.playerName = "Pick a name!";
        stats.health = 100;
        stats.attackDamage = 20;
        stats.maxFrenzy = 10;

        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();

        Debug.Log("File deleted");
    }
    #endregion
}
