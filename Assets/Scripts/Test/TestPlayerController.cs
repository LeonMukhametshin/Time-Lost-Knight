using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestPlayerController : MonoBehaviour
{
    [Header("Horizontal Movement Settings: ")]
    [SerializeField] private float m_walkSpeed = 1f;

    [Header("Vertical Movement Settings: ")]
    [SerializeField] private float m_jumpForce = 10f;
    [SerializeField] private int m_jumpBufferFrames;
    [SerializeField] private int m_maxAitJumps;

    private int m_jumpBufferCounter;
    private int m_airJumpCounter = 0;

    [Header("Ground Check Settings: ")]
    [SerializeField] private Transform m_groundCheckPoint;
    [SerializeField] private float m_groundCheckY = 0.2f;
    [SerializeField] private float m_groundCheckX = 0.2f;
    [SerializeField] private LayerMask m_whatIsGround;

    [SerializeField] private Animator m_animator;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private PlayerStateList m_playerStateList;
    
    
    [Header("Dash Settings")]
    
    [SerializeField] private float m_dashSpeed;
    [SerializeField] private float m_dashTime;
    [SerializeField] private float m_dashCooldown;

    private float xAxis;
    private float m_coyoteTimeCounter = 0;
    [SerializeField] float m_coyoteTime;

    private float m_gravity;
    private bool m_canDash = true;
    private bool m_dashed;

    [SerializeField] private GameObject m_dashEffect;

    private void Start()
    {
        m_gravity = m_rigidbody.gravityScale;
    }   

    private void Update()
    {
        GetInput();
        UpdateJumpVariables();
        if(m_playerStateList.Dashing) return;
        Flip();
        Move();   
        Jump();
        StartDash();
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

    private void StartDash()
    {
        if (Keyboard.current.leftShiftKey.wasPressedThisFrame && m_canDash && !m_dashed)
        {
            StartCoroutine(Dash());
            m_dashed = true;
        }
        if(Grounded())
        {
            m_dashed = false;
        }
    }

    private IEnumerator Dash()
    {
        Debug.Log("Dash");
        m_canDash = false;
        m_playerStateList.Dashing = true;
        m_animator.SetTrigger("Dashing");
        m_rigidbody.gravityScale = 0;
        m_rigidbody.linearVelocityX = transform.localScale.x * m_dashSpeed;
        if(Grounded())
        {
            Instantiate(m_dashEffect, transform);
        }

        yield return new WaitForSeconds(m_dashTime);

        m_rigidbody.gravityScale = m_gravity;
        m_playerStateList.Dashing = false;

        yield return new WaitForSeconds(m_dashCooldown);

        m_canDash = true;
    }
    private void Jump()
    {
        if(Input.GetButtonUp("Jump") && m_rigidbody.linearVelocityX > 0)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, 0);

            m_playerStateList.Jumping = false;
        }

        if(!m_playerStateList.Jumping)
        {
            if(m_jumpBufferCounter > 0 && m_coyoteTimeCounter > 0)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, m_jumpForce);
                m_playerStateList.Jumping = true;
            }
            else if(!Grounded() && m_airJumpCounter < m_maxAitJumps && (Input.GetButtonDown("Jump")))
            {
                m_playerStateList.Jumping = true;

                m_airJumpCounter++;

                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, m_jumpForce);
            }
        }

        m_animator.SetBool("Jumping", !Grounded());
    }

    private void UpdateJumpVariables()
    {
        if(Grounded())
        {
            m_playerStateList.Jumping = false;
            m_coyoteTimeCounter = m_coyoteTime;
            m_airJumpCounter = 0;
        }
        else
        {
            m_coyoteTimeCounter -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Jump"))
        {
            m_jumpBufferCounter = m_jumpBufferFrames;
        }
        else
        {
            m_jumpBufferCounter--;
        }
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
