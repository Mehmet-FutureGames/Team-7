using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeEnemyType : MonoBehaviour
{
    public static InitializeEnemyType Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void Initialize(Enemy enemy, StateMachine stateMachine)
    {
        switch (enemy.enemyType)
        {
            case EnemyType.Base:
                enemy.moveState = new BaseMoveState(enemy, stateMachine);
                enemy.combatPhase1 = new BaseCombatP1(enemy, stateMachine);
                enemy.combatPhase2 = new BaseCombatP2(enemy, stateMachine);
                enemy.combatPhase3 = new BaseCombatP3(enemy, stateMachine);
                break;
            case EnemyType.Tanky:
                enemy.moveState = new TankMoveState(enemy, stateMachine);
                enemy.combatPhase1 = new TankCombatP1(enemy, stateMachine);
                enemy.combatPhase2 = new TankCombatP2(enemy, stateMachine);
                enemy.combatPhase3 = new TankCombatP3(enemy, stateMachine);
                enemy.combatPhase4 = new TankCombatP4(enemy, stateMachine);
                enemy.combatPhase5 = new TankCombatP5(enemy, stateMachine);
                enemy.combatPhase6 = new TankCombatP6(enemy, stateMachine);
                break;
            case EnemyType.Caster:
                enemy.moveState = new CasterMoveState(enemy, stateMachine);
                enemy.combatPhase1 = new CasterCombatP1(enemy, stateMachine);
                enemy.combatPhase2 = new CasterCombatP2(enemy, stateMachine);
                enemy.combatPhase3 = new CasterCombatP3(enemy, stateMachine);
                enemy.combatPhase4 = new CasterCombatP4(enemy, stateMachine);
                break;
            case EnemyType.Lobber:
                enemy.moveState = new LobberMoveState(enemy, stateMachine);
                enemy.combatPhase1 = new LobberCombatP1(enemy, stateMachine);
                enemy.combatPhase2 = new LobberCombatP2(enemy, stateMachine);
                enemy.combatPhase3 = new LobberCombatP3(enemy, stateMachine);
                break;
            case EnemyType.Ninja:
                enemy.moveState = new NinjaMoveState(enemy, stateMachine);
                enemy.combatPhase1 = new NinjaCombatP1(enemy, stateMachine);
                enemy.combatPhase2 = new NinjaCombatP2(enemy, stateMachine);
                enemy.combatPhase3 = new NinjaCombatP3(enemy, stateMachine);
                break;
            default:
                break;
        }
    }
}
