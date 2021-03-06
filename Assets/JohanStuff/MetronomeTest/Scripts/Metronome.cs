using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Metronome : MonoBehaviour
{
    //Timer Events based on the beat
    public delegate void Beat();
    public static event Beat OnBeat;

    public delegate void DownBeat();
    public static event DownBeat OnDownBeat;

    private double downBeatTime = 0;
    private double lastDownBeatTime = 0;

    private double beatTime = 0;
    private double lastBeatTime = 0;

    public double bpm = 140.0F;
    public float gain = 0.5F;
    public int signatureHi = 4;
    public int signatureLo = 4;
    public bool playMetronomeTick = true;

    private double nextTick = 0.0F;
    private float amp = 0.0F;
    private float phase = 0.0F;
    private double sampleRate = 0.0F;
    private int accent;
    private bool running = false;


    void Start()
    {
        Time.timeScale = 1;
        accent = signatureHi;
        double startTick = AudioSettings.dspTime;
        sampleRate = AudioSettings.outputSampleRate;
        nextTick = startTick * sampleRate;
        running = true;
    }

    private void Update()
    {

        if (lastBeatTime == beatTime)
        {
            if (lastDownBeatTime == downBeatTime)
            {
                if (OnDownBeat != null)
                    OnDownBeat();
            }
            else
            {
                if (OnBeat != null)
                    OnBeat();
            }
        }

        downBeatTime = AudioSettings.dspTime;
        beatTime = AudioSettings.dspTime;
    }

    void OnAudioFilterRead(float[] data, int channels)
    {
        if (!running)
            return;

        double samplesPerTick = sampleRate * 60.0F / bpm * 4.0F / signatureLo;
        double sample = AudioSettings.dspTime * sampleRate;

        int dataLen = data.Length / channels;
        int n = 0;

        while (n < dataLen)
        {
            float x = gain * amp * Mathf.Sin(phase);
            int i = 0;
            while (i < channels)
            {
                data[n * channels + i] += x;
                i++;
            }
            while (sample + n >= nextTick)
            {
                nextTick += samplesPerTick;
                if (playMetronomeTick)
                    amp = 1.0F;
                if (++accent > signatureHi)
                {
                    accent = 1;
                    if (playMetronomeTick)
                        amp *= 2.0F;
                    lastDownBeatTime = AudioSettings.dspTime;

                }

                lastBeatTime = AudioSettings.dspTime;

                // Debug.Log("Tick: " + accent + "/" + signatureHi);
            }
            if (playMetronomeTick)
            {
                phase += amp * 0.3F;
                amp *= 0.993F;
            }
            n++;
        }
    }
}