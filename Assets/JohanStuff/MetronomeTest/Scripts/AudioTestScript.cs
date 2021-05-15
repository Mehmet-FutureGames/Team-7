using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AudioTestScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentDelay;
    [SerializeField] TMP_InputField manualDelay;
    public CalibrationSaver save;
    AudioSource audioSource;
    float storedVal;
    private float value;
    private float nonAbsVal;
    public float nonAbsAverage;
    public float average;
    double beatVal;
    double inputVal;
    double difference;
    public List<double> inputValList = new List<double>();
    public List<double> beatValList = new List<double>();
    public List<float> storedValues = new List<float>();
    public List<float> nonAbsList = new List<float>();
    Metronome metronome;
    int counter;
    bool hasStartedTapping;
    bool hasAddedList;
    [SerializeField]SpriteRenderer circleIndicator;
    public bool HasStartedTapping
    {
        get { return hasStartedTapping; }
        set
        {
            hasStartedTapping = value;
            if (hasStartedTapping)
            {
                circleIndicator.color = new Color32(29, 27, 117,255);
            }
            else
            {
                circleIndicator.color = new Color(1, 1, 1);
            }
        }
    }

    [SerializeField] GameObject saveButtonMenu;
    [SerializeField] GameObject saveButtonSettings;

    private void Start()
    {
        if (StartedGame.gameStartedFirstTime)
        {
            saveButtonMenu.SetActive(true);
            saveButtonSettings.SetActive(false);
        }
        else
        {
            saveButtonMenu.SetActive(false);
            saveButtonSettings.SetActive(true);
        }

        if (FindObjectOfType<MusicSingleton>() != null)
        {
            FindObjectOfType<MusicSingleton>().DestroyThis();
        }
        metronome = FindObjectOfType<Metronome>();
        Metronome.OnBeat += OnBeat;
        Metronome.OnDownBeat += OnDownBeat;
        counter = 0;
        manualDelay.text = (save.delay * 1000).ToString("F0");
        currentDelay.text = "Current delay: " + manualDelay.text + "ms";
    }
    private void Update()
    {
        if (((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.X)) && counter < 11) && metronome.enabled)
        {
            if (HasStartedTapping)
            {
                inputVal = Time.time;
                inputValList.Add(inputVal);
                Debug.Log(inputVal);
                counter++;
            }
            HasStartedTapping = true;
        }
        if((inputValList.Count == 10 && beatValList.Count == 10) && !hasAddedList)
        {
            for (int i = 0; i < inputValList.Count; i++)
            {
                storedVal = Mathf.Abs((float)inputValList[i] - (float)beatValList[i]);
                storedValues.Add(storedVal);
            }
            for (int i = 0; i < storedValues.Count; i++)
            {
                value += storedValues[i];
            }
            average = value / storedValues.Count;
            manualDelay.text = (average * 1000).ToString("F0");
            
            metronome.enabled = false;
            hasAddedList = true;
            HasStartedTapping = false;
        }
        if(inputValList.Count > 10 || beatValList.Count > 10)
        {
            RestartTest();
        }
    }
    public void RestartTest()
    {
        value = 0;
        hasAddedList = false;
        HasStartedTapping = false;
        counter = 0;
        storedValues.Clear();
        inputValList.Clear();
        beatValList.Clear();
        if(metronome.enabled == false)
        {
            metronome.enabled = true;
        }
    }
    public void SaveTest()
    {
        if(manualDelay.text != null)
        {
            save.delay = (float.Parse(manualDelay.text) / 1000);
        }
        currentDelay.text = "Current delay: " + (save.delay * 1000).ToString("F0") + "ms";
        //save.delay = average;
    }
    void OnBeat()
    {


    }
    void OnDownBeat()
    {
        if (HasStartedTapping && counter < 10)
        {
            beatVal = Time.time;
            beatValList.Add(beatVal);
            Debug.Log("OnBeat");
        }
    }
}
