using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankFindTarget : MonoBehaviour, ICombatTargetFinding
{
    public Vector3 GetTarget(List<Transform> enemies, List<Transform> allies)
    {
        // get the middle point of all enemies
        Vector3 enemyMidPoint = new Vector3();
        Vector3 alliesMidPoint = new Vector3();
        for (int i = 0; i < enemies.Count; i++)
        {
            enemyMidPoint += enemies[i].position;
        }
        enemyMidPoint /= enemies.Count;
        
        // get the middle point of all Allies
        if(allies.Count > 1)
        {
            for (int i = 0; i < allies.Count; i++)
            {
                if(allies[i] != transform)
                {
                    alliesMidPoint += allies[i].position;
                }
            }
            alliesMidPoint /= allies.Count - 1; 
            return (enemyMidPoint + alliesMidPoint) * .5f;
        }
        else
        {
            //TODO: attack the nearest enemy when there's no allies surviving?
            return transform.position;
        }
    }
}
