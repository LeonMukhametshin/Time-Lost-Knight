using System;
using TMPro;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour, IControllable
{
    [SerializeField] private TextMeshProUGUI m_textState;

    public event Action<MovementStates> StateChanged;

    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private BoxCollider2D m_boxCollider2D;
    [SerializeField] private CoroutineRunner m_coroutineRunner;

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

    public void Initialize(PlayerMovementData movemetData)
    {
        if (m_isInitialized)
        {
            return;
        }

        m_movemnetData = movemetData;

        CreateComponents();
        RegisterAbility();

        StateChanged += UpdateText;

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
        m_abilityFactory = new AbilityFactory(m_coroutineRunner);
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

    private void Update()
    {
        UpdateState();
    }

    private void FixedUpdate()
    {
        UpdateAbilityContex();
    }

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
    }

    private void UpdateText(MovementStates state)
    {
        m_textState.text = state.ToString();
    }

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