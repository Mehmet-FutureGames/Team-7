using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioStuff : MonoBehaviour
{
    public enum Samples
    {
        _128,
        _256,
        _512,
        _1024
    }


    public static float[] spectrum;
    public static float spectrumValue;
    public Samples chooseSamples;
    public FFTWindow fFTWindow;
    private float samples;

    public int spectrumIndex;
    /*
    private void Start()
    {
        SetSamples();
        spectrum = new float[(int)samples];
    }


    void Update()
    {
        spectrumIndex = Mathf.Clamp(spectrumIndex, 0, (int)samples - 1);
        AudioListener.GetSpectrumData(spectrum, 0, fFTWindow);
        if (spectrum != null && spectrum.Length > 0)
        {
            spectrumValue = spectrum[spectrumIndex] * 100;
        }

    }
    */
    void SetSamples()
    {
        switch (chooseSamples)
        {
            case Samples._128:
                samples = 128;
                break;
            case Samples._256:
                samples = 256;
                break;
            case Samples._512:
                samples = 512;
                break;
            case Samples._1024:
                samples = 1024;
                break;
        }
    }
}