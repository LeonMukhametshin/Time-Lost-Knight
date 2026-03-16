using Game.Camera;
using Game.Core.CoreComponents;
using Game.Player;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Camera.CoreSystem
{
    [MovedFrom("")]
    public sealed class CameraController : MonoBehaviour
    {
        public event Action<CameraMode> ModeChanged;

        [SerializeField] private PlayerController m_player;
        [SerializeField][Min(0f)] private float m_recenterDelay = 0.6f;
        [SerializeField][Min(0f)] private float m_sideDelay = 0.15f;

        private CameraContext m_context;
        private CameraStateResolver m_resolver;
        private CameraMode m_currentMode;
        private Vector3 m_lastPlayerPosition;
        private FlipContoller m_flipController;

        private bool m_hasPendingMode;
        private CameraMode m_pendingMode;
        private float m_pendingTimer;

        private void Awake()
        {
            m_context = new CameraContext();
            m_resolver = new CameraStateResolver(m_recenterDelay);
            ResolvePlayer();
        }

        private void OnEnable()
        {
            if (!TryResolvePlayer()) return;

            m_lastPlayerPosition = m_player.transform.position;
            m_context.facing = ResolveFacingDirection();
        }

        private void Update()
        {
            if (!TryResolvePlayer()) return;

            Vector3 currentPosition = m_player.transform.position;
            bool isMoving = (currentPosition - m_lastPlayerPosition).sqrMagnitude > 0.0001f;
            m_context.isMoving = isMoving;
            m_context.facing = ResolveFacingDirection();

            if (isMoving)
                m_context.timeSinceLastMove = 0f;
            else
                m_context.timeSinceLastMove += Time.deltaTime;

            m_lastPlayerPosition = currentPosition;

            CameraMode desiredMode = m_resolver.Resolve(m_context);

            bool isSideMode = desiredMode == CameraMode.LeftThird || desiredMode == CameraMode.RightThird;

            if (isSideMode && desiredMode != m_currentMode)
            {
                if (!m_hasPendingMode || m_pendingMode != desiredMode)
                {
                    m_hasPendingMode = true;
                    m_pendingMode = desiredMode;
                    m_pendingTimer = 0f;
                }
                else
                {
                    m_pendingTimer += Time.deltaTime;
                    if (m_pendingTimer >= m_sideDelay)
                    {
                        ApplyMode(m_pendingMode);
                        m_hasPendingMode = false;
                        m_pendingTimer = 0f;
                    }
                }
            }
            else
            {
                if (m_hasPendingMode)
                {
                    m_hasPendingMode = false;
                    m_pendingTimer = 0f;
                }

                if (desiredMode != m_currentMode)
                    ApplyMode(desiredMode);
            }
        }

        private void ApplyMode(CameraMode mode)
        {
            m_currentMode = mode;
            ModeChanged?.Invoke(m_currentMode);
        }

        private void ResolvePlayer() =>
            m_player ??= FindAnyObjectByType<PlayerController>();

        private bool TryResolvePlayer()
        {
            ResolvePlayer();
            if (m_player == null)
                return false;

            if (m_flipController == null && m_player.core != null)
            {
                try
                {
                    m_flipController = m_player.core.GetCoreComponent<FlipContoller>();
                }
                catch
                {
                    m_flipController = null;
                }
            }

            return true;
        }

        private int ResolveFacingDirection()
        {
            if (m_flipController != null)
                return m_flipController.facingDirection;

            return m_player.transform.right.x >= 0f ? 1 : -1;
        }
    }
}
