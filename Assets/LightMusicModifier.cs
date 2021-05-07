using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMusicModifier : MonoBehaviour
{

    Light light;
    float startIntensity;
    [Range(0,7)]
    public int HertzBand;
    public float multiplier;
    [Range(0,2)]
    public int formula;
    private void Start()
    {
        light = GetComponent<Light>();
        startIntensity = light.intensity;
    }
    private void FixedUpdate()
    {
        if (formula == 0)
        {
            light.intensity = startIntensity + (AudioReader.bandBuffer[HertzBand] * multiplier);
        }
        if(formula == 1){
            light.intensity = startIntensity * AudioReader.bandBuffer[HertzBand] * multiplier;
        }
        if(formula == 2)
        {

        }
        
    }

    
}
