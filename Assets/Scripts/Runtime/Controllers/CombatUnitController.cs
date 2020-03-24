using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitController: MonoBehaviour
{
    public CombatManager.UnitTypes m_Type;
    public int m_TeamNumber;
    private ICombatTargetFinding m_TargetFinder;
    private List<Transform> m_Enemies;
    private List<Transform> m_Allies;

    private void OnEnable()
    {
        CombatManager.Instance.AddUnit(m_TeamNumber, transform);
    }
    private void Start()
    {
        System.Type t = CombatManager.Instance.m_CombatStyles[(int)m_Type];
        m_TargetFinder = gameObject.AddComponent(t) as ICombatTargetFinding;
        m_Allies = CombatManager.Instance.m_UnitPool[m_TeamNumber];
        m_Enemies = CombatManager.Instance.m_UnitPool[(m_TeamNumber + 1) % 2];
    }

    private void Update()
    {
        transform.position = m_TargetFinder.GetTarget(m_Enemies, m_Allies);
    }

}
