using UnityEngine;

public class EnemyFour : Entity
{
    public override void Awake()
    {
        base.Awake();

        fsm.Initialize(
            //new EnemyFourIdleState(fsm, core, EnemyAnimationConst.IDLE, ),
            //new EnemyFourLookForPlayerState()
            //new EnemyFourPlayerDetectedState()
            //new EnemyFourAttackState()
            );

        animationToFSM.Initialize(fsm);

        fsm.ChangeState<EnemyFourIdleState>();
    }
}

public class EnemyFourIdleState : IdleState
{
    public EnemyFourIdleState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        IdleStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }
}

public class EnemyFourLookForPlayerState : LookForPlayerState
{
    public EnemyFourLookForPlayerState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity,
        LookForPlayerStateData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }
}

public class EnemyFourPlayerDetectedState : PlayerDetectedState
{
    public EnemyFourPlayerDetectedState(EntityFSM fsm, Core core, string animBoolName, 
        Entity entity, PlayerDetectedData data) 
        : base(fsm, core, animBoolName, entity, data)
    {
    }
}

public class EnemyFourAttackState : RangeAttackState
{
    public EnemyFourAttackState(EntityFSM fsm, Core core,
        string animBoolName, Entity entity, 
        Transform attackPosition, RangeAttackData data) 
        : base(fsm, core, animBoolName, entity,
            attackPosition, data)
    {
    }
}