using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [Tooltip("Movement speed multiplicative")]
    public float playerMovementSpeedMultiplier;
    [Tooltip("Movement speed additive")]
    public float playerMovementSpeedModifier;

    public LayerMask LayerToMovement;
}
