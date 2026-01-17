using System.Transactions;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IControllable
{
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private BoxCollider2D m_boxCollider2D;

    private PlayerMovementData m_movemnetData;

    private AbilitiesContainer m_abilitiesContainer;
    private AbilityFactory m_abilityFactory;
    private GroundChecker m_groundChecker;

    private AbilityContext m_abilityContext;

    private bool m_isInitialized = false;

    public void Initialize(PlayerMovementData movemetData)
    {
        if (m_isInitialized)
        {
            return;
        }

        m_movemnetData = movemetData;

        CreateComponents();
        RegisterAbility();

        m_isInitialized = true;
    }

    private void CreateComponents()
    {
        m_groundChecker = new GroundChecker(m_boxCollider2D, m_movemnetData.m_groundCheckData);

        m_abilityContext = new AbilityContext(Vector2.zero, (int)transform.localScale.x, m_groundChecker.IsGrounded());
        m_abilitiesContainer = new AbilitiesContainer();
        m_abilityFactory = new AbilityFactory();
    }

    private void RegisterAbility()
    {
        m_abilitiesContainer.RegisterAbility(m_abilityFactory.Create(AbilityKey.WALK, m_rigidbody2D, m_movemnetData), AbilityKey.WALK);
        m_abilitiesContainer.RegisterAbility(m_abilityFactory.Create(AbilityKey.DASH, m_rigidbody2D, m_movemnetData), AbilityKey.DASH);
        m_abilitiesContainer.RegisterAbility(m_abilityFactory.Create(AbilityKey.JUMP, m_rigidbody2D, m_movemnetData), AbilityKey.JUMP);
    }

    public void Move(Vector2 direction)
    {
        if(direction.sqrMagnitude > 0.01f)
        {
            Flip(direction.x);
        }

        m_abilityContext.moveDirection = direction;
        m_abilitiesContainer.GetAbility(AbilityKey.WALK).Do(m_abilityContext);
    }

    public void Jump()
    {
        m_abilityContext.grounded = m_groundChecker.IsGrounded();
        m_abilitiesContainer.GetAbility(AbilityKey.JUMP).Do(m_abilityContext);
    }

    public void Dash()
    {
        m_abilitiesContainer.GetAbility(AbilityKey.DASH).Do(m_abilityContext);
    }

    private void Flip(float xDirection)
    {
        if (xDirection < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        m_abilityContext.xScale = (int)transform.localScale.x;
    }
}