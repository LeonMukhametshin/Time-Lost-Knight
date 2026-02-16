using UnityEngine;

public class PlayerDetectedState : State
{
    protected PlayerDetectedData data;

    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;
    protected bool performeLongRangeAction;
    protected bool performeCloseRangeAction;
    protected bool isDetectingLedge;
    public PlayerDetectedState(FSM fsm, Entity entity, string animBoolName, PlayerDetectedData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = entity.CheckPlayerInMaxAgroRange();
        isDetectingLedge = entity.CheckLedge();
        performeCloseRangeAction = entity.CheckPlayerInCloseRangeAction();
    }

    public override void Enter()
    {
        base.Enter();

        performeLongRangeAction = false;
        entity.SetVelocityX(0f);
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if(Time.time >= startTime + data.longRangeActionTime)
        {
            performeLongRangeAction = true;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }
}