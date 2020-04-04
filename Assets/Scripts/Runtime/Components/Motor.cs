using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Motor : MonoBehaviour
{
    private enum FacingDirection { Right, Left }
    [SerializeField] private float m_motorSpeed = 0;

    private Rigidbody2D m_rigidbody;
    private Vector2 m_currentTargetPoint;
    private Rigidbody2D m_currentTarget;
    private FacingDirection facingDirection;

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
        if (GetComponent<Rigidbody2D>()) //&& !InMotion())
        {
            //relative to unit position
            m_currentTargetPoint = targetPoint;
            m_rigidbody.velocity = (m_currentTargetPoint - m_rigidbody.position).normalized * m_motorSpeed;

            if (m_rigidbody.velocity.x > 0)
            {
                //face right
                facingDirection = FacingDirection.Right;
            }
            else if (m_rigidbody.velocity.x < 0)
            {
                //face left
                facingDirection = FacingDirection.Left;
            }

            StartCoroutine(StopIfPointReached());
        }
        else
        {
            Debug.LogWarning("There is no rigidbody present.");
        }
    }

    private void Update()
    {
        //Debug.DrawLine(start, end);

        if (facingDirection == FacingDirection.Right)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = -Vector3.one;
        }
    }

    private IEnumerator StopIfPointReached()
    {
        yield return new WaitForEndOfFrame();

        //If target point is reached before, stop movement
        if (Mathf.Abs((m_currentTargetPoint - m_rigidbody.position).magnitude) <= m_rigidbody.velocity.magnitude * Time.deltaTime)
        {
            m_rigidbody.velocity = Vector2.zero;
            m_rigidbody.position = m_currentTargetPoint;

            if (m_currentTarget != null)
            {
                //face enemy at velocity = 0
                if (m_rigidbody.position.x < m_currentTarget.position.x)
                {
                    facingDirection = FacingDirection.Right;
                }
                else if (m_rigidbody.position.x > m_currentTarget.position.x)
                {
                    facingDirection = FacingDirection.Left;
                }
            }

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
            m_currentTarget = targetUnit.GetComponent<Rigidbody2D>();

            //Debug.Log("Distance between vectors are " + rawVectorFromTarget);
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
                //Debug.Log("Angle from target is " + angleFromTarget * Mathf.Rad2Deg);
            }
        }
        else
        {
            Debug.LogWarning("Warning: Rigidbody is missing from target object.");
        }
    }

    //Move out of range of the target unit using target's interact range
    //- consider wall collisions
    public void MoveAwayFromTarget(GameObject targetUnit)
    {

    }

    //private Vector2 

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
