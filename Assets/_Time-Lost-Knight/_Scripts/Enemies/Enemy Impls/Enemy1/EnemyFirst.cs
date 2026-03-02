using UnityEngine;

public class EnemyFirst : Entity
{
    [SerializeField] private IdleStateData m_idleData;
    [SerializeField] private MoveStateData m_moveData;
    [SerializeField] private PlayerDetectedData m_playerDetectedData;
    [SerializeField] private ChargeStateData m_chargeStateData;
    [SerializeField] private LookForPlayerStateData m_lookForPlayerData;
    [SerializeField] private MeleeAttackStateData m_meleeAttackStateData;
    [SerializeField] private StunStateData m_stanStateData;
    [SerializeField] private DeadStateData m_deadStateData;

    [SerializeField] private Transform m_meleeAttackPoint;

    public override void Awake()
    {
        base.Awake();

        fsm.Initialize(
            new EnemyFirstIdleState(fsm, core, EnemyAnimationConst.IDLE, this, m_idleData),
            new EnemyFirstMoveState(fsm, core, EnemyAnimationConst.MOVE, this, m_moveData),
            new EnemyFirstPlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, m_playerDetectedData),
            new EnemyFirstChargeState(fsm, core, EnemyAnimationConst.CHARGE, this, m_chargeStateData),
            new EnemyFirstLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, m_lookForPlayerData),
            new EnemyFirstMeleeAttackState(fsm, core, EnemyAnimationConst.MELEE_ATTACK, this, m_meleeAttackPoint, m_meleeAttackStateData),
            new EnemyFirstStanState(fsm, core, EnemyAnimationConst.STUN, this, m_stanStateData),
            new EnemyFirstDeadState(fsm, core, EnemyAnimationConst.DEAD, this, m_deadStateData));

        animationToFSM.Initialize(fsm);
        fsm.ChangeState<EnemyFirstIdleState>();
    }
}