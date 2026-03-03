using UnityEngine;

public class EnemyFirst : Entity
{
    [SerializeField] private Transform m_meleeAttackPoint;

    public override void Awake()
    {
        base.Awake();

        var enemyFirstData = data as EnemyFirstData;

        fsm.Initialize(
            new EnemyFirstIdleState(fsm, core, EnemyAnimationConst.IDLE, this, enemyFirstData.idle),
            new EnemyFirstMoveState(fsm, core, EnemyAnimationConst.MOVE, this, enemyFirstData.move),
            new EnemyFirstPlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, enemyFirstData.playerDetected),
            new EnemyFirstChargeState(fsm, core, EnemyAnimationConst.CHARGE, this, enemyFirstData.chargeState),
            new EnemyFirstLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, enemyFirstData.lookForPlayer),
            new EnemyFirstMeleeAttackState(fsm, core, EnemyAnimationConst.MELEE_ATTACK, this, m_meleeAttackPoint, enemyFirstData.meleeAttackState),
            new EnemyFirstStanState(fsm, core, EnemyAnimationConst.STUN, this, enemyFirstData.stanState));

        animationToFSM.Initialize(fsm);
        fsm.ChangeState<EnemyFirstIdleState>();
    }
}