using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrenzy : MonoBehaviour
{
    EnemyPublisher enemyPublisher;
    //[SerializeField] Text text;
    public Image frenzyBar;
    public float maxFrenzy;
    [SerializeField] float minFrenzy;
    [SerializeField] private float currentFrenzy;

    public float CurrentFrenzy
    {
        get { return currentFrenzy; }
        set 
        {
            currentFrenzy = value;
            frenzyBar.fillAmount = currentFrenzy / maxFrenzy;
            //text.text = "Frenzy: " + currentFrenzy.ToString();
        }
    }

    private void Awake()
    {
        frenzyBar = GameObject.Find("FrenzyBar").GetComponent<Image>(); ;
        enemyPublisher = FindObjectOfType<EnemyPublisher>();
        enemyPublisher.enemyTakeDamage += AddFrenzy;
        currentFrenzy = 0;
    }

    void AddFrenzy()
    {
        currentFrenzy = Mathf.Clamp(currentFrenzy + 1, minFrenzy, maxFrenzy);
        frenzyBar.fillAmount = currentFrenzy / maxFrenzy;
        //text.text = "Frenzy: " + currentFrenzy.ToString();
    }


}
