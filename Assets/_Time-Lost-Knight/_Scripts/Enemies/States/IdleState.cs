using UnityEngine;

public class IdleState : State
{
    protected IdleStateData data;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;

    protected float idleTime;

    public IdleState(FSM fsm, Entity entity, string animBoolName, IdleStateData data) : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        entity.SetVelocity(0f);

        isIdleTimeOver = false;
        SetRandomIdleTime();    
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            entity.Flip();
        }
    }

    public override void Update()
    {
        base.Update();

        if (Time.time > startTime + idleTime)
        {
            isIdleTimeOver = true;
        }
    }

    public override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void SetFlipAfredIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime() =>
        idleTime = UnityEngine.Random.Range(data.minIdleTime, data.maxIdleTime);
}