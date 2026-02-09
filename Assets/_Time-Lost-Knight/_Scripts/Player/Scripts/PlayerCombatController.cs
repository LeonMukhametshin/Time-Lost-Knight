using UnityEngine;

public class PlayerCombatController : MonoBehaviour
{
    private GameInput m_gameInput;

    [SerializeField] private bool m_combatEnabled;
    [SerializeField] private float m_inputTimer;
    [SerializeField] private Transform m_attack1HitBoxPosition;
    [SerializeField] private LayerMask m_whatIsDamageable;
    [SerializeField] private float m_attack1Radius;
    [SerializeField] private float m_attack1Damage;

    private bool m_gotInput = true;
    private bool m_isAttacking = false;
    private bool m_isFirstAttack = false;

    private float m_lastInputTime = Mathf.NegativeInfinity;

    [SerializeField] private Animator m_animator;

    private float[] m_attackDetails = new float[2];

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

        m_attackDetails[0] = m_attack1Damage;
        m_attackDetails[1] = transform.position.x;

        foreach (var obj in detectedObject)
        {
            if(obj.gameObject.TryGetComponent<BasicEnemyController>(out var enemy))
            {
                enemy.Damage(m_attackDetails);
            }
       
            // Instantiate hit particle 
        }
    }   

    private void FinishAttack1()
    {
        m_isAttacking = false;
        m_animator.SetBool("isAttacking", m_isAttacking);
        m_animator.SetBool("attack1", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_attack1HitBoxPosition.position, m_attack1Radius);
    }
}