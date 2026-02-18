using UnityEngine;

public class EnemyTwo : Entity
{
    public EnemyTwoIdleState idleState { get; private set; }
    public EnemyTwoMoveState moveState { get; private set; }
    public EnemyTwoPlayerDetectedState playerDetectedState { get; private set; }
    public EnemyTwoMeleeAttackState meleeAttackState { get; private set; }
    public EnemyTwoLookForPlayerState lookForPlayerState { get; private set; }
    public EnemyTwoStunState stunState { get; private set; }
    public EnemyTwoDeadState deadState { get; private set; }
    public EnemyTwoDodgeState dodgeState { get; private set; }
    public EnemyTwoRangeAttackState rangeAttackState { get; private set; }


    [Header("DATAS")]
    [SerializeField] private IdleStateData m_idleStateData;
    [SerializeField] private MoveStateData m_moveStateData;
    [SerializeField] private PlayerDetectedData m_playerDetectedData;
    [SerializeField] private MeleeAttackStateData m_meleeAttackData;
    [SerializeField] private LookForPlayerStateData m_lookForPlayerData;
    [SerializeField] private StunStateData m_stunStateData;
    [SerializeField] private DeadStateData m_deadStateData;
    [field: SerializeField] public DodgeStateData m_dodgeStateData { get; private set; }
    [SerializeField] public RangeAttackData m_rangeAttackData;

    [SerializeField] private Transform m_meleeAttackPosition;
    [SerializeField] private Transform m_rangeAttackPosition;

    public override void Awake()
    {
        base.Awake();

        idleState = new EnemyTwoIdleState(fsm, this,
            EnemyAnimationConst.IDLE, m_idleStateData, this);
        moveState = new EnemyTwoMoveState(fsm, this, 
            EnemyAnimationConst.MOVE, m_moveStateData, this);
        playerDetectedState = new EnemyTwoPlayerDetectedState(fsm, this, 
            EnemyAnimationConst.PLAYER_DETECTED, m_playerDetectedData, this);
        meleeAttackState = new EnemyTwoMeleeAttackState(fsm, this, 
            EnemyAnimationConst.MELEE_ATTACK, m_meleeAttackPosition, m_meleeAttackData, this);
        lookForPlayerState = new EnemyTwoLookForPlayerState(fsm, this, 
            EnemyAnimationConst.LOOK_FOR_PLAYER, m_lookForPlayerData, this);
        stunState = new EnemyTwoStunState(fsm, this, 
            EnemyAnimationConst.STUN, m_stunStateData, this);
        deadState = new EnemyTwoDeadState(fsm, this,
            EnemyAnimationConst.DEAD, m_deadStateData, this);
        dodgeState = new EnemyTwoDodgeState(fsm, this,
            EnemyAnimationConst.DODGE, m_dodgeStateData, this);
        rangeAttackState = new EnemyTwoRangeAttackState(fsm, this,
            EnemyAnimationConst.RANGED_ATTACK, m_rangeAttackPosition, m_rangeAttackData, this);

        animationToFSM.Initialize(meleeAttackState);

        fsm.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(m_meleeAttackPosition.position, m_meleeAttackData.attackRadius);
    }
}