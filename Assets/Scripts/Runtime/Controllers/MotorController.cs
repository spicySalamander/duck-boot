using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MotorController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Vector2 m_currentTargetPoint;

    [SerializeField] private float m_motorSpeed = 0;

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
    }
    
    /// <summary>
    /// Move towards specified point, unit must have rigidbody.
    /// Note: Method assumes there will be only trigger colliders within the field of movement.
    /// </summary>
    /// <param name="targetPoint">Vector position to move to.</param>
    public void MoveToPoint(Vector2 targetPoint)
    {
        //If rigidbody exists, adjust velocity and set new target point
        if (GetComponent<Rigidbody2D>())
        {
            m_rigidbody.velocity = targetPoint.normalized * m_motorSpeed;
            m_currentTargetPoint = targetPoint;

            StartCoroutine(StopIfPointReached());
        }
        else
        {
            Debug.LogWarning("There is no rigidbody present.");
        }
    }

    private IEnumerator StopIfPointReached()
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
            StartCoroutine(StopIfPointReached());
        }
    }

    //Move towards point given a target and interaction range
    public void MoveToTarget(GameObject targetUnit, float interactRange)
    {
        
    }

    //Move out of range of the target unit - consider wall collisions
    public void MoveAwayFromTarget(GameObject targetUnit)
    {

    }

    //Move away from all targets (tentative)
    public void MoveAwayAll()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
