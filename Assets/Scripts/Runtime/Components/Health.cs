using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public DuckType type;
    public int m_totalHealth;
    private int m_currentHealth;

    public UnityEvent onDeath;

    private void Start()
    {
        if (m_totalHealth <= 0)
        {
            m_totalHealth = type.totalHealth; //default value
        }

        m_currentHealth = m_totalHealth;
    }

    public int TakeDamage(int dmg)
    {
        if (dmg < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot hurt target for negative value");
        }

        m_currentHealth -= dmg;

        if (m_currentHealth <= 0)
        {
            m_currentHealth = 0;
            Die();
        }

        return m_currentHealth;
    }

    private void Die()
    {
        onDeath.Invoke();
        Destroy(gameObject);

    }

    /*public int Heal(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot heal target for negative value");
        }
        m_currentHealth += amount;

        if (m_currentHealth >= m_totalHealth)
        {
            m_currentHealth = m_totalHealth;
        }

        return m_currentHealth;
    }*/
}
