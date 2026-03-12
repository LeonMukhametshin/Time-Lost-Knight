using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class PlanformMover
{
    [SerializeField] private Transform[] m_points;
    [SerializeField][Min(0.01f)] private float m_speed = 2f;

    private Transform m_transform;
    private Rigidbody2D m_rb;

    private int m_currentIndex = 0;
    private int m_nextIndex = 0;
    private int m_direction = 1;

    private bool m_isMoving;

    private Vector2 m_targetPosition;

    public void Initialize(Rigidbody2D rb, Transform transform)
    {
        if (rb == null)
            throw new InvalidOperationException("PlatformMover: Rigidbody2D is not assigned.");

        if (m_points == null || m_points.Length < 2)
            throw new InvalidOperationException("PlatformMover requires at least 2 points.");

        foreach (var point in m_points)
        {
            if (point == null)
                throw new InvalidOperationException("PlatformMover contains null waypoint.");
        }

        m_rb = rb;
        m_transform = transform;

        m_currentIndex = 0;
        m_nextIndex = 0;

        m_rb.position = m_points[0].position;
        m_targetPosition = m_points[0].position;
    }

    public void Activate()
    {
        if (m_isMoving)
        {
            return;
        }

        m_nextIndex = GetNextIndex();
        m_targetPosition = m_points[m_nextIndex].position;

        m_isMoving = true;
    }

    public void FixedTick()
    {
        if (!m_isMoving)
        {
            return;
        }

        Vector2 newPos = Vector2.MoveTowards(
            m_rb.position,
            m_targetPosition,
            m_speed * Time.fixedDeltaTime);

        m_transform.position = newPos;
        //m_rb.MovePosition(newPos);

        if (Vector2.Distance(newPos, m_targetPosition) < 0.01f)
        {
            OnReachedPoint();
        }
    }

    private void OnReachedPoint()
    {
        m_currentIndex = m_nextIndex;
        m_isMoving = false;
    }

    private int GetNextIndex()
    {
        int next = m_currentIndex + m_direction;

        if (next >= m_points.Length || next < 0)
        {
            m_direction *= -1;
            next = m_currentIndex + m_direction;
        }

        return next;
    }

    public bool CanMove() =>
        !m_isMoving;
}
