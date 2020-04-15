using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Duck Type", menuName = "Duck Type")]
public class DuckType : ScriptableObject
{
    public Sprite sprite;
    public int attackDmg;
    public float attackRange;
    public float motorSpeed;
    public float cooldown;
    public int totalHealth;

    public RuntimeAnimatorController anim;
}
