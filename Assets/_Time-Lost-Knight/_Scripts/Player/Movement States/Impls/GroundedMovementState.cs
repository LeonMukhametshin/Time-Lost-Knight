using Inputs;

public abstract class GroundedMovementState : MovementState
{
    protected readonly PlayerInputController m_input;
    private readonly GroundContactChecker m_groundChecker;
    private readonly MovementAbilityCharges m_abilityCharges;

    public GroundedMovementState(MovementStateMachine fsm, PlayerInputController inputs, GroundContactChecker groundChecker, MovementAbilityCharges ability) : base(fsm)
    {
        m_input = inputs;
        m_groundChecker = groundChecker;
        m_abilityCharges = ability;

        RegisterInputListeners();
    }

    public virtual void RegisterInputListeners()
    {
        m_input.jump += HandleJumpInput;
        m_input.dash += HandleDashInput;

        m_groundChecker.groundedStateChanged += HandleGroundedStateChanged;
    }

    private void HandleJumpInput()
    {
        if (!m_abilityCharges.CanJump(m_groundChecker.isGround))
        {
            return;
        }

        m_abilityCharges.ConsumeJump();
        fsm.SetState<JumpMovementState>();
    }

    private void HandleDashInput()
    {
        if(!m_abilityCharges.CanDash(m_groundChecker.isGround))
        {
            return;
        }

        m_abilityCharges.ConsumeDash();
        fsm.SetState<DashMovementState>();
    }

    private void HandleGroundedStateChanged()
    {
        if (fsm.currentState is DashMovementState or JumpMovementState)
        {
            return;
        }

        if (m_groundChecker.isGround)
        {
            m_abilityCharges.ResetJump();
            m_abilityCharges.ResetDash();

            if (m_input.moveDirection.sqrMagnitude > 0.01f)
            {
                fsm.SetState<RunMovementState>();
            }
            else
            {
                fsm.SetState<IdleMovementState>();
            }
        }
        else
        {
            fsm.SetState<FallMovementState>();
        }
    }
}