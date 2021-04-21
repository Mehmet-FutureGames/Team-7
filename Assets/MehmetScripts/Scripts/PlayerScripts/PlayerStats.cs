using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New PlayerStats", menuName = "ScriptableObjects/PlayerStats", order = 1)]
    public class PlayerStats : ScriptableObject
    {
        public GameObject playerModel;
        public GameObject playerDamageText;
        public string playerName;
        [Space]
        public float movementSpeed;
        public float attackDamage;
        public float health;
        public float dashDamage;
        public float meleeAttackDuration;
        public float dashAttackDuration;
        public float dashAttackCooldown;
        [Space]
        public int distanceToClick;
    }

