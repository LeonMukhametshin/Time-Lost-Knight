using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private float m_maxHealth;
    [SerializeField] private GameObject m_deathChunkParticle;
    [SerializeField] private GameObject m_deathBloodParticle;
    [SerializeField] private GameManager m_gameManager;

    private float m_currentHealt;

    private void Start()
    {
        m_currentHealt = m_maxHealth;
    }

    public void DecreaseHealth(float amount)
    {
        if(amount < 0)
        {
            throw new ArgumentException("Damage can`t be negatice");
        }

        m_currentHealt -= amount;

        if(m_currentHealt <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(m_deathChunkParticle, transform.position, m_deathChunkParticle.transform.rotation, null);
        Instantiate(m_deathBloodParticle, transform.position, m_deathBloodParticle.transform.rotation, null);
        m_gameManager.Respawn();
        Destroy(gameObject);
    }
}