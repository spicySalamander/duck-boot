using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatTargetFinding
{
    Vector3 GetTarget(List<Transform> enemies, List<Transform> allies); 
}
