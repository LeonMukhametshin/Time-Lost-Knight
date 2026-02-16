using UnityEngine;

public class StanState : State
{
    protected StunStateData data;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementSropped;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    public StanState(FSM fsm, Entity entity, string animBoolName, StunStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isGrounded = entity.CheckGround();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementSropped = false;
        entity.SetVelocity(data.stunKnockbackSpeed, data.stunKnockbackAngle, entity.lastDamageDirection);
    }

    public override void Exit()
    {
        base.Exit();
        entity.ResetStunResistance();
    }

    public override void Update()
    {
        base.Update();

        if (Time.time >= startTime + data.stunTime)
        {
            isStunTimeOver = true;
        }

        if(isGrounded && Time.time >= startTime + data.stunKnockbactTime && !isMovementSropped)
        {
            isMovementSropped = true;
            entity.SetVelocityX(0f);
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}