using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AssassinFindTarget : MonoBehaviour, ICombatTargetFinding
{

    private Transform m_CurrentTarget;

    public Vector3 GetTargetPosition(List<CombatUnitController> enemies, List<CombatUnitController> allies, float attackRange)
    {
        Transform farthestEnemyPosition = transform;
        if (enemies.Count > 0)
        {
            for (int i = 0; i < enemies.Count; i++)
            {
                if ((enemies[i].transform.position - transform.position).magnitude > (farthestEnemyPosition.position - transform.position).magnitude)
                {
                    farthestEnemyPosition = enemies[i].transform;
                }
            }
            return farthestEnemyPosition.position - (transform.position - farthestEnemyPosition.position) * attackRange;
        }
        else
        {
            Debug.LogError($"there's no enemy exist but {gameObject.name} still tring to find a target position");
            return Vector3.zero;
        }
    }

    public Transform GetAttackTarget(List<CombatUnitController> enemies, Vector3 targetPosition)
    {
        if (null == m_CurrentTarget)
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
            m_CurrentTarget = attackTarget;
            return attackTarget;
        }
        else if (!m_CurrentTarget.gameObject.activeInHierarchy)
        {
            m_CurrentTarget = null;
            return GetAttackTarget(enemies, targetPosition);
        }
        else
        {

            return m_CurrentTarget;
        }
    }
}
