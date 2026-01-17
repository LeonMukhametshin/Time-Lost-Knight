using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IControllable
{
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private BoxCollider2D m_boxCollider2D;

    private PlayerMovementData m_movemnetData;

    private AbilitiesContainer m_abilitiesContainer;
    private AbilityContext m_abilityContext;
    private AbilityFactory m_abilityFactory;

    private GroundChecker m_groundChecker;

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
        m_abilityContext = new AbilityContext();
        m_abilitiesContainer = new AbilitiesContainer();
        m_abilityFactory = new AbilityFactory();

        m_groundChecker = new GroundChecker(m_boxCollider2D, m_movemnetData.m_groundCheckData);
    }

    private void RegisterAbility()
    {
        m_abilitiesContainer.RegisterAbility(m_abilityFactory.Create(AbilityKey.WALK, m_rigidbody2D, m_movemnetData), AbilityKey.WALK);
        m_abilitiesContainer.RegisterAbility(m_abilityFactory.Create(AbilityKey.DASH, m_rigidbody2D, m_movemnetData), AbilityKey.DASH);
        m_abilitiesContainer.RegisterAbility(m_abilityFactory.Create(AbilityKey.JUMP, m_rigidbody2D, m_movemnetData), AbilityKey.JUMP);
    }

    public void Move(float direction)
    {
        m_abilityContext.xDirection = direction;
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
}