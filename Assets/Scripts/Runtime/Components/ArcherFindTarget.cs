using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArcherFindTarget : MonoBehaviour, ICombatTargetFinding
{
    public Vector3 GetTargetPosition(List<CombatUnitController> enemies, List<CombatUnitController> allies, float attackRange)
    {
        // get the middle point of all enemies
        Vector3 closestEnemyPosition = new Vector3();
        Vector3 alliesMidPoint = new Vector3();
        Vector3 targetPosition = new Vector3();
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if ((enemies[i].transform.position - transform.position).magnitude < (closestEnemyPosition - transform.position).magnitude)
                {
                    closestEnemyPosition = enemies[i].transform.position;
                }
            }

            // get the middle point of all Allies
            if (allies.Count > 1)
            {
                for (int i = 0; i < allies.Count; i++)
                {
                    if (allies[i] != transform && allies[i].m_Type != CombatManager.UnitTypes.Assassin)
                    {
                        alliesMidPoint += allies[i].transform.position;
                    }
                }
                alliesMidPoint /= allies.Count - 1;
                targetPosition = closestEnemyPosition + (alliesMidPoint - closestEnemyPosition).normalized * attackRange;
            }
            else
            {
                targetPosition = closestEnemyPosition + (transform.position - closestEnemyPosition).normalized * attackRange;
            }
            targetPosition = BoundingCorrection(targetPosition);
            return targetPosition;
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

    private Vector3 BoundingCorrection(Vector3 targetPosition)
    {
        return targetPosition;
    }
}
