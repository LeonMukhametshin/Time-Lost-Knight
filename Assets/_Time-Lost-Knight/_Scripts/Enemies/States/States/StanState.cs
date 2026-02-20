using UnityEngine;

public class StanState : EnemyState
{
    protected StunStateData data;

    protected bool isStunTimeOver;
    protected bool isGrounded;
    protected bool isMovementSropped;

    protected bool performCloseRangeAction;
    protected bool isPlayerInMinAgroRange;

    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    protected EnemyCollisionDetector enemyCollisionDetector =>
        m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

    private Movement m_movement;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public StanState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        StunStateData data) 
        : base(fsm, core, animBoolName, entity)
    {
        this.data = data;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isGrounded = enemyCollisionDetector.CheckGrounded();
        performCloseRangeAction = enemyCollisionDetector.CheckPlayerInCloseRangeAction();
        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isStunTimeOver = false;
        isMovementSropped = false;
        movement.SetVelocity(data.stunKnockbackSpeed, data.stunKnockbackAngle, entity.lastDamageDirection);
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
            movement.SetVelocityX(0f);
        }
    }
}