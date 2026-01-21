using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] private Transform m_pointA;
	[SerializeField] private Transform m_pointB;
	[SerializeField] private float m_speed =2f;
	[SerializeField] private bool m_loop = true;

	private Vector2 m_target;
	private Vector2 m_prevPosition;
	private Vector2 m_velocity;

	private void Start()
	{
		if (m_pointA == null || m_pointB == null)
		{
			enabled = false;
			return;
		}

		m_target = m_pointB.position;
		m_prevPosition = (Vector2)transform.position;
	}

	private void FixedUpdate()
	{
		Vector2 current = transform.position;
		Vector2 next = Vector2.MoveTowards(current, m_target, m_speed * Time.fixedDeltaTime);
		transform.position = new Vector3(next.x, next.y, transform.position.z);

		m_velocity = (next - m_prevPosition) / Time.fixedDeltaTime;
		m_prevPosition = next;

		if (Vector2.Distance(next, m_target) <0.01f)
		{
			m_target = m_target == (Vector2)m_pointB.position ? (Vector2)m_pointA.position : (Vector2)m_pointB.position;
			if (!m_loop) enabled = false;
		}
	}

	public Vector2 GetVelocity()
	{
		return m_velocity;
	}

	public void AttachTarget(Rigidbody2D targetRb)
	{
		if (targetRb == null) return;
		targetRb.transform.SetParent(transform);
	}

	public void DetachTarget(Rigidbody2D targetRb)
	{
		if (targetRb == null) return;
		targetRb.transform.SetParent(null);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(transform);
			if (collision.rigidbody != null) AttachTarget(collision.rigidbody);
		}
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			collision.transform.SetParent(null);
			if (collision.rigidbody != null) DetachTarget(collision.rigidbody);
		}
	}
}
