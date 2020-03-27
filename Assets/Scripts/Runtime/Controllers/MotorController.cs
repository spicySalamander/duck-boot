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

    //DEBUG
    Vector3 start;
    Vector3 end;

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
    
    public bool InMotion()
    {
        return m_rigidbody.velocity != Vector2.zero;
    }

    /// <summary>
    /// Move towards specified point, unit must have rigidbody.
    /// Note: Method assumes there will be only trigger colliders within the field of movement.
    /// </summary>
    /// <param name="targetPoint">Vector position to move to.</param>
    public void MoveToPoint(Vector2 targetPoint)
    {
        //If rigidbody exists, adjust velocity and set new target point
        if (GetComponent<Rigidbody2D>() && !InMotion())
        {
            //relative to unit position
            m_currentTargetPoint = targetPoint;

            m_rigidbody.velocity = (m_currentTargetPoint - m_rigidbody.position).normalized * m_motorSpeed;

            Debug.Log("Current velocity set to " + m_rigidbody.velocity);

            StartCoroutine(StopIfPointReached());
        }
        else
        {
            Debug.LogWarning("Object is either still in motion or there is no rigidbody present.");
        }
    }

    private void Update()
    {
        Debug.DrawLine(start, end);
    }

    private IEnumerator StopIfPointReached()
    {
        yield return new WaitForEndOfFrame();

        //If target point is reached before, stop movement
        if (Mathf.Abs((m_currentTargetPoint - m_rigidbody.position).magnitude) <= m_rigidbody.velocity.magnitude * Time.deltaTime)
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
        if (targetUnit.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D targetRigidbody = targetUnit.GetComponent<Rigidbody2D>();
            Vector2 rawVectorFromTarget = m_rigidbody.position - targetRigidbody.position;

            Debug.Log("Distance between vectors are " + rawVectorFromTarget);
            start = Vector3.zero;
            end = rawVectorFromTarget;

            float currDistanceFromTarget = Mathf.Abs(rawVectorFromTarget.magnitude);

            //find new point if not at interaction range
            if (currDistanceFromTarget != interactRange)
            {
                //angle of the current position from target
                var angleFromTarget = Mathf.Atan2(rawVectorFromTarget.y, rawVectorFromTarget.x);
                Vector2 newTarget = targetRigidbody.position + (new Vector2(Mathf.Cos(angleFromTarget), Mathf.Sin(angleFromTarget)) * interactRange);

                MoveToPoint(newTarget);
                Debug.Log("Angle from target is " + angleFromTarget * Mathf.Rad2Deg);
            }
        }
        else
        {
            Debug.LogWarning("Warning: Rigidbody is missing from target object.");
        }
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
