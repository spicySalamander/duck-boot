using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CombatManager : Singleton<CombatManager>
{
    public enum UnitTypes { Tank, Archer, Assassin }
    public float[] m_AttackRanges;
    public int[] m_AttackDamage;
    public float[] m_AttackCoolDown;
    public List<System.Type> m_CombatStyles;
    public List<Sprite> m_CombatUnitSprites;
    public List<CombatUnitController>[] m_UnitPool;
    public List<CombatUnitController> m_Team1;
    public List<CombatUnitController> m_Team2;
    public Color[] m_TeamColors;
    public bool m_IsCombatCompleted;

    public UnityEvent m_OnCombatWin;
    public UnityEvent m_OnCombatLose;

    public void Awake()
    {
        m_IsCombatCompleted = false;
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

    public void UnitDefeated(CombatUnitController unit, int teamNum)
    {
        for (int i = 0; i < m_UnitPool[teamNum].Count; i++)
        {
            if(m_UnitPool[teamNum][i] == unit)
            {
                m_UnitPool[teamNum].RemoveAt(i);
            }
        }
        if(m_UnitPool[teamNum].Count < 1)
        {
            m_IsCombatCompleted = true;
            CombatComplete(teamNum);
        }
    }

    private void CombatComplete(int teamNum)
    {
        switch (teamNum)
        {
            case 0:
                m_OnCombatLose.Invoke();
                break;
            case 1:
                m_OnCombatWin.Invoke();
                break;
        }
    }
}
