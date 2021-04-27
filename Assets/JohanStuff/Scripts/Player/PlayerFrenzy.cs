using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerFrenzy : MonoBehaviour
{
    public static PlayerFrenzy Instance;
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
        }
    }

    private void Awake()
    {
        if(Instance == null) { Instance = this; }
        frenzyBar = GameObject.Find("FrenzyBar").GetComponent<Image>(); ;
        CurrentFrenzy = 0;
    }

    public void AddFrenzy()
    {
        CurrentFrenzy = Mathf.Clamp(CurrentFrenzy + 1, minFrenzy, maxFrenzy);
        //frenzyBar.fillAmount = currentFrenzy / maxFrenzy;
    }


}
