using System;
using UnityEngine;
using Inputs;

public class PlayerMovementController : MonoBehaviour, IControllable
{
    public event Action<MovementStates> StateChanged;

    [SerializeField] private BoxCollider2D m_boxCollider2D;

    private Rigidbody2D m_rigidbody2D;

    private CoroutineRunner m_coroutines;

    private PlayerMovementData m_movemnetData;

    private AbilitiesContainer m_abilitiesContainer;
    private AbilityFactory m_abilityFactory;
    private GroundChecker m_groundChecker;

    private AbilityContext m_abilityContext;
    private MovementStates m_state;

    public MovementStates state
    {
        get => m_state;
        set
        {
            if(m_state != value)
            {
                m_state = value;
                StateChanged?.Invoke(m_state);
            }
        }
    }

    private bool m_isInitialized = false;

    public void Initialize(PlayerMovementData movemetData, CoroutineRunner coroutine, Rigidbody2D rigidbody)
    {
        if (m_isInitialized)
        {
            return;
        }

        m_movemnetData = movemetData;
        m_coroutines = coroutine;
        m_rigidbody2D = rigidbody;

        CreateComponents();
        RegisterAbility();

        m_isInitialized = true;
    }

    private void CreateComponents()
    {
        m_groundChecker = new GroundChecker(
            m_boxCollider2D, 
            m_movemnetData.m_groundCheckData);

        m_abilityContext = new AbilityContext(
            Vector2.zero,
            Vector2.zero,
            false,
            false,
            (int)transform.localScale.x);

        m_abilitiesContainer = new AbilitiesContainer();
        m_abilityFactory = new AbilityFactory(m_coroutines);
    }

    private void RegisterAbility()
    {
        m_abilitiesContainer.RegisterAbility(
            m_abilityFactory.Create(AbilityKey.Walk, m_rigidbody2D, m_movemnetData),
            AbilityKey.Walk);

        m_abilitiesContainer.RegisterAbility(
            m_abilityFactory.Create(AbilityKey.Dash, m_rigidbody2D, m_movemnetData), 
            AbilityKey.Dash);

        m_abilitiesContainer.RegisterAbility(
            m_abilityFactory.Create(AbilityKey.Jump, m_rigidbody2D, m_movemnetData),
            AbilityKey.Jump);
    }

    private void Update() =>
        UpdateState();

    private void FixedUpdate() =>
         UpdateAbilityContex();

    public void Move(Vector2 direction)
    {
        if (direction.sqrMagnitude > 0.01f)
        {
            Flip(direction.x);
        }

        m_abilityContext.moveDirection = direction;

        m_abilitiesContainer
            .GetAbility(AbilityKey.Walk)
            .Do(m_abilityContext);
    }

    public void Jump()
    {
        if(!m_groundChecker.IsGrounded())
        {
            return;
        }

        m_abilitiesContainer
            .GetAbility(AbilityKey.Jump)
            .Do(m_abilityContext);
    }

    public void Dash()
    {
        if (!m_abilityContext.canDash)
        {
            return;
        }

        m_abilityContext.canDash = false;

        state = MovementStates.Dash;
        m_abilitiesContainer
            .GetAbility(AbilityKey.Dash)
            .Do(m_abilityContext);
    }

    private void Flip(float xDirection) =>
        transform.localScale = xDirection < 0
            ? new Vector2(-1, transform.localScale.y)
            : new Vector2(1, transform.localScale.y);

    private void UpdateState()
    {
        if(m_abilitiesContainer.GetAbility(AbilityKey.Dash).isActive)
        {
            state = MovementStates.Dash;
            return;
        }

        if(!m_abilityContext.isGrounded)
        {
            state = m_abilityContext.velocity.y > 0.1f
                ? MovementStates.Jump
                : MovementStates.Fall;

            return;
        }

        state = Mathf.Abs(m_abilityContext.moveDirection.x) > 0.01f
            ? MovementStates.Walk
            : MovementStates.Idle;
    }

    private void UpdateAbilityContex()
    {
        m_abilityContext.velocity = m_rigidbody2D.linearVelocity;
        m_abilityContext.isGrounded = m_groundChecker.IsGrounded();
        m_abilityContext.xScale = (int)transform.localScale.x;

        if (m_abilityContext.isGrounded)
        {
            m_abilityContext.canDash = true;
        }
    }
}