using UnityEngine;

public class IdleState : EnemyState
{
    protected IdleStateData data;

    protected bool flipAfterIdle;
    protected bool isIdleTimeOver;
    protected bool isPlayerInMinAgroRange;
    protected bool isPlayerInMaxAgroRange;

    protected float idleTime;

    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    protected FlipContoller flipController =>
        m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
   
    private EnemyCollisionDetector enemyCollisionDetector => 
        m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    
    private Movement m_movement;
    private FlipContoller m_flipContoller;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public IdleState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, IdleStateData data) 
        : base(fsm, core, animBoolName, entity)
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

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMaxAgroRange();
    }

    public void SetFlipAfterIdle(bool flip) =>
        flipAfterIdle = flip;

    private void SetRandomIdleTime() =>
        idleTime = Random.Range(data.minIdleTime, data.maxIdleTime);
}