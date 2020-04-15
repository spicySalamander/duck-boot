using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : Unit
{
    [SerializeField]
    private float motorSpeed = 0;
    public DuckType type;

    private void Start()
    {
        initState = direction;

        if (motorSpeed == 0)
        {
            //m_motorSpeed = defaultSpeed for type of unit;
            motorSpeed = type.motorSpeed;  //temporary
        }
    }

    private void FixedUpdate()
    {
        DirectionBasedOperation();
    }

    protected override void SetupFacingRight()
    {
        //rb.position += Vector2.right * motorSpeed * Time.fixedDeltaTime;
        rb.velocity = Vector2.right * motorSpeed;

        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("Speed", 1);
        }

    }

    protected override void SetupFacingLeft()
    {
        //rb.position += Vector2.left * motorSpeed * Time.fixedDeltaTime;
        rb.velocity = Vector2.left * motorSpeed;

        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().SetFloat("Speed", 1);
        }

        //transform.localScale = new Vector3(-1, 1, 1);
    }
}
