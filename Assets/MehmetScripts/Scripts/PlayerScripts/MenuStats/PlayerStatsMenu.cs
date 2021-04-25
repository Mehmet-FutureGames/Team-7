using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using SimpleJSON;

public class PlayerStatsMenu : MonoBehaviour
{

    [SerializeField] PlayerStats stats;

    int currentCharacterSelected;

    [SerializeField] List<GameObject> characters = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacters();
        LoadData();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadData();
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
    public void UpgradeStatsHealth()
    {
        stats.health += 5;
        Debug.Log("Upgraded stats!");
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }
    public void UpgradeStatsDamage()
    {
        stats.attackDamage += 5;
        Debug.Log("Upgraded stats!");
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }
    public void UpgradeStatsFrenzy()
    {
        stats.maxFrenzy += 5;
        Debug.Log("Upgraded stats!");
        characters[currentCharacterSelected].GetComponent<CharacterStats>().UpdateText();
    }

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
}
