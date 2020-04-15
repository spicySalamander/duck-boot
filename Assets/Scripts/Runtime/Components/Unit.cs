using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour
{
    public enum FacingDirection { Stop, Left, Right }

    [SerializeField]
    protected FacingDirection direction;
    protected FacingDirection initState;
    protected Rigidbody2D rb;

    protected UnityEvent enemyInRange;
    protected UnityEvent enemyDead;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    protected void DirectionBasedOperation()
    {
        switch (direction)
        {
            case FacingDirection.Right:
                SetupFacingRight();
                break;
            case FacingDirection.Left:
                SetupFacingLeft();
                break;
            default:
                if (GetComponent<Animator>())
                {
                    GetComponent<Animator>().SetFloat("Speed", 0);
                }
                rb.velocity = Vector2.zero;
                break;
        }
    }

    protected void ChangeState(FacingDirection state)
    {
        direction = state;
    }

    public void Stop()
    {
        ChangeState(FacingDirection.Stop);
    }

    public void Resume()
    {
        ChangeState(initState);
    }

    protected abstract void SetupFacingRight();
    protected abstract void SetupFacingLeft();
}
