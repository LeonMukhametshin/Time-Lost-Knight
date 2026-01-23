using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlatform : MonoBehaviour
{
    //TODO animation 
	public Action StateChanged;

	[SerializeField] private Transform[] m_points;
	[SerializeField][Min(0)] private float m_duration;

    [SerializeField] private bool m_isLooped;

    [SerializeField] private Ease m_ease = Ease.Linear;
    [SerializeField] private LoopType m_loopType = LoopType.Yoyo;

    private int m_currentIndex = 0;
    private int m_direction = 1;

    private Tween m_tween;
    private bool m_isMoving = false;

    private void OnDestroy() =>
        m_tween?.Kill(true);

    private void Awake()
	{
		if(m_points is null || m_points.Length < 0)
		{
			throw new Exception("MovingPlatform requires at least 2 points");
		}

		transform.position = m_points[0].position;

        if (m_isLooped)
        {
            Activate();
        }
    }

    private void Update()
    {
        // Test
        if(!m_isLooped && Keyboard.current.oKey.wasPressedThisFrame)
		{
            Activate();
        }
    }

    public void Activate()
    {
		if(m_isMoving)
		{
			return;
		}

        int nextIndex = GetNextIndex();
        m_isMoving = true;

        m_tween = transform
            .DOMove(m_points[nextIndex].position, m_duration)
			.SetEase(m_ease)
			.OnComplete( () =>
			{
				m_currentIndex = nextIndex;
				m_isMoving = false;
				StateChanged?.Invoke();

				if(m_isLooped )
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player))
        {
            return;
        }
       

        Attach(collision.transform, collision.rigidbody);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player))
        {
            return;
        }
            
        Detach(collision.transform, collision.rigidbody);
    }

    private void Attach(Transform target, Rigidbody2D rb)
    {
        target.SetParent(transform);

        if (rb is not null)
        {
            rb.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void Detach(Transform target, Rigidbody2D rb)
    {
        target.SetParent(null);

        if (rb is not null)
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}