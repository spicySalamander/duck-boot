using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public enum UnitTypes { Tank, Archer, Assassin }
    public float[] m_AttackRanges;
    public float[] m_AttackCoolDown;
    public List<System.Type> m_CombatStyles;
    public List<Sprite> m_CombatUnitSprites;
    public List<CombatUnitController>[] m_UnitPool;
    public List<CombatUnitController> m_Team1;
    public List<CombatUnitController> m_Team2;
    public Color[] m_TeamColors;

    public void Awake()
    {
        m_UnitPool = new List<CombatUnitController>[2];
        m_UnitPool[0] = m_Team1;
        m_UnitPool[1] = m_Team2;
        m_CombatStyles = new List<System.Type>()
        {
            typeof(TankFindTarget), typeof(ArcherFindTarget), typeof(AssassinFindTarget)
        };
    }

    public void AddUnit(int teamNum, CombatUnitController unit)
    {
        m_UnitPool[teamNum].Add(unit);
    }
}
