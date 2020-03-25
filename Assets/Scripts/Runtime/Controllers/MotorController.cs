using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;
    private Vector2 m_currentTarget;

    [SerializeField] private float m_motorSpeed = 0;

    //Note: sprite size must be taken into account

    private void Start()
    {
        if (m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }

        if (m_motorSpeed == 0)
        {
            //m_motorSpeed = defaultSpeed for type of unit;
            m_motorSpeed = 1f;  //temporary
        }
    }

    //Move towards point (raw)
    public void MoveToPointWithRigidbody(Vector2 target)
    {
        if (GetComponent<Rigidbody2D>())
        {
            m_rigidbody.velocity = target.normalized * m_motorSpeed;
            m_currentTarget = target;

            //MOVE THIS MUST BE CALLED EVERY UPDATE
            //the current rigidboy position - the target position yields a result less than the velocity, then stop at target?
            if (m_rigidbody.position.magnitude - target.magnitude <= m_rigidbody.velocity.magnitude)
            {
                m_rigidbody.position = target;
            }

        }
        else
        {
            Debug.LogWarning("There is no rigidbody present.");
        }
    }

    //Move towards point with reference to object, object should have float for range of interaction
    public void MoveToTargetWithRigidbody(ICombatTargetFinding target)
    {

    }

    //Move away from point -- redundant
}
