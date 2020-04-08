using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UnitController : MonoBehaviour
{
    public UnityEvent onDeath;
    public UnityEvent onCanMove;
    public DuckType type;

    private void Start()
    {
        GetComponent<Attack>().type = type;
        GetComponent<Health>().type = type;
        GetComponent<Move>().type = type;

        GetComponent<Attack>().beginAttack.AddListener(StopMovement);
        GetComponent<Attack>().onKill.AddListener(StartMovement);

        GetComponent<SpriteRenderer>().sprite = type.sprite;
    }

    void StopMovement()
    {
        GetComponent<Move>().Stop();
    }

    void StartMovement()
    {
        GetComponent<Move>().Resume();
    }
}
