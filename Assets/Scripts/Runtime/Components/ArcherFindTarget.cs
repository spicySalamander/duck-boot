﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArcherFindTarget : MonoBehaviour, ICombatTargetFinding
{
    public Vector3 GetTarget(List<Transform> enemies, List<Transform> allies)
    {
        return transform.position;
    }
}