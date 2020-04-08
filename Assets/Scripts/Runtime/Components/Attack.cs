using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Attack : Unit
{
    public DuckType type;
    public int attackDmg;
    [SerializeField]
    private float cooldown;
    [SerializeField]
    private float attackRange;
    private string enemyLayer =  "Bread";
    private Health target;

    bool tripped = false;

    public UnityEvent beginAttack;
    public UnityEvent onAttack;
    public UnityEvent onKill;

    private void Start()
    {
        beginAttack.AddListener(OnAttack);

        if (attackDmg == 0)
        {
            attackDmg = type.attackDmg;
        }

        if (cooldown == 0)
        {
            cooldown = type.cooldown;
        }

        if (attackRange == 0)
        {
            attackRange = type.attackRange;
        }

        DirectionBasedOperation();
    }

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Linecast(rb.position, new Vector2(rb.position.x + attackRange, rb.position.y), LayerMask.GetMask(enemyLayer));
        if (hit)
        {
            target = hit.rigidbody.gameObject.GetComponent<Health>();

            if (!tripped)
            {
                beginAttack.Invoke();
                tripped = true;
            }
        }
        else
        {
            tripped = false;
            onKill.Invoke();
        }
    }

    protected override void SetupFacingRight()
    {
        //positive range
        enemyLayer = "Bread";
    }

    protected override void SetupFacingLeft()
    {
        //negative range
        attackRange = -attackRange;
        enemyLayer = "Duck";
    }

    private void OnAttack()
    {
        StartCoroutine(AttackBegin());
    }

    IEnumerator AttackBegin()
    {
        onAttack.Invoke();
        target.TakeDamage(attackDmg);
        target.GetComponentInChildren<UIMeter>().Sub(target.type.totalHealth, type.attackDmg);
        yield return new WaitForSeconds(cooldown);
        if (tripped)
        {
            StartCoroutine(AttackBegin());
        }
    }
}
