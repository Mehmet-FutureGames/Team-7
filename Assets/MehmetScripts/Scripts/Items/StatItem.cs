using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    HealthUpgrade,
    AttackUpgrade,
    DashAttackUpgrade,
    FrenzyUpgrade
}

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Items", order = 1)]

public class StatItem : ScriptableObject
{
    public string nameItem;
    public GameObject itemModel;
    public float upgradeAmount;
    public int itemCostCoins;
    public int itemCostNotes;
    [Space]
    public ItemType itemType;
}
