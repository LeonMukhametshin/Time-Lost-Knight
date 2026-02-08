using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameInput m_gameInput;

    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private Animator m_animator;
    [SerializeField] private Transform m_groundCheck;
    [SerializeField] private Transform m_ledgeCheck;

    [SerializeField] private float m_movementSpeed = 10f;
    [SerializeField] private float m_jumpForce = 5f;
    [SerializeField] [Min(1)] private int m_amountOfJumps = 1;

    private int m_amountOfJumpsLeft;
    private int m_facingDirection = 1;
    private int m_lassWallJumpDirection;

    private bool m_isFasingRight = true;
    private bool m_isWalking = false;
    private bool m_canNormalJump = false;
    private bool m_canWallJump = false;
    private bool m_isTouchingWall = false;
    private bool m_isWallSliding;
    private bool m_isGrounded;
    private bool m_isAttemptingToJump;
    private bool m_canMove;
    private bool m_canFlip;
    private bool m_hasWallJumped;
    private bool m_isTouchingLedge;
    private bool m_canClimbLedge = false;
    private bool m_ledgeDetacted;
    private bool m_checkJumpMultiplier;
    private bool m_isDashing;

    private Vector2 m_movementInputDirection;
    private Vector2 m_ledgePositionBotom;
    private Vector2 m_ledgePosition1;
    private Vector2 m_ledgePosition2;

    [SerializeField] private float m_grouncCheckRadius;
    [SerializeField] private LayerMask m_groundLayer;

    [SerializeField] private Transform m_wallCheck;
    [SerializeField] [Min(0)] private float m_wallCheckedDistance;
    [SerializeField] private float m_wallSlideSpeed;
    [SerializeField] private float m_movementForceInAir;
    [SerializeField] private float m_airDragMultiplier = 0.95f;
    [SerializeField] private float m_variableJumpHeightMultiplier = 0.5f;
    [SerializeField] private float m_wallHopForce;
    [SerializeField] private float m_wallJumpForce;
    [SerializeField] private float m_turnTimerSet = 0.1f;
    [SerializeField] private float m_wallJumpTimerSet = 0.5f;
    [SerializeField] private float m_ledgeClimbXOffset1 = 0f;
    [SerializeField] private float m_ledgeClimbYOffset1 = 0f;
    [SerializeField] private float m_ledgeClimbXOffset2 = 0f;
    [SerializeField] private float m_ledgeClimbYOffset2 = 0f;
    [SerializeField] private float m_dashTime; 
    [SerializeField] private float m_dashSpeed;
    [SerializeField] private float m_dashCooldown;
    [SerializeField] private float m_distanceBetweenImages;

    [SerializeField] private Vector2 m_wallHopDirection;
    [SerializeField] private Vector2 m_wallJumpDirection;

    private float m_jumpTimer;
    private float m_turnTimer;
    private float m_wallJumpTimer;
    private float m_dashTimeLeft;
    private float m_lastImageXPosition;
    private float m_lastDash;

    [SerializeField] private float m_jumpTimerSet = 0.15f;

    private void OnEnable()
    {
        m_gameInput = new GameInput();
        m_gameInput.Enable();
    }

    private void Start()
    {
        m_amountOfJumpsLeft = m_amountOfJumps;

        m_wallHopDirection.Normalize();
        m_wallJumpDirection.Normalize();
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        UpdateAnimations();
        CheckCanJump();
        CheckIfWallSliding();
        CheckJump();
        CheckLedgelimb();
        CheckDash();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckSurroundings();
    }

    private void CheckIfWallSliding()
    {
        m_isWallSliding = m_isTouchingWall 
            && m_movementInputDirection.x == m_facingDirection 
            && m_rigidbody.linearVelocityY < 0
            && !m_canClimbLedge
            ? true : false;
    }

    private void CheckCanJump()
    {
        if (m_isGrounded && m_rigidbody.linearVelocityY <= 0.01f)
        {
            m_amountOfJumpsLeft = m_amountOfJumps;
        }

        if(m_isTouchingWall)
        {
            m_canWallJump = true;
        }

        m_canNormalJump = m_amountOfJumpsLeft <= 0
            ? false : true;
    }

    private void CheckSurroundings()
    {
        m_isGrounded = Physics2D.OverlapCircle(m_groundCheck.position, m_grouncCheckRadius, m_groundLayer);
        m_isTouchingWall = Physics2D.Raycast(m_wallCheck.position, transform.right, m_wallCheckedDistance, m_groundLayer);
        m_isTouchingLedge = Physics2D.Raycast(m_ledgeCheck.position, transform.right, m_wallCheckedDistance, m_groundLayer); 

        if(m_isTouchingWall && !m_isTouchingLedge && !m_ledgeDetacted)
        {
            m_ledgeDetacted = true;
            m_ledgePositionBotom = m_wallCheck.position;
        }
    }

    private void CheckLedgelimb()
    {
        if(m_ledgeDetacted && !m_canClimbLedge)
        {
            m_canClimbLedge = true;

            if(m_isFasingRight)
            {
                m_ledgePosition1 = new Vector2(Mathf.Floor(m_ledgePositionBotom.x + m_wallCheckedDistance) - m_ledgeClimbXOffset1,
                    Mathf.Floor(m_ledgePositionBotom.y) + m_ledgeClimbYOffset1);
                m_ledgePosition2 = new Vector2(Mathf.Floor(m_ledgePositionBotom.x + m_wallCheckedDistance) + m_ledgeClimbXOffset2,
                   Mathf.Floor(m_ledgePositionBotom.y) + m_ledgeClimbYOffset2);
            }
            else
            {
                m_ledgePosition1 = new Vector2(Mathf.Ceil(m_ledgePositionBotom.x - m_wallCheckedDistance) + m_ledgeClimbXOffset1,
                  Mathf.Floor(m_ledgePositionBotom.y) + m_ledgeClimbYOffset1);
                m_ledgePosition2 = new Vector2(Mathf.Ceil(m_ledgePositionBotom.x - m_wallCheckedDistance) - m_ledgeClimbXOffset2,
                   Mathf.Floor(m_ledgePositionBotom.y) + m_ledgeClimbYOffset2);
            }

            m_canMove = false;
            m_canFlip = false;

            m_animator.SetBool("canClimbLedge", m_canClimbLedge);
        }

        if(m_canClimbLedge)
        {
            transform.position = m_ledgePosition1;
        }
    }

    public void FinishLedgeClimb()
    {
        m_canClimbLedge = false;
        transform.position = m_ledgePosition2;
        m_canMove = true;
        m_canFlip = true;
        m_ledgeDetacted = false;
        m_animator.SetBool("canClimbLedge", m_canClimbLedge);
    }

    private void UpdateAnimations()
    {
        m_animator.SetBool("isWalking", m_isWalking);
        m_animator.SetBool("isGrounded", m_isGrounded);
        m_animator.SetFloat("yVelocity", m_rigidbody.linearVelocityY);
        m_animator.SetBool("isWallSliding", m_isWallSliding);
    }

    private void CheckJump()
    {
        if(m_jumpTimer > 0)
        {
            if(!m_isGrounded && m_isTouchingWall && m_movementInputDirection.x != 0
                && m_movementInputDirection.x != m_facingDirection)
            {
                WallJump();
            }
            else if(m_isGrounded)
            {
                NormalJump();
            }
        }
        if(m_isAttemptingToJump)
        {
            m_jumpTimer -= Time.deltaTime;
        }

        if(m_wallJumpTimer > 0)
        {
            if(m_hasWallJumped && m_movementInputDirection.x == -m_lassWallJumpDirection)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, 0f);
                m_hasWallJumped = false;
            }
            else if(m_wallJumpTimer <= 0)
            {
                m_hasWallJumped = false;
            }
            else
            {
                m_wallJumpTimer -= Time.deltaTime;
            }
        }
    }

    private void NormalJump()
    {
        if (m_canNormalJump)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, m_jumpForce);
            m_amountOfJumpsLeft--;
            m_jumpTimer = 0;
            m_isAttemptingToJump = false;
            m_checkJumpMultiplier = true;
        }
    }

    private void WallJump()
    {  
        if (m_canWallJump)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, 0f);
            m_isWallSliding = false;
            m_amountOfJumpsLeft--;
            Vector2 forceToAdd = new Vector2(m_wallJumpForce * m_wallJumpDirection.x * m_movementInputDirection.x,
                m_wallJumpForce * m_wallJumpDirection.y);
            m_rigidbody.AddForce(forceToAdd, ForceMode2D.Impulse);
            m_jumpTimer = 0;
            m_isAttemptingToJump = false;
            m_checkJumpMultiplier = true;
            m_turnTimer = 0;
            m_canMove = true;
            m_canFlip = true;
            m_hasWallJumped = true;
            m_wallJumpTimer = m_wallJumpTimerSet;
            m_lassWallJumpDirection = -m_facingDirection;
        }
    }

    private void CheckMovementDirection()
    {
        if(m_isFasingRight && m_movementInputDirection.x < 0)
        {
            Flip();
        }
        else if(!m_isFasingRight && m_movementInputDirection.x > 0)
        {
            Flip();
        }

        if(!m_isTouchingLedge)
        {
            m_isWalking = m_rigidbody.linearVelocityX != 0
                ? true
                : false;
        }
    }

    private void Flip()
    {
        if(!m_isWallSliding && m_canFlip)
        {
            m_facingDirection *= -1;
            m_isFasingRight = !m_isFasingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }

    private void CheckInput()
    {
        m_movementInputDirection = m_gameInput.Player.Move.ReadValue<Vector2>();

        if (m_gameInput.Player.Jump.WasPerformedThisFrame())
        {
            if (m_isGrounded || (m_amountOfJumpsLeft > 0 && m_isTouchingWall))
            {
                NormalJump();
            }
            else
            {
                m_jumpTimer = m_jumpTimerSet;
                m_isAttemptingToJump = true;
            }
        }

        if(m_gameInput.Player.Move.WasPressedThisFrame() && m_isTouchingWall)
        {
            if(!m_isGrounded && m_movementInputDirection.x != m_facingDirection)
            {
                m_canMove = false;
                m_canFlip = false;

                m_turnTimer = m_turnTimerSet;
            }
        }

        if(m_turnTimer >= 0)
        {
            m_turnTimer -= Time.deltaTime;

            if(m_turnTimer <= 0)
            {
                m_canMove = true;
                m_canFlip = true;
            }
        }

        if(m_checkJumpMultiplier && !m_gameInput.Player.Jump.WasCompletedThisFrame())
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocity.x, m_rigidbody.linearVelocityY * m_variableJumpHeightMultiplier);
            m_checkJumpMultiplier = false;
        }

        if(m_gameInput.Player.Dash.WasPressedThisFrame())
        {
            if(Time.time >= (m_lastDash + m_dashCooldown))
            {
                AttemptToDash();
            }
        }
    }

    private void AttemptToDash()
    {
        m_isDashing = true;
        m_dashTimeLeft = m_dashTime;
        m_lastDash = Time.time;

        Pool.Instance.Get();
        m_lastImageXPosition = transform.position.x;
    }

    private void CheckDash()
    {
        if(m_isDashing)
        {
            if(m_dashTimeLeft > 0)
            {
                m_canMove = false;
                m_canFlip = false;
                m_rigidbody.linearVelocity = new Vector2(m_dashSpeed * m_facingDirection, m_rigidbody.linearVelocityY);
                m_dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - m_lastImageXPosition) > m_distanceBetweenImages)
                {
                    Pool.Instance.Get();
                    m_lastImageXPosition = transform.position.x;
                }
            }
            
            if(m_dashTimeLeft <= 0 || m_isTouchingWall)
            {
                m_isDashing = false;
                m_canMove = true;   
                m_canFlip = true;
            }
        }
    }

    private void ApplyMovement()
    {
        if (!m_isGrounded && !m_isWallSliding && m_movementInputDirection.x == 0)
        {
            m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX * m_airDragMultiplier, m_rigidbody.linearVelocityY);
        }
        else if(m_canMove)
        {
            m_rigidbody.linearVelocity = new Vector2(m_movementSpeed * m_movementInputDirection.x, m_rigidbody.linearVelocityY);
        }

        if (m_isWallSliding)
        {
            if (m_rigidbody.linearVelocityY < -m_wallSlideSpeed)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, -m_wallSlideSpeed);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(m_groundCheck.position, m_grouncCheckRadius);
        Gizmos.DrawLine(m_wallCheck.position, new Vector3(m_wallCheck.position.x + m_wallCheckedDistance, 
            m_wallCheck.position.y, m_wallCheck.position.z));
    }
}