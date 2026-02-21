using UnityEngine;

public class EnemyTwo : Entity
{  
    [Header("DATAS")]
    [SerializeField] private IdleStateData m_idleStateData;
    [SerializeField] private MoveStateData m_moveStateData;
    [SerializeField] private PlayerDetectedData m_playerDetectedData;
    [SerializeField] private MeleeAttackStateData m_meleeAttackData;
    [SerializeField] private LookForPlayerStateData m_lookForPlayerData;
    [SerializeField] private StunStateData m_stunStateData;
    [SerializeField] private DeadStateData m_deadStateData;
    [SerializeField] public RangeAttackData m_rangeAttackData;
    [SerializeField] private DodgeStateData m_dodgeStateData;

    [SerializeField] private Transform m_meleeAttackPosition;
    [SerializeField] private Transform m_rangeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        fsm.Initialize(
            new EnemyTwoIdleState(fsm, core, EnemyAnimationConst.IDLE, this, m_idleStateData),
            new EnemyTwoMoveState(fsm, core, EnemyAnimationConst.MOVE, this, m_moveStateData),
            new EnemyTwoPlayerDetectedState(fsm, core, EnemyAnimationConst.PLAYER_DETECTED, this, m_playerDetectedData),
            new EnemyTwoDodgeState(fsm, core, EnemyAnimationConst.DODGE, this, m_dodgeStateData),
            new EnemyTwoRangeAttackState(fsm, core, EnemyAnimationConst.RANGED_ATTACK, this, m_rangeAttackPosition, m_rangeAttackData),
            new EnemyTwoMeleeAttackState(fsm, core, EnemyAnimationConst.MELEE_ATTACK, this, m_meleeAttackPosition, m_meleeAttackData),
            new EnemyTwoLookForPlayerState(fsm, core, EnemyAnimationConst.LOOK_FOR_PLAYER, this, m_lookForPlayerData),
            new EnemyTwoStunState(fsm, core, EnemyAnimationConst.STUN, this, m_stunStateData),
            new EnemyTwoDeadState(fsm, core, EnemyAnimationConst.DEAD, this, m_deadStateData));


        animationToFSM.Initialize(fsm);

        fsm.ChangeState<EnemyTwoIdleState>();
    }
}