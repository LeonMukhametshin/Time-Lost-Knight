using System;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private Transform m_rayCast;
    [SerializeField] private LayerMask m_raycastMask;
    [SerializeField] private float m_rayCastLength;

    [SerializeField] private float m_attackDistance;
    [SerializeField] private float m_movementSpeed;
    [SerializeField] private float m_timer;

    private RaycastHit2D m_hit;
    private GameObject m_target;
    private Animator m_animator;
    private float m_distance;
    private bool m_attackMode;
    private bool m_inRange;
    private bool m_cooling;
    private float m_intTimer;

    private void Awake()
    {
        m_intTimer = m_timer;
        m_animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(m_inRange)
        {
            m_hit = Physics2D.Raycast(m_rayCast.position, 
                Vector2.left, m_rayCastLength, m_raycastMask);
        }

        if(m_hit.collider is not null)
        {
            EnemyLogic();
        }
        else
        {
            m_inRange = false;
        }
        
        if(!m_inRange)
        {
            m_animator.SetBool("Can Walk", false);
            StopAttack();
        }
    }

    private void EnemyLogic()
    {
        m_distance = Vector2.Distance(transform.position, m_target.transform.position);

        if(m_distance > m_attackDistance)
        {
            Move();
            StopAttack();
        }
        else if(m_distance <= m_attackDistance && !m_cooling)
        {
            Attack();
        }

        if(m_cooling)
        {
            Cooldown();
            m_animator.SetBool("Attack", false);
        }
    }

    private void Attack()
    {
        m_timer = m_intTimer;
        m_attackMode = true;

        m_animator.SetBool("Can Walk", false);
        m_animator.SetBool("Attack", true);
    }

    private void StopAttack()
    {
        m_cooling = false;
        m_attackMode = false;

        m_animator.SetBool("Attack", false);
    }

    private void Move()
    {
        m_animator.SetBool("Can Walk", true);
        if (!m_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        {
            Vector2 targetPosition = new Vector2(m_target.transform.position.x, 
                transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, 
                m_movementSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if(trigger.gameObject.tag == "Player")
        {
            m_target = trigger.gameObject;
            m_inRange = true;
        }
    }

    private void Cooldown()
    {
        m_timer -= Time.deltaTime;

        if(m_timer <= 0 && m_cooling && m_attackMode)
        {
            m_timer = m_intTimer;
        }
    }

    public void TriggerCooling()
    {
        m_cooling = true;
    }
}   