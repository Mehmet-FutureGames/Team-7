using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public enum MovePattern
{
    TowardsPlayer,
    RandomDirection,
    ProximityDetection
}
public enum EnemyType
{
    Base,
    Caster,
    Tanky,
    Lobber,
    Ninja
}
[CreateAssetMenu(fileName = "New EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    public string enemyName;
    [Space]
    public MovePattern movePattern;
    public EnemyType enemyType;
    public GameObject enemyModel;
    public GameObject floatingText;
    [Space(20)]
    public GameObject attackAreaShape;
    public Vector3 attackAreaScale;

    [Space]
    public GameObject attackAreaShape2;
    public Vector3 attackAreaScale2;

    [Space(20)]
    public bool isRanged;
    public float movementSpeed;
    public float moveDistance;
    public float detectionRange;
    [Range(1, 5), Tooltip("Determines how many notes that pass through the hitArea before the enemy moves")]
    public int notesToMove;
    [Space]
    public float attackDamage;
    public float attackRange;
    public float health;
    [Space]
    [Range(0f,100f)]
    public float noteDropChance;
    [Space]
    [Space]
    [Range(0,10)]
    public int coinMinDropCount;
    [Range(0,10)]
    public int coinMaxDropCount;
    public float coinValue;

}


/*[CustomEditor(typeof(EnemyStats))]
public class TestCustomInspector : Editor
{

    public override void OnInspectorGUI()
    {
        EnemyStats script = (EnemyStats)target;

        script.movePattern = (MovePattern)EditorGUILayout.EnumPopup("Move Pattern", script.movePattern);

        if (script.movePattern == MovePattern.TowardsPlayer)
        {
            script.enemyModel = (GameObject)EditorGUILayout.ObjectField("Enemy Model", script.enemyModel, typeof(GameObject), true);
            script.area = (GameObject)EditorGUILayout.ObjectField("area", script.area, typeof(GameObject), true);
            script.enemyName = EditorGUILayout.TextField("Enemy Name", script.enemyName);
            script.movementSpeed = EditorGUILayout.FloatField("Movement Speed", script.movementSpeed);
            script.moveDistance = EditorGUILayout.FloatField("Move Distance", script.moveDistance);
            script.notesToMove = EditorGUILayout.IntField("Notes to move", script.notesToMove);
            script.attackDamage = EditorGUILayout.FloatField("Attack Damage", script.attackDamage);
            script.health = EditorGUILayout.FloatField("Health", script.health);
            script.attackRange = EditorGUILayout.FloatField("Attack Range", script.attackRange);
        }
        if (script.movePattern == MovePattern.RandomDirection)
        {
            script.enemyModel = (GameObject)EditorGUILayout.ObjectField("Enemy Model", script.enemyModel, typeof(GameObject), true);
            script.enemyName = EditorGUILayout.TextField("Enemy Name", script.enemyName);
            script.movementSpeed = EditorGUILayout.FloatField("Movement Speed", script.movementSpeed);
            script.moveDistance = EditorGUILayout.FloatField("Move Distance", script.moveDistance);
            script.notesToMove = EditorGUILayout.IntField("Notes to move", script.notesToMove);
            script.attackDamage = EditorGUILayout.FloatField("Attack Damage", script.attackDamage);
            script.health = EditorGUILayout.FloatField("Health", script.health);
            script.attackRange = EditorGUILayout.FloatField("Attack Range", script.attackRange);
        }
        if (script.movePattern == MovePattern.ProximityDetection)
        {
            script.enemyModel = (GameObject)EditorGUILayout.ObjectField("Enemy Model", script.enemyModel, typeof(GameObject), true);
            script.enemyName = EditorGUILayout.TextField("Enemy Name", script.enemyName);
            script.movementSpeed = EditorGUILayout.FloatField("Movement Speed", script.movementSpeed);
            script.moveDistance = EditorGUILayout.FloatField("Move Distance", script.moveDistance);
            script.notesToMove = EditorGUILayout.IntField("Notes to move", script.notesToMove);
            script.attackDamage = EditorGUILayout.FloatField("Attack Damage", script.attackDamage);
            script.health = EditorGUILayout.FloatField("Health", script.health);
            script.attackRange = EditorGUILayout.FloatField("Attack Range", script.attackRange);
            script.detectionRange = EditorGUILayout.FloatField("Detection Range", script.detectionRange);
        }
    }
}
*/

