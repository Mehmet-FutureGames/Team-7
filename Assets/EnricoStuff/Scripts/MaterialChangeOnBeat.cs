using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChangeOnBeat : MonoBehaviour
{
    NotePublisher notePublisher;
    MovePlayer movePlayer;
    public Material material;
    public Vector2 offSet1;
    public Vector2 offSet2;
    bool colorChange;

    private void OnEnable()
    {
        notePublisher = FindObjectOfType<NotePublisher>();
        movePlayer = FindObjectOfType<MovePlayer>();
        //movePlayer.playerRegMove += EventUpdate;
        notePublisher.noteNotHit += EventUpdate;
        notePublisher.noteHitBlock += EventUpdate;
        notePublisher.noteHitAttack += EventUpdate;
        movePlayer.playerRegMove += EventUpdate;
        material.SetTextureOffset("_BaseMap", offSet1);
    }

    private void EventUpdate()
    {
        Debug.Log("colorChange");
        if(colorChange == true)
        {
            material.SetTextureOffset("_BaseMap",  offSet1);
            colorChange = false;
        }

        else 
        {
            material.SetTextureOffset("_BaseMap", offSet2);
            colorChange = true;
        }
        
    }
}
