using UnityEngine;

public class EnemyFirst : Entity
{
    public EnemyFirstIdleState idleState { get; private set; }
    public EnemyFirstMoveState moveState { get; private set; }
    public EnemyFirstPlayerDetectedState playerDetectedState { get; private set; }
    public EnemyFirstChargeState chargeState { get; private set; }
    public EnemyFirstLookForPlayerState lookForPlayerState { get; private set; }
    public EnemyFirstMeleeAttackState meleeAttackState { get; private set; }
    public EnemyFirstStanState stanState { get; private set; }
    public EnemyFirstDeadState deadState { get; private set; }

    [SerializeField] private IdleStateData m_idleData;
    [SerializeField] private MoveStateData m_moveData;
    [SerializeField] private PlayerDetectedData m_playerDetectedData;
    [SerializeField] private ChargeStateData m_chargeStateData;
    [SerializeField] private LookForPlayerStateData m_lookForPlayerData;
    [SerializeField] private MeleeAttackStateData m_meleeAttackStateData;
    [SerializeField] private StunStateData m_stanStateData;
    [SerializeField] private DeadStateData m_deadStateData;

    [SerializeField] private Transform m_meleeAttackPoint;

    public override void Start()
    {
        base.Start();

        moveState = new EnemyFirstMoveState(fsm, this, EnemyAnimationConst.MOVE, m_moveData, this);
        idleState = new EnemyFirstIdleState(fsm, this, EnemyAnimationConst.IDLE, m_idleData, this);
        playerDetectedState = new EnemyFirstPlayerDetectedState(fsm, this, EnemyAnimationConst.PLAYER_DETECTED, m_playerDetectedData, this);
        chargeState = new EnemyFirstChargeState(fsm, this, EnemyAnimationConst.CHARGE, m_chargeStateData, this);
        lookForPlayerState = new EnemyFirstLookForPlayerState(fsm, this, EnemyAnimationConst.LOOK_FOR_PLAYER, m_lookForPlayerData, this);
        meleeAttackState = new EnemyFirstMeleeAttackState(fsm, this, EnemyAnimationConst.MELEE_ATTACK, m_meleeAttackPoint, m_meleeAttackStateData, this);
        stanState = new EnemyFirstStanState(fsm, this, EnemyAnimationConst.STUN, m_stanStateData, this);
        deadState = new EnemyFirstDeadState(fsm, this, EnemyAnimationConst.DEAD, m_deadStateData, this);

        fsm.Initialize(moveState);
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(m_meleeAttackPoint.position, m_meleeAttackStateData.attackRadius);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            fsm.SetState(deadState);
        }
        else if (isStunned && fsm.currentState != stanState)
        {
            fsm.SetState(stanState);
        }
    }
}