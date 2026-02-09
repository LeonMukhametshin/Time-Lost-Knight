using UnityEngine;

public class EnemyFirst : Entity
{
    public EnemyFirstIdleState idleState { get; private set; }
    public EnemyFirstMoveState moveState { get; private set; }

    [SerializeField] private IdleStateData m_idleData;
    [SerializeField] private MoveStateData m_moveData;

    public override void Start()
    {
        base.Start();

        moveState = new EnemyFirstMoveState(fsm, this, "move", m_moveData, this);
        idleState = new EnemyFirstIdleState(fsm, this, "idle", m_idleData, this);

        fsm.Initialize(moveState);
    }
}   