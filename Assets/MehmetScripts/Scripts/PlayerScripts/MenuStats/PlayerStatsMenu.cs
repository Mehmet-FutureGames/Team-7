using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsMenu : MonoBehaviour
{

    [SerializeField] PlayerStats stats;

    int currentCharacterSelected;

    [SerializeField] List<GameObject> characters = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ChangeCharacters();        
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
}
