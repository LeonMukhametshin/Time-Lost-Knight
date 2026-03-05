using UnityEngine;

public class EnemyFourMoveState : MoveState
{
    protected Vector2? isInAgroZone;

    private Transform[] m_waypoints;

    private int m_currentIndex;
    private int m_direction = 1;

    public EnemyFourMoveState(EntityFSM fsm, Core core, 
        string animBoolName, Entity entity, 
        MoveStateData data, Transform[] waypoints) 
        : base(fsm, core, animBoolName, entity, data)
    {
        m_waypoints = waypoints;
    }

    public override void DoCheck()
    {
        base.DoCheck();

        isInAgroZone = enemyCollisionDetector.GetPlayerPositionInZone();
    }

    public override void Update()
    {
        movement.MoveTarget(data.movementSpeed);

        if(isInAgroZone is not null)
        {
            fsm.ChangeState<EnemyFourPlayerDetectedState>();
        }

        else if ((entity.transform.position - m_waypoints[m_currentIndex].position).sqrMagnitude < 0.0025f)
        {
            fsm.ChangeState<EnemyFourLookForPlayerState>();
        }
    }

    protected override void Move()
    {
        CheckFlip(m_waypoints[m_currentIndex].position);

        if (Vector2.Distance(entity.transform.position, m_waypoints[m_currentIndex].position) > 0.1f)
        {
            return;
        }

        m_currentIndex += m_direction;

        if (m_currentIndex >= m_waypoints.Length)
        {
            m_direction = -1;
            m_currentIndex = m_waypoints.Length - 2;
        }
        else if (m_currentIndex < 0)
        {
            m_direction = 1;
            m_currentIndex = 0;
        }

        movement.SetTarget(m_waypoints[m_currentIndex]);
    }

    private void CheckFlip(Vector3 targetPosition)
    {
        float dir = targetPosition.x - entity.transform.position.x;

        if (Mathf.Sign(dir) != flipController.facingDirection)
        {
            flipController.Flip();
        }
    }
}