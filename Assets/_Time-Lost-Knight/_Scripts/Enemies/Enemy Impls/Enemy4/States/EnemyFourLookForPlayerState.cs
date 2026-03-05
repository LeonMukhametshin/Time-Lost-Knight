using UnityEngine;

public class EnemyFourLookForPlayerState : LookForPlayerState
{
    private Vector2? isInAgroZone;

    public EnemyFourLookForPlayerState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity,
        LookForPlayerStateData data) 
        : base(fsm, core, animBoolName, entity, data)
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

        if(isInAgroZone is not null)
        {
            fsm.ChangeState<EnemyFourAttackState>();
        }
        else if(isAllTurnsTimeDone)
        {
            fsm.ChangeState<EnemyFourMoveState>();
        }
    }
}
