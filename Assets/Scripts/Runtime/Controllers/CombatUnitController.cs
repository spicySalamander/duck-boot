using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatUnitController: MonoBehaviour
{
    public enum TeamName {Duck, Enemy};
    public CombatManager.UnitTypes m_Type;
    public TeamName m_MyTeam;

    private Motor m_MotorScript;
    private ICombatTargetFinding m_TargetFinder;
    private List<CombatUnitController> m_Enemies;
    private List<CombatUnitController> m_Allies;
    private Transform m_CurrentAttackTarget;
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

    private void Update()
    {
        if (m_IsAttacking)
        {
            Debug.DrawLine(transform.position, m_CurrentAttackTarget.position, Color.red);
        }
        else if (m_JumpToTarget)
        {
            JumpToTarget();
        }
        else
        {
            Movement();
        }
    }

    private void Movement()
    {
        Vector3 targetPosition = m_TargetFinder.GetTargetPosition(m_Enemies, m_Allies, CombatManager.Instance.m_AttackRanges[(int)m_Type]);
        if (!m_AttackCoolingDown)
        {
            // check Target
            Transform newTarget = m_TargetFinder.GetAttackTarget(m_Enemies, targetPosition);
            if (newTarget != m_CurrentAttackTarget)
            {
                m_CurrentAttackTarget = newTarget;
                if(m_Type == CombatManager.UnitTypes.Assassin)
                {
                    m_JumpToTarget = true;
                    m_JumpTargetPosition = m_CurrentAttackTarget.position + 
                        (m_CurrentAttackTarget.position - transform.position).normalized * CombatManager.Instance.m_AttackRanges[(int)m_Type];
                }
            }
            // check attack range
            if ((m_CurrentAttackTarget.position - transform.position).magnitude > CombatManager.Instance.m_AttackRanges[(int)m_Type])
            {
                //m_MotorScript.MoveToPoint(m_CurrentAttackTarget.position);
                MoveToPoint(m_CurrentAttackTarget.position);
            }
            else
            {
                // TODO: Attack
                Debug.Log($"{gameObject.name} attack {m_CurrentAttackTarget.name}");
                m_IsAttacking = true;
                m_AttackCoolingDown = true;
                StartCoroutine(AttackCoolDown(2f));
            }
        }
        else if (m_AttackCoolingDown)
        {
            //m_MotorScript.MoveToPoint(targetPosition);
            MoveToPoint(targetPosition);
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

    /// <summary>
    /// test
    /// </summary>
    private void MoveToPoint(Vector3 targetPosition)
    {
        transform.position += (targetPosition - transform.position).normalized * Time.deltaTime;
    }
}
