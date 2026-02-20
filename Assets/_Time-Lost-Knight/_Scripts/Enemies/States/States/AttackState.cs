using UnityEngine;

public class AttackState : EnemyState
{
    protected Transform attackPosition;
    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

    protected Movement movement
    {
        get => m_movement ??= core.GetCoreComponent<Movement>();
    }

    private EnemyCollisionDetector enemyCollisionDetector
    {
        get => m_enemyCollisionDetector ??= core.GetCoreComponent<EnemyCollisionDetector>();
    }

    private Movement m_movement;
    private EnemyCollisionDetector m_enemyCollisionDetector;

    public AttackState(float startTime, string animBoolName,
        Entity entity, Transform attackPosition) 
        : base(startTime, animBoolName, entity)
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

        entity.animationToFSM.attackState = this;
        isAnimationFinished = false;
        movement.SetVelocityX(0f);
    }

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() =>
        isAnimationFinished = true;
}