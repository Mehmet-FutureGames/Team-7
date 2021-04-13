using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    [CreateAssetMenu(fileName = "New PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
    public class PlayerStats : ScriptableObject
    {
        public GameObject playerModel;
        public string playerName;
        [Space]
        public float movementSpeed;
        public float attackDamage;
        public float health;
        public float dashDamage;
        [Space]
        public int distanceToClick;
    }

