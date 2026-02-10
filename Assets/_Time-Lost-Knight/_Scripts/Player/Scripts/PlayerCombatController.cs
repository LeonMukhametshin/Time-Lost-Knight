using UnityEngine;

public class PlayerCombatController : MonoBehaviour, IDamageable
{
    private GameInput m_gameInput;

    [SerializeField] private PlayerController m_controller;
    [SerializeField] private PlayerStats m_playerStart;

    [SerializeField] private bool m_combatEnabled;
    [SerializeField] private float m_inputTimer;
    [SerializeField] private float m_attack1Radius;
    [SerializeField] private float m_attack1Damage;
    [SerializeField] private float m_stunDamageAmount;

    [SerializeField] private Transform m_attack1HitBoxPosition;
    [SerializeField] private LayerMask m_whatIsDamageable;

    private bool m_gotInput = true;
    private bool m_isAttacking = false;
    private bool m_isFirstAttack = false;

    private float m_lastInputTime = Mathf.NegativeInfinity;

    [SerializeField] private Animator m_animator;

    private AttackDetails attackDetails;

    private void OnEnable()
    {
        m_gameInput = new GameInput();
        m_gameInput.Player.Enable();
    }

    private void Start()
    {
        m_animator.SetBool("canAttack", m_combatEnabled);
    }

    private void Update()
    {
        CheckCombatInput();
        CheckAttacks();
    }

    private void CheckCombatInput()
    {
        if (m_gameInput.Player.MainWeaponAttack1.WasCompletedThisFrame())
        {
            if (m_combatEnabled)
            {
                m_gotInput = true;
                m_lastInputTime = Time.time;
            }
        }
    }

    private void CheckAttacks()
    {
        if (Time.time >= m_lastInputTime + m_inputTimer)
        {
            m_gotInput = false;
        }

        if (m_gotInput)
        {
            if(!m_isAttacking)
            {
                m_gotInput = false;
                m_isAttacking = true;
                m_isFirstAttack = !m_isFirstAttack;

                m_animator.SetBool("attack1", true);
                m_animator.SetBool("firstAttack", m_isFirstAttack);
                m_animator.SetBool("isAttacking", m_isAttacking);
            }
        }
    }

    private void CheckAttackHitBox()
    {
        var detectedObject = Physics2D.OverlapCircleAll(m_attack1HitBoxPosition.position, m_attack1Radius, m_whatIsDamageable);

        attackDetails.damageAmout = m_attack1Damage;
        attackDetails.position = transform.position;
        attackDetails.stunDamageAmount = m_stunDamageAmount;

        foreach (var obj in detectedObject)
        {
            if (obj.TryGetComponent<IDamageable>(out var damageable1))
            {
                damageable1.TakeDamage(attackDetails);
                continue;
            }
            if (obj.gameObject.transform.parent.TryGetComponent<IDamageable>(out var damageable2))
            {
                damageable2.TakeDamage(attackDetails);
            }
        }
    }   

    private void FinishAttack1()
    {
        m_isAttacking = false;
        m_animator.SetBool("isAttacking", m_isAttacking);
        m_animator.SetBool("attack1", false);
    }

    public void TakeDamage(AttackDetails attackDetails)
    {
        if(m_controller.GetDashStatus())
        {
            return;
        }

        int direction = attackDetails.position.x < transform.position.x
            ? 1 : -1;

        m_playerStart.DecreaseHealth(attackDetails.damageAmout);

        m_controller.Knockback(direction);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_attack1HitBoxPosition.position, m_attack1Radius);
    }
}