using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorController : MonoBehaviour
{
    private Rigidbody2D m_rigidbody;


    //Note: sprite size must be taken into account

    private void Start()
    {
        if (m_rigidbody == null)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
    }



    //motor given behaviour/pattern and then executes based on range/offset
}
