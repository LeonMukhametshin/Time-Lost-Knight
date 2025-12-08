using UnityEngine;

namespace Players
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private PlayerData m_data;
        [SerializeField] private Rigidbody2D m_rigidbody;
        [SerializeField] private Transform m_groundCheckPosition;
        [SerializeField] private Animator m_animator;

        private PlayerInput m_playerInput;
        private GroundCheck m_groundCheck;

        private PlayerJumpSystem m_jumpSystem;
        private PlayerMovementSystem m_movementSystem;

        private void Awake()
        {
            var systems = PlayerSystemsFactory.CreateAllSystems(
                m_data, m_rigidbody, transform, m_groundCheckPosition
                );

            m_playerInput = systems.input;
            m_groundCheck = systems.groundCheck;
            m_jumpSystem = systems.jumpSystem;
            m_movementSystem = systems.movementSystem;
        }

        private void OnEnable()
        {
            m_playerInput?.Enable();
        }

        private void OnDisable()
        {
            m_playerInput.Disable();
        }

        private void Update()
        {
            m_groundCheck.Update();

            m_jumpSystem.Update(Time.deltaTime);

            if (m_playerInput.JumpPressedThisFrame)
            {
                m_jumpSystem.StartJump();
            }

            if (m_playerInput.JumpReleasedThisFrame)
            {
                m_jumpSystem.StopJump();
            }

            m_animator.SetBool("Walking", Mathf.Abs(m_rigidbody.linearVelocityX) > 0.1f && m_groundCheck.IsGrounded);
           
        }

        private void LateUpdate()
        {
            m_playerInput.Update();
        }

        private void FixedUpdate()
        {
            m_movementSystem.Update(m_playerInput.MoveInput.x);
        }
    }
}