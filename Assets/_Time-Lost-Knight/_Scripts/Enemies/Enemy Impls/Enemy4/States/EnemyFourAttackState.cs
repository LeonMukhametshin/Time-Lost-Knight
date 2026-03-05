using UnityEngine;

public class EnemyFourAttackState : RangeAttackState
{
    private Vector2? isInAgroZone;

    public EnemyFourAttackState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity, 
        Transform attackPosition, RangeAttackData data) 
        : base(fsm, core, animBoolName, entity,
            attackPosition, data)
    {
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isInAgroZone = enemyCollisionDetector.GetPlayerPositionInZone();
    }

    public override void Update()
    {
        base.Update();

        if (!isAnimationFinished)
        {
            return;
        }

        if (isInAgroZone is not null)
        {
            fsm.ChangeState<EnemyFourPlayerDetectedState>();
        }
        else
        {
            fsm.ChangeState<EnemyFourLookForPlayerState>();
        }
    }

    public override void CheckPlayerPosition()
    {
        playerPosition = enemyCollisionDetector.GetPlayerPositionInZone();
    }
}
