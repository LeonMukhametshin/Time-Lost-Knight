using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerJump : MonoBehaviour
{
    [SerializeField] private float m_jumpForce;
    [SerializeField] private Rigidbody2D m_rigidbody2D;
    [SerializeField] private LayerMask _groundLayer;

    [SerializeField] private Transform m_groundChecker;
    [SerializeField] private float m_checkDistance = 0.4f;

    [SerializeField] private PhysicsMaterial2D m_jumpMaterial;

    private bool m_isJumping = false;

    public void Jump()
    {
        if (!m_isJumping)
        {
            m_rigidbody2D.AddForce(Vector2.up * m_jumpForce, ForceMode2D.Impulse);
            m_rigidbody2D.sharedMaterial = m_jumpMaterial;

            m_isJumping = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        RaycastHit2D hit = Physics2D.Raycast(
             m_groundChecker.position,
             Vector2.down,
             m_checkDistance,
             _groundLayer
        );

        if (hit.collider is not null)
        {
            m_isJumping = false;
            m_rigidbody2D.sharedMaterial = null;
        }
        else
        {
            m_isJumping = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_groundChecker.position, m_checkDistance);
    }
}