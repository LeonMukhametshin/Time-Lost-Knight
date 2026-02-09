using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatDummyController : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject m_hitParticle;

    [SerializeField] private float m_maxHealth;
    [SerializeField] private bool m_applyKnockback;
    [SerializeField] private float m_knockbackSpeedX;
    [SerializeField] private float m_knockbackSpeedY;
    [SerializeField] private float m_knockbackDuration;
    [SerializeField] private float m_knockbackDeathSpeedX;
    [SerializeField] private float m_knockbackDeathSpeedY;
    [SerializeField] private float m_deathTorque;
    
    private float m_knockbackStart;
    private bool m_knockBack;

    private float m_currentHealth;
    private int m_playerFacingDirection;
    private bool m_playerOnLeft;

    [SerializeField] private PlayerController m_playerController;

    [SerializeField] private DummyComponent m_alive;
    [SerializeField] private DummyComponent m_brokenTop;
    [SerializeField] private DummyComponent m_brokenBotom;

    [SerializeField] private Animator m_animator;

    private void Start()
    {
        m_currentHealth = m_maxHealth;

        m_alive.gameObject.SetActive(true);
        m_brokenTop.gameObject.SetActive(false);
        m_brokenBotom.gameObject.SetActive(false);
    }

    private void Update()
    {
        CheckKnockback();
    }

    public void TakeDamage(float[] details)
    {
        if(details[0] < 0)
        {
            throw new ArgumentException("Damage can`t be negative");
        }

        m_currentHealth -= details[0];

        m_playerFacingDirection = details[1] < m_alive.gameObject.transform.position.x
            ? 1 : -1;

        Instantiate(m_hitParticle, m_alive.gameObject.transform.position, Quaternion.Euler(0f, 0f, Random.Range(0, 360f)));

        m_playerOnLeft = m_playerFacingDirection == 1
            ? true
            : false;
        
        m_animator.SetBool("playerOnLeft", m_playerOnLeft);
        m_animator.SetTrigger("Damage");

        if(m_applyKnockback && m_currentHealth > 0f)
        {
            Knockback();
        }

        if(m_currentHealth <= 0f)
        {
            Die();
        }
    }

    private void Knockback()
    {
        m_knockBack = true;
        m_knockbackStart = Time.time;
        m_alive.rigidbody.linearVelocity = new Vector2(m_knockbackSpeedX * m_playerFacingDirection, m_knockbackSpeedY);
    }

    private void CheckKnockback()
    {
        if(m_knockBack && Time.time >= m_knockbackStart + m_knockbackDuration)
        {
            m_knockBack = false;
            m_alive.rigidbody.linearVelocity = new Vector2(0f, m_alive.rigidbody.linearVelocityY);
        }
    }

    private void Die()
    {
        m_alive.gameObject.SetActive(false);
        m_brokenTop.gameObject.SetActive(true);
        m_brokenBotom.gameObject.SetActive(true);

        m_brokenTop.gameObject.transform.position = m_alive.gameObject.transform.position;
        m_brokenBotom.gameObject.transform.position = m_alive.gameObject.transform.position;

        m_brokenBotom.rigidbody.linearVelocity = new Vector2(m_knockbackSpeedX * m_playerFacingDirection, m_knockbackSpeedY);
        m_brokenTop.rigidbody.linearVelocity = new Vector2(m_knockbackDeathSpeedX * m_playerFacingDirection, m_knockbackDeathSpeedY);
        m_brokenTop.rigidbody.AddTorque(m_deathTorque * -m_playerFacingDirection, ForceMode2D.Impulse);

    }
}

[Serializable]
public class DummyComponent
{
    public GameObject gameObject;
    public Rigidbody2D rigidbody;
}