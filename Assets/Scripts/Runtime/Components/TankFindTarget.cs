using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFindTarget : MonoBehaviour, ICombatTargetFinding
{
    public Vector3 GetTargetPosition(List<CombatUnitController> enemies, List<CombatUnitController> allies, float attackRange)
    {
        // get the middle point of all enemies
        Vector3 enemyMidPoint = new Vector3();
        Vector3 alliesMidPoint = new Vector3();
        Vector3 targetPosition = new Vector3();
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                enemyMidPoint += enemies[i].transform.position;
            }
            enemyMidPoint /= enemies.Count;

            // get the middle point of all Allies
            if (allies.Count > 1)
            {
                for (int i = 0; i < allies.Count; i++)
                {
                    if (allies[i] != transform)
                    {
                        alliesMidPoint += allies[i].transform.position;
                    }
                }
                alliesMidPoint /= allies.Count - 1;
                targetPosition = (enemyMidPoint + alliesMidPoint) * .5f;
            }
            else
            {
                targetPosition = enemyMidPoint;
            }

            return targetPosition + (transform.position - targetPosition).normalized * attackRange;
        }
        else
        {
            Debug.LogError($"there's no enemy exist but {gameObject.name} still tring to find a target position");
            return Vector3.zero;
        }
    }

    public Transform GetAttackTarget(List<CombatUnitController> enemies, Vector3 targetPosition)
    {
        Transform attackTarget = enemies[0].transform;
        if (enemies.Count > 1)
        {
            for (int i = 1; i < enemies.Count; i++)
            {
                attackTarget = (enemies[i].transform.position - targetPosition).magnitude < (attackTarget.position - targetPosition).magnitude ? 
                    enemies[i].transform : attackTarget;
            }
        }
        return attackTarget;
    }
}
