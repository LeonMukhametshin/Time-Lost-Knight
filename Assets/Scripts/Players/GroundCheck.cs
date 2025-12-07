using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private Transform m_transform;
    private float m_checkDistance;
    private LayerMask m_groundLayer;

    public bool IsGrounded { get; private set; }

    public GroundCheck(Transform transform, float checkDistance, LayerMask groundLayer)
    {
        m_transform = transform;
        m_checkDistance = checkDistance;
        m_groundLayer = groundLayer;
    }

    public void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            m_transform.position,
            Vector2.down,
            m_checkDistance,
            m_groundLayer
        );

        IsGrounded = hit.collider != null;

        Debug.DrawRay(m_transform.position, Vector2.down * m_checkDistance,
                     IsGrounded ? Color.green : Color.red);
    }
}
