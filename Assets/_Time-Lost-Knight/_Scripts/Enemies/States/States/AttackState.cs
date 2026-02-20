using UnityEngine;

public class AttackState : EnemyState, IAnimationAttackTrigger
{
    protected Transform attackPosition;

    protected bool isAnimationFinished;
    protected bool isPlayerInMinAgroRange;

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

        entity.animationToFSM.attackState = this;
        isAnimationFinished = false;
        movement.SetVelocityX(0f);
    }

    public virtual void TriggerAttack() { }

    public virtual void FinishAttack() =>
        isAnimationFinished = true;
}

public interface IAnimationAttackTrigger
{
    void TriggerAttack();
    void FinishAttack();
}