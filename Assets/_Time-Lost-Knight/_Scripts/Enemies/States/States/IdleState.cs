using UnityEngine;

public class IdleState : State
{
    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    protected FlipContoller flipController
    {
        get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
    }

    private Movement m_movement;
    private FlipContoller m_flipContoller;

    protected IdleStateData data;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;

    protected float idleTime;

    public IdleState(FSM fsm, Entity entity, 
        string animBoolName, IdleStateData data) 
        : base(fsm, entity, animBoolName)
    {
        this.data = data;
    }

    public override void Enter()
    {
        base.Enter();

        movement.SetVelocityX(0f);
        isIdleTimeOver = false;
        SetRandomIdleTime();    
    }

    public override void Exit()
    {
        base.Exit();

        if (flipAfterIdle)
        {
            flipController.Flip();
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

    public void SetFlipAfterIdle(bool flip)
    {
        flipAfterIdle = flip;
    }

    private void SetRandomIdleTime() =>
        idleTime = UnityEngine.Random.Range(data.minIdleTime, data.maxIdleTime);

    public override void DoChecks()
    {
        base.DoChecks();

        isPlayerInMinAgroRange = entity.CheckPlayerInMinAgroRange();
    }
}