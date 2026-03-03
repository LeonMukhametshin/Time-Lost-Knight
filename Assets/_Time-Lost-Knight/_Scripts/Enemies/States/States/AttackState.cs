using UnityEngine;

public class AttackState : EnemyState, IAnimationTrigger
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    protected Vector3 playerPosition { get; private set; }

    protected Movement movement => 
        m_movement ??= core.GetCoreComponent<Movement>();

    private EnemyCollisionDetector enemyCollisionDetector => 
        m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();

    private Movement m_movement;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public AttackState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, Transform attackPosition) 
        : base(fsm, core, animBoolName, entity)
    {
        this.attackPosition = attackPosition;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isPlayerInMinAgroRange = enemyCollisionDetector.CheckPlayerInMinAgroRange();
    }

    public override void Enter()
    {
        base.Enter();

        isAnimationFinished = false;
        movement.SetVelocityX(0f);

        playerPosition = enemyCollisionDetector.GetPlayerPositionInMaxAgroRange();
    }

    public virtual void TriggerAnimation() { }

    public virtual void FinishAnimation() =>
        isAnimationFinished = true;

    protected void SetLayer(GameObject visualEffect) =>
        visualEffect.layer = entity.gameObject.layer;
}