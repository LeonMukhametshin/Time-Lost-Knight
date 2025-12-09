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
    [field: SerializeField] public PlayerStateList m_playerStateList {get; set;}
    
    
    [Header("Dash Settings")]
    
    [SerializeField] private float m_dashSpeed;
    [SerializeField] private float m_dashTime;
    [SerializeField] private float m_dashCooldown;
    [SerializeField] private GameObject m_dashEffect;

    private float xAxis;
    private float yAxis;
    private float m_coyoteTimeCounter = 0;
    [SerializeField] float m_coyoteTime;

    private float m_gravity;
    private bool m_canDash = true;
    private bool m_dashed;

    [Header("Attacking")]
    private bool m_attack = false;
    [SerializeField] private float m_timeBetweenAttack;
    [SerializeField] private float m_timeSinceAttack;
    [SerializeField] private Transform m_sideAttackTransform;
    [SerializeField] private Transform m_upAttackTransform;
    [SerializeField] private Transform m_downAttackTransform;
    [SerializeField] private Vector2 m_sideAttackArea;
    [SerializeField] private Vector2 m_downAttackArea;
    [SerializeField] private Vector2 m_upAttackArea;
    [SerializeField] private LayerMask m_attackableLayer;
    [SerializeField] private float m_damage;
    [SerializeField] private GameObject m_slashEffect;

    private GameObject m_createdSlashEffect;

    [Header("Recoil")]
    [SerializeField] private int m_recoilXSteps = 5;
    [SerializeField] private int m_recoilYSteps = 5;
    [SerializeField] private int m_recoilXSpeed = 100;
    [SerializeField] private int m_recoilYSpeed = 100;
    private int m_stepsXRecoiled;
    private int m_stepsYRecoiled;

    [Header("Health Settings")]
    [SerializeField] private float m_health;
    [SerializeField] private float m_maxHealth;
    [SerializeField] [Range(0,5)] private float m_invincibleDuration = 2f;

    private bool m_restoreTime;
    private float m_restoreTimeSpeed;

    [SerializeField] private float m_hitFlashSpeed;
    [SerializeField] private SpriteRenderer m_spriteRenderer;

    public float Health
    {
        get => m_health;
        set
        {
            if(m_health != value)
            {
                m_health = Mathf.Clamp(value, 0, m_health);
            }
        }
    }

    private void Start()
    {
        m_gravity = m_rigidbody.gravityScale;
        Health = m_maxHealth;
    }   

    private void Update()
    {
        GetInput();
        UpdateJumpVariables();
        if(m_playerStateList.Dashing) return;
        Flip();
        StartDash();
        Attack();
        RestoreTimeScale();
        FlashWhileInvinvible();
    }

    private void FixedUpdate()
    {
        if(m_playerStateList.Dashing) return;
        Move();   
        Jump();
        Recoil();
    }

    private void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        yAxis =  Input.GetAxisRaw("Vertical");
        m_attack = Input.GetButtonDown("Attack");
    }

    private void Flip()
    {
        if(xAxis < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
            m_playerStateList.LookingRight = false;
        }
        else if(xAxis > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
            m_playerStateList.LookingRight = true;
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
    
    private void Attack()
    {
        m_timeSinceAttack += Time.deltaTime;
        if(m_attack && m_timeSinceAttack >= m_timeBetweenAttack)
        {
            m_timeSinceAttack = 0;
            m_animator.SetTrigger("Attacking");

            if(yAxis == 0 || yAxis < 0 && Grounded())
            {
                Hit(m_sideAttackTransform, m_sideAttackArea, ref m_playerStateList.RecoilingX, m_recoilXSpeed);
                Instantiate(m_slashEffect, m_sideAttackTransform);
            }
            else if(yAxis > 0)
            {
                Hit(m_upAttackTransform, m_upAttackArea,  ref m_playerStateList.RecoilingY, m_recoilYSpeed);
                SlashEffectAtAngle(m_slashEffect, 90, m_upAttackTransform);
            }
            else if(yAxis < 0 && !Grounded())
            {
                Hit(m_downAttackTransform, m_downAttackArea, ref m_playerStateList.RecoilingY, m_recoilYSpeed);
                SlashEffectAtAngle(m_slashEffect, -90, m_downAttackTransform);
            }
        }
    }

    private void Hit(Transform attackTransform, Vector2 attackArea, ref bool recoilDirection, float recoilStrength)
    {
        Collider2D[] objectsToHit = Physics2D.OverlapBoxAll(attackTransform.position, attackArea, 0, m_attackableLayer);
    
        if(objectsToHit.Length > 0)
        {
            recoilDirection = true;
        }
        for(int i = 0; i < objectsToHit.Length; i++)
        {
            if(objectsToHit[i].GetComponent<Enemy>() is not null)
            {
                objectsToHit[i].GetComponent<Enemy>().Hit(m_damage, (transform.position - objectsToHit[i].transform.position).normalized, recoilStrength);
            }
        }
    }

    private void SlashEffectAtAngle(GameObject shashEffect, int effectAngle, Transform attackTransform)
    {
        m_createdSlashEffect = Instantiate(shashEffect, attackTransform);
        m_createdSlashEffect.gameObject.SetActive(true);
        m_createdSlashEffect.transform.eulerAngles = new Vector3(0, 0, effectAngle);
        m_createdSlashEffect.transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y);
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        StartCoroutine(StopTakingDamage());
    }

    private void FlashWhileInvinvible()
    {
        m_spriteRenderer.material.color = m_playerStateList.Invincible ? Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time * m_hitFlashSpeed, 1.0f)) 
            : Color.white;
    }

    private IEnumerator StopTakingDamage()
    {
        m_playerStateList.Invincible = true;
        m_animator.SetTrigger("TakeDamage");
        yield return new WaitForSeconds(m_invincibleDuration);
        m_playerStateList.Invincible = false;
    }

    private void RestoreTimeScale()
    {
        if(m_restoreTime)
        {
            if(Time.timeScale < 1)
            {
                Time.timeScale += Time.deltaTime * m_restoreTimeSpeed;
            }
            else
            {
                Time.timeScale = 1;
                m_restoreTime = false;
            }
        }
    }

    public void HitStopTime(float newTimeScale, int restoreSpeed, float delay)
    {
        m_restoreTimeSpeed = restoreSpeed;
        Time.timeScale = newTimeScale;

        if(delay > 0)
        {
            StopCoroutine(StartTimeAgain(delay));
            StartCoroutine(StartTimeAgain(delay));
        }
        else
        {
            m_restoreTime = true;
        }
    }

    private IEnumerator StartTimeAgain(float delay)
    {
        m_restoreTime = true;
        yield return new WaitForSeconds(delay);
    }

    private void Recoil()
    {
        if(m_playerStateList.RecoilingX)
        {
            if(m_playerStateList.LookingRight)
            {
                m_rigidbody.linearVelocity = new Vector2(-m_recoilXSpeed, 0);
            }
            else
            {
                m_rigidbody.linearVelocity = new Vector2(m_recoilXSpeed, 0);
            }
        }
        if(m_playerStateList.RecoilingY)
        {
            m_rigidbody.gravityScale = 0;
            if(yAxis < 0)
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, m_recoilYSpeed);
            }
            else
            {
                m_rigidbody.linearVelocity = new Vector2(m_rigidbody.linearVelocityX, -m_recoilYSpeed);
            }
            m_airJumpCounter = 0;
        }
        else
        {
            m_rigidbody.gravityScale = m_gravity;
        }

        if(m_playerStateList.RecoilingX && m_stepsXRecoiled < m_recoilXSteps)
        {
            m_stepsXRecoiled++;
        }
        else
        {
            StopRectiolX();
        }

        if(m_playerStateList.RecoilingY && m_stepsYRecoiled < m_recoilYSteps)
        {
            m_stepsYRecoiled++;
        }
        else
        {
            StopRectiolY();
        }
        if(Grounded())
        {
            StopRectiolY();
        }
    }

    private void StopRectiolX()
    {
        m_stepsXRecoiled = 0;
        m_playerStateList.RecoilingX = false;
    }

    private void StopRectiolY()
    {
        m_stepsYRecoiled = 0;
        m_playerStateList.RecoilingY = false;
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
        Gizmos.DrawWireCube(m_sideAttackTransform.position, m_sideAttackArea);
        Gizmos.DrawWireCube(m_upAttackTransform.position, m_upAttackArea);
        Gizmos.DrawWireCube(m_downAttackTransform.position, m_downAttackArea);
    }
}