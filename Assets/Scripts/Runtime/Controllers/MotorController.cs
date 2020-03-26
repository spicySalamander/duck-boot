using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotorController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;        //unit rigidbody
    private Vector2 m_currentTargetPoint;

    [SerializeField] private float m_motorSpeed = 0;
    [SerializeField] private float m_reboundTime = 0;

    public UnityEvent onPointReached;   //should begin attack/defend sequence etc

    //Note: sprite size must be taken into account

    private void Start()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        if (m_motorSpeed == 0)
        {
            //m_motorSpeed = defaultSpeed for type of unit;
            m_motorSpeed = 1f;  //temporary
        }

        if (m_reboundTime == 0)
        {
            //m_reboundTime = default for type of unit;
            m_reboundTime = 1f;
        }
    }
    
    /// <summary>
    /// Move towards specified point, unit must have rigidbody.
    /// Note: Method assumes there will be only trigger colliders within the field of movement.
    /// </summary>
    /// <param name="targetPoint">Vector position to move to</param>
    public void MoveToPoint(Vector2 targetPoint)
    {
        //If rigidbody exists, adjust velocity and set new target point
        if (GetComponent<Rigidbody2D>())
        {
            m_rigidbody.velocity = targetPoint.normalized * m_motorSpeed;
            m_currentTargetPoint = targetPoint;

            StartCoroutine(CheckIfPointReached());
        }
        else
        {
            Debug.LogWarning("There is no rigidbody present.");
        }
    }

    private IEnumerator CheckIfPointReached()
    {
        yield return new WaitForEndOfFrame();

        //If target point is reached before, stop movement
        if (Mathf.Abs(m_rigidbody.position.magnitude - m_currentTargetPoint.magnitude) <= m_rigidbody.velocity.magnitude * Time.deltaTime)
        {
            m_rigidbody.velocity = Vector2.zero;
            m_rigidbody.position = m_currentTargetPoint;
            onPointReached.Invoke();
        }
        else
        {
            StartCoroutine(CheckIfPointReached());
        }
    }

    //Move towards point with reference to object, object should have float for range of interaction
    public void MoveToTarget(ICombatTargetFinding targetUnit)
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //StartCoroutine(Rebound());
    }

    /*private IEnumerator Rebound()
    {
        yield return new WaitForSeconds(m_reboundTime);
        MoveToPoint(m_currentTargetPoint);
        Debug.Log("Rebound in " + m_reboundTime + "s.");
    }*/
}
