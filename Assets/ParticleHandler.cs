using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour
{
    ParticleSystem particleSystem;
    MovePlayer movePlayer;
    bool isEnabled;
    // Start is called before the first frame update
    void Start()
    {
        movePlayer = FindObjectOfType<MovePlayer>();
        particleSystem = GetComponent<ParticleSystem>();
        particleSystem.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (movePlayer.isMoving)
        {
            if (!isEnabled)
            {
                particleSystem.Play();
                isEnabled = true;
            }
            
        }
        else
        {
            if (isEnabled)
            {
                particleSystem.Stop();
                isEnabled = false;
            }
        }
    }
}
