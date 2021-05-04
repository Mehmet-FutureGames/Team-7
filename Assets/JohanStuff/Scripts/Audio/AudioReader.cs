using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class AudioReader : MonoBehaviour
{
    AudioSource audioSource;
    public static float[] samples = new float[512];
    public static float[] freqBand = new float[8];
    public static float[] bandBuffer = new float[8];
    float[] bufferDecrease = new float[8];
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GetSpectrumAudioSource();
        MakeFreqBands();
        BandBuffer();
    }

    void GetSpectrumAudioSource()
    {
        audioSource.GetSpectrumData(samples, 0, FFTWindow.BlackmanHarris);
    }

    void BandBuffer()
    {
        /*
        for (int i = 0; i < 16; i++)
        {
            if(freqBand16[i] > bandBuffer16[i])
            {
                bandBuffer16[i] = freqBand16[i];
                bufferDecrease16[i] = 0.005f;
            }
            if (freqBand16[i] < bandBuffer16[i])
            {
                bandBuffer16[i] -= bufferDecrease16[i];
                bufferDecrease16[i] *= 1.2f;
            }
        }
        */
        
        for (int i = 0; i < 8; i++)
        {
            if(freqBand[i] > bandBuffer[i])
            {
                bandBuffer[i] = freqBand[i];
                bufferDecrease[i] = 0.005f;
            }
            if(freqBand[i] < bandBuffer[i])
            {
                bandBuffer[i] -= bufferDecrease[i];
                bufferDecrease[i] *= 1.2f;
            }
        }
        
        
    }

    void Make16FreqBands()
    {


        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2, i) * 2;
            if(i == 15)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            average = average / count;
            freqBand[i] = average * 10;
        }
    }

    void MakeFreqBands()
    {
        int count = 0;
        for (int i = 0; i < 8; i++)
        {
            float average = 0;
            int sampleCount = (int)Mathf.Pow(2f, i) * 2;
            if (i == 7)
            {
                sampleCount += 2;
            }
            for (int j = 0; j < sampleCount; j++)
            {
                average += samples[count] * (count + 1);
                count++;
            }
            Debug.Log(sampleCount);
            average = average / count;

            freqBand[i] = average * 10;
        }
    }
}
