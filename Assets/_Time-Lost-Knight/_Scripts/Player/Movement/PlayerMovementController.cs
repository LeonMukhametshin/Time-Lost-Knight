using Inputs;
using TMPro;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private TMP_Text m_text;
    [SerializeField] private TMP_Text m_velocityText;

    public MovementStateMachine m_fsm { get; private set; }

    [SerializeField] private PlayerInputController m_inputs;

    [SerializeField] private BoxCollider2D m_collider;
    [SerializeField] private Rigidbody2D m_rigidbody;

    [SerializeField] private GroundContactChecker m_groundChecker;

    private MovementAbilityCharges m_abilityResourceController;
    private PlayerMovementData m_data;

    private CoroutineRunner m_coroutines;

    private bool m_isInitialized = false;

    public void Initialize(
        PlayerMovementData movemetData,
        CoroutineRunner coroutine)
    {
        if (m_isInitialized)
        {
            return;
        }

        m_data = movemetData;
        m_coroutines = coroutine;
        m_groundChecker.Initialize(m_collider, m_data.groundCheckData);


        m_abilityResourceController = new MovementAbilityCharges(1, 1);

        m_fsm = new MovementStateMachine();

        m_fsm.AddState(new IdleMovementState(m_fsm, m_inputs, m_rigidbody, m_groundChecker, m_abilityResourceController));
        m_fsm.AddState(new RunMovementState(m_fsm, m_inputs, m_rigidbody, m_data.moveData, m_groundChecker, m_abilityResourceController));
        m_fsm.AddState(new JumpMovementState(m_fsm, m_inputs, m_rigidbody, m_data.jumpData, m_coroutines));
        m_fsm.AddState(new FallMovementState(m_fsm, m_inputs, m_rigidbody, m_data.fallData));
        m_fsm.AddState(new DashMovementState(m_fsm, m_rigidbody, transform, m_data.dashData, m_coroutines));

        m_fsm.GetState<JumpMovementState>().jumpFineshed += JumpFinished;
        m_fsm.GetState<DashMovementState>().dashFinished += DashFinished;

        m_fsm.SetState<IdleMovementState>();

        m_isInitialized = true;
    }

    private void DashFinished()
    {
        if(m_groundChecker.isGround)
        {
            if(m_inputs.moveDirection.sqrMagnitude > 0.01f)
            {
                m_fsm.SetState<RunMovementState>();
            }
            else
            {
                m_fsm.SetState<IdleMovementState>();
            }
        }
        else
        {
            m_fsm.SetState<FallMovementState>();
        }
    }

    private void JumpFinished()
    {
        if (m_groundChecker.isGround)
        {
            m_fsm.SetState<IdleMovementState>();
        }
        else
        {
            m_fsm.SetState<FallMovementState>();
        }
    }

    private void Update()
    {
        var input = m_inputs.moveDirection;
        if(input.sqrMagnitude > 0 && m_fsm.currentState is not DashMovementState)
        {
            UpdateFacingDirection(input);
        }
        
        m_velocityText.text = m_rigidbody.linearVelocityY.ToString();
        m_fsm.FixedUpdate();
    }

    private void UpdateFacingDirection(Vector2 direction) =>
     transform.localScale = direction.x < 0
        ? new Vector2(-1, transform.localScale.y)
        : new Vector2(1, transform.localScale.y);
}