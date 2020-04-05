using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatTargetFinding
{
    Vector3 GetTargetPosition(List<CombatUnitController> enemies, List<CombatUnitController> allies, float attackRange);

    CombatUnitController GetAttackTarget(List<CombatUnitController> enemies, Vector3 targetPosition);
}
