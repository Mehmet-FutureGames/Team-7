using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class AudioTestScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI resultText;
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
    private void Start()
    {
        metronome = FindObjectOfType<Metronome>();
        Metronome.OnBeat += OnBeat;
        Metronome.OnDownBeat += OnDownBeat;
        counter = 0;
    }
    private void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.X)) && counter < 10)
        {
            if (hasStartedTapping)
            {
                inputVal = Time.time;
                inputValList.Add(inputVal);
                Debug.Log(inputVal);
                counter++;
            }
            hasStartedTapping = true;
        }
        if((inputValList.Count == 10 && beatValList.Count == 10) && !hasAddedList)
        {
            for (int i = 0; i < inputValList.Count; i++)
            {
                storedVal = Mathf.Abs((float)inputValList[i] - (float)beatValList[i]);
                storedValues.Add(storedVal);
                nonAbsList.Add((float)inputValList[i] - (float)beatValList[i]);
            }
            for (int i = 0; i < storedValues.Count; i++)
            {
                value += storedValues[i];
                nonAbsVal += nonAbsList[i];
            }
            average = value / storedValues.Count;
            nonAbsAverage = nonAbsVal / nonAbsList.Count;
            resultText.text = "Result: " + (average * 1000).ToString("F0") + "ms Delay";
            metronome.enabled = false;
            hasAddedList = true;
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
        hasStartedTapping = false;
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
        save.delay = average;
    }
    void OnBeat()
    {


    }
    void OnDownBeat()
    {
        if (hasStartedTapping && counter < 10)
        {
            beatVal = Time.time;
            beatValList.Add(beatVal);
            Debug.Log("OnBeat");
        }
    }
}
