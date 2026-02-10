using UnityEngine;

public class ChargeState : State
{
    protected ChargeStateData data;
    protected bool isPlayerInMinAgroRange;
    protected bool isDetectingLedge;
    protected bool isDetectingWall;
    protected bool isChargeTimeOver;
    protected bool performCloseRangeAction;

    public ChargeState(FSM fsm, Entity entity, string animBoolName, ChargeStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        isChargeTimeOver = false;
        entity.SetVelocity(data.chargeSpeed);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Time.time >= startTime + data.chargeTime)
        {
            isChargeTimeOver = true;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isDetectingLedge = entity.CheckLedge();
        isDetectingWall = entity.CheckWall();
        performCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }
}
