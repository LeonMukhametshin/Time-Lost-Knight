using UnityEngine;

public class DodgeState : State
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }
    private Movement m_movement;

    protected DodgeStateData data;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;

    protected bool isGrounded;
    protected bool isDodgeOver;

    public DodgeState(FSM fsm, Entity entity, 
        string animBoolName, DodgeStateData data) 
        : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMinAgroRange();
        isGrounded = entity.CheckGround();
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        movement.SetVelocity(data.dodgeSpeed, data.dodgeAngle, -entity.facingDirection);
    }

    public override void Update()
    {
        base.Update();

        if(Time.time >= startTime + data.dodgeTime)
        {
            isDodgeOver = true;
        }
    }
}