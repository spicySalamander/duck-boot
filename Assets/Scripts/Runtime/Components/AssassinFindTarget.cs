using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssassinFindTarget : MonoBehaviour, ICombatTargetFinding
{
    public Vector3 GetTargetPosition(List<CombatUnitController> enemies, List<CombatUnitController> allies, float attackRange)
    {
        Vector3 farthestEnemyPosition = transform.position;
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if ((enemies[i].transform.position - transform.position).magnitude > (farthestEnemyPosition - transform.position).magnitude)
                {
                    farthestEnemyPosition = enemies[i].transform.position;
                }
            }
            return farthestEnemyPosition - (transform.position - farthestEnemyPosition) * attackRange;
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
