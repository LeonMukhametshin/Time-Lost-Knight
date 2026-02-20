using UnityEngine;

public class DodgeState : EnemyState
{
    public DodgeStateData data { get; private set; }

    protected bool performCloseRangeAction;
    protected bool isPlayerInMaxAgroRange;

    protected bool isGrounded;
    protected bool isDodgeOver;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    private FlipContoller flipContoller
    {
        get => m_flipController ??= core.GetCoreComponent<FlipContoller>();
    }

    private EnemyCollisionDetector enemyCollisionDetector
    {
        get => m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    }

    private Movement m_movement;
    private FlipContoller m_flipController;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public DodgeState(EnemyFSM fsm, Entity entity, 
        string animBoolName, DodgeStateData data) 
        : base(fsm, entity, animBoolName)
    {
        this.data = data
    }

    public override void DoCheck()
    {
        base.DoCheck();

        performCloseRangeAction = enemyCollisionDetector.CheckPlayerInCloseRangeAction();
        isPlayerInMaxAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
        isGrounded = enemyCollisionDetector.CheckWallTouch();
    }

    public override void Enter()
    {
        base.Enter();

        isDodgeOver = false;
        movement.SetVelocity(data.dodgeSpeed, data.dodgeAngle, -flipContoller.facingDirection);
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