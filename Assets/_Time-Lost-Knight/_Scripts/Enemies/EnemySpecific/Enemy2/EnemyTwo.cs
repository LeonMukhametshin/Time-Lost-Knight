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


    [Header("DATAS")]
    [SerializeField] private IdleStateData m_idleStateData;
    [SerializeField] private MoveStateData m_moveStateData;
    [SerializeField] private PlayerDetectedData m_playerDetectedData;
    [SerializeField] private MeleeAttackStateData m_meleeAttackData;
    [SerializeField] private LookForPlayerStateData m_lookForPlayerData;
    [SerializeField] private StunStateData m_stunStateData;
    [SerializeField] private DeadStateData m_deadStateData;

    [SerializeField] private Transform m_meleeAttackPosition;

    public override void Start()
    {
        base.Start();

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

        fsm.Initialize(moveState);
    }

    public override void Damage(AttackDetails attackDetails)
    {
        base.Damage(attackDetails);

        if (isDead)
        {
            fsm.SetState(deadState);
        }
        else if (isStunned && fsm.currentState != stunState)
        {
            fsm.SetState(stunState);
        }
        else if(!CheckPlayerInMinAgroRange())
        {
            lookForPlayerState.SetTurnImmediately(true);
            fsm.SetState(lookForPlayerState);
        }
    }

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.DrawWireSphere(m_meleeAttackPosition.position, m_meleeAttackData.attackRadius);
    }
}