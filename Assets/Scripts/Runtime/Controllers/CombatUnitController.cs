using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitController: MonoBehaviour
{
    public enum TeamName {Duck, Enemy};
    public CombatManager.UnitTypes m_Type;
    public TeamName m_MyTeam;
    public Health m_HealthScript;

    private Motor m_MotorScript;
    private ICombatTargetFinding m_TargetFinder;
    private List<CombatUnitController> m_Enemies;
    private List<CombatUnitController> m_Allies;
    private CombatUnitController m_CurrentAttackTarget;
    private SpriteRenderer m_SpriteRenderer;
    private bool m_IsAttacking;
    private bool m_JumpToTarget;
    private Vector3 m_JumpTargetPosition;
    private bool m_AttackCoolingDown;

    //for test only
    public float m_AttackTime;

    private void OnEnable()
    {
#if UNITY_EDITOR
        gameObject.name = $"{m_MyTeam.ToString()}_{m_Type.ToString()}";
#endif
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_MotorScript = GetComponent<Motor>();
        m_HealthScript = GetComponent<Health>();
        CombatManager.Instance.AddUnit((int)m_MyTeam, this);
    }
    private void Start()
    {
        System.Type t = CombatManager.Instance.m_CombatStyles[(int)m_Type];
        m_TargetFinder = gameObject.AddComponent(t) as ICombatTargetFinding;
        m_SpriteRenderer.color = CombatManager.Instance.m_TeamColors[(int)m_MyTeam];
        m_SpriteRenderer.sprite = CombatManager.Instance.m_CombatUnitSprites[(int)m_Type];
        m_Allies = CombatManager.Instance.m_UnitPool[(int)m_MyTeam];
        m_Enemies = CombatManager.Instance.m_UnitPool[((int)m_MyTeam + 1) % 2];
    }

    private void FixedUpdate()
    {
        if (!CombatManager.Instance.m_IsCombatCompleted)
        {
            if (m_JumpToTarget)
            {
                JumpToTarget();
            }
            else if (!m_IsAttacking)
            {
                Movement();
            }
        }
    }

    private void Update()
    {
        // attack Gizmo
        if (m_IsAttacking)
        {
            Vector3 colorChangePoint = Vector3.Lerp(transform.position, m_CurrentAttackTarget.transform.position, .5f);
            Debug.DrawLine(transform.position, colorChangePoint, Color.green);
            Debug.DrawLine(colorChangePoint, m_CurrentAttackTarget.transform.position, Color.yellow);
        }
    }

    private void Movement()
    {
        Vector3 targetPosition = m_TargetFinder.GetTargetPosition(m_Enemies, m_Allies, CombatManager.Instance.m_AttackRanges[(int)m_Type]);
        if (!m_AttackCoolingDown)
        {
            // check Target
            CombatUnitController newTarget = m_TargetFinder.GetAttackTarget(m_Enemies, targetPosition);
            if (newTarget != m_CurrentAttackTarget)
            {
                m_CurrentAttackTarget = newTarget;
                if(m_Type == CombatManager.UnitTypes.Assassin)
                {
                    m_JumpToTarget = true;
                    m_JumpTargetPosition = m_CurrentAttackTarget.transform.position + 
                        (m_CurrentAttackTarget.transform.position - transform.position).normalized * CombatManager.Instance.m_AttackRanges[(int)m_Type];
                }
            }
            // check attack range
            if ((m_CurrentAttackTarget.transform.position - transform.position).magnitude > CombatManager.Instance.m_AttackRanges[(int)m_Type])
            {
                m_MotorScript.MoveToPoint(m_CurrentAttackTarget.transform.position);
            }
            else
            {
                float targetCurrentHealth = m_CurrentAttackTarget.m_HealthScript.TakeDamage(CombatManager.Instance.m_AttackDamage[(int)m_Type]);
                if(targetCurrentHealth <= 0)
                {
                    StartCoroutine(EliminateEnemy(m_CurrentAttackTarget));
                    CombatManager.Instance.UnitDefeated(m_CurrentAttackTarget, (int)m_CurrentAttackTarget.m_MyTeam);
                    return;
                }
                Debug.Log($"{gameObject.name} attack {m_CurrentAttackTarget.name}");
                m_IsAttacking = true;
                m_AttackCoolingDown = true;
                StartCoroutine(AttackCoolDown(2f));
            }
        }
        else if (m_AttackCoolingDown)
        {
            if(CombatManager.UnitTypes.Assassin == m_Type)
            {
                if ((m_CurrentAttackTarget.transform.position - transform.position).magnitude > CombatManager.Instance.m_AttackRanges[(int)m_Type])
                {
                    m_MotorScript.MoveToPoint(m_CurrentAttackTarget.transform.position);
                }
            }
            m_MotorScript.MoveToPoint(targetPosition);
        }
    }

    private IEnumerator AttackCoolDown(float coolDownTime)
    {
        //test
        yield return new WaitForSeconds(m_AttackTime);
        m_IsAttacking = false;

        yield return new WaitForSeconds(coolDownTime);
        m_AttackCoolingDown = false;
    }

    private void JumpToTarget()
    {
        Vector3 distance = m_JumpTargetPosition - transform.position;
        if(distance.magnitude > .1f)
        {
            transform.position += (m_JumpTargetPosition - transform.position).normalized * 10 * Time.deltaTime;
        }
        else
        {
            m_JumpToTarget = false;
        }
    }

    private IEnumerator EliminateEnemy(CombatUnitController enemy)
    {
        yield return null;
        enemy.gameObject.SetActive(false);
    }
}
