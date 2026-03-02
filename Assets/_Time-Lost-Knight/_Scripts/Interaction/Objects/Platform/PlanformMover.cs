using DG.Tweening;
using System;
using UnityEngine;

[Serializable]
public class PlanformMover
{
    [SerializeField] private Transform m_transform;
    [SerializeField] private Transform[] m_points;
    [SerializeField][Min(0)] private float m_duration;

    [SerializeField] private Ease m_ease = Ease.Linear;

    [SerializeField] private bool m_isLooped;

    private int m_currentIndex = 0;
    private int m_direction = 1;

    private Tween m_tween;
    private bool m_isMoving = false;

    public bool isLooped => m_isLooped;
    public bool isMoving => m_isMoving;

    public void Initialize()
    {
        if (m_transform == null || m_points == null || m_points.Length < 2)
        {
            throw new Exception("MovingPlatform requires at least 2 points");
        }

        m_transform.position = m_points[0].position;

        if (m_isLooped)
        {
            Activate();
        }
    }

    public void Activate()
    {
        int nextIndex = GetNextIndex();
        m_isMoving = true;

        m_tween = m_transform
            .DOMove(m_points[nextIndex].position, m_duration)
            .SetEase(m_ease)
            .OnComplete(() =>
            {
                m_currentIndex = nextIndex;
                m_isMoving = false;

                if (m_isLooped)
                {
                    Activate();
                }
            });
    }

    private int GetNextIndex()
    {
        int next = m_currentIndex + m_direction;

        if (next >= m_points.Length)
        {
            m_direction = -1;
            next = m_points.Length - 2;
        }
        else if (next < 0)
        {
            m_direction = 1;
            next = 1;
        }

        return next;
    }

    public bool CanMove() =>
        !(m_isMoving && m_isLooped);
}
