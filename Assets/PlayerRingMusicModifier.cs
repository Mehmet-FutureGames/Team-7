using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRingMusicModifier : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Vector4 startColor;
    Vector3 startSize;
    [Range(0,10f)]
    public float alphaMult;
    [Range(0, 100)]
    public int sizeMult;
    [Range(0,7)]
    public int alphaBand, sizeBand;
    
    void Start()
    {
        startSize = transform.localScale;
        spriteRenderer = GetComponent<SpriteRenderer>();
        startColor = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0);
    }

    private void FixedUpdate()
    {
        transform.localScale = Vector3.Lerp(startSize, startSize * AudioReader.bandBuffer[sizeBand] * sizeMult, 10 * Time.fixedDeltaTime);
        spriteRenderer.color = Vector4.Lerp(startColor, new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, AudioReader.bandBuffer[alphaBand] * alphaMult), 10 * Time.fixedDeltaTime);
    }
}
