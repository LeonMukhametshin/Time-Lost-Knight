using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] private Transform m_pointA;
	[SerializeField] private Transform m_pointB;
	[SerializeField] private float m_speed =2f;
	[SerializeField] private bool m_loop = true;

	private Rigidbody2D m_rb;
	private Vector2 m_target;
	private Vector2 m_prevPosition;
	private Vector2 m_velocity;

	private void Awake()
	{
		m_rb = GetComponent<Rigidbody2D>();
		m_rb.bodyType = RigidbodyType2D.Kinematic;

		if (m_pointA == null || m_pointB == null)
		{
			enabled = false;
			return;
		}

		m_target = m_pointB.position;
		m_prevPosition = m_rb.position;
	}

	private void FixedUpdate()
	{
		Vector2 current = m_rb.position;
		Vector2 next = Vector2.MoveTowards(current, m_target, m_speed * Time.fixedDeltaTime);
		m_rb.MovePosition(next);

		m_velocity = (next - m_prevPosition) / Time.fixedDeltaTime;
		m_prevPosition = next;

		if (Vector2.Distance(next, m_target) <0.001f)
		{
			m_target = m_target == (Vector2)m_pointB.position ? m_pointA.position : m_pointB.position;
			if (!m_loop) enabled = false;
		}
	}

	public Vector2 GetVelocity()
	{
		return m_velocity;
	}

	public void AttachTarget(Rigidbody2D targetRb)
	{
		// TODO
	}

	public void DetachTarget(Rigidbody2D targetRb)
	{
		// TODO
	}
}
