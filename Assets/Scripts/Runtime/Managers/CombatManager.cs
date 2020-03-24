using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public enum UnitTypes { Tank, Archer, Assassin }
    public List<System.Type> m_CombatStyles;
    public List<Transform>[] m_UnitPool;
    public List<Transform> m_Team1;
    public List<Transform> m_Team2;

    public void Awake()
    {
        m_UnitPool = new List<Transform>[2];
        m_UnitPool[0] = m_Team1;
        m_UnitPool[1] = m_Team2;
        m_CombatStyles = new List<System.Type>()
        {
            typeof(TankFindTarget), typeof(ArcherFindTarget), typeof(AssassinFindTarget)
        };
    }

    public void AddUnit(int teamNum, Transform unit)
    {
        m_UnitPool[teamNum].Add(unit);
    }
}
