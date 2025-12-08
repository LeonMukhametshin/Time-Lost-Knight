
using UnityEditor.ShaderGraph;
using UnityEngine;

public class TestPlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings: ")]
    [SerializeField] private float m_walkSpeed = 1f;

    [Header("Ground Check Settings: ")]
    [SerializeField] private float m_jumpForce = 10f;
    [SerializeField] private Transform m_groundCheckPoint;
    [SerializeField] private float m_groundCheckY = 0.2f;
    [SerializeField] private float m_groundCheckX = 0.2f;
    [SerializeField] private LayerMask m_whatIsGround;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody2D m_rigidbody;

    private float xAxis;

    private void Update()
    {
        GetInput();
        Flip();
        Move();   
        Jump();
    }

    private void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
    }

    private void Flip()
    {
        if(xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
        else if(xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
    }

    private void Move()
    {
        m_rigidbody.linearVelocity = new Vector2(m_walkSpeed * xAxis, m_rigidbody.linearVelocityY);
        m_animator.SetBool("Walking", m_rigidbody.linearVelocityX != 0 && Grounded());
    }

    private void Jump()
    {
        if(Input.GetButtonUp("Jump") && m_rigidbody.linearVelocityX > 0)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, 0);
        }
        if(Input.GetButtonDown("Jump") && Grounded())
        {
             m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, m_jumpForce);
        }

        m_animator.SetBool("Jumping", !Grounded());
    }

    private bool Grounded()
    {
        if(Physics2D.Raycast(m_groundCheckPoint.position, Vector2.down, m_groundCheckY, m_whatIsGround)
            || Physics2D.Raycast(m_groundCheckPoint.position + new Vector3(m_groundCheckX, 0, 0), Vector2.down, m_groundCheckY, m_whatIsGround)
            || Physics2D.Raycast(m_groundCheckPoint.position + new Vector3(-m_groundCheckX, 0, 0), Vector2.down, m_groundCheckY, m_whatIsGround)
        )
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * m_groundCheckY);
    }
}
