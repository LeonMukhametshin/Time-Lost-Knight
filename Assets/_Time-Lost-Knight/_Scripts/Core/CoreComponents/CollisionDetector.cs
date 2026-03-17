using Game.Interaction.Objects.Platform;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.CoreComponents
{
    [MovedFrom("")]
    public abstract class CollisionDetector : CoreComponent
    {
        private FlipContoller m_flipContoller;
        protected FlipContoller flipController
        {
            get => m_flipContoller ??= core.GetCoreComponent<FlipContoller>();
        }

        private Movement m_movement;
        protected Movement movement
        {
            get => m_movement ??= core.GetCoreComponent<Movement>();
        }

        [SerializeField] protected Transform m_groundCheck;
        [SerializeField] protected Transform m_wallCheck;
        [SerializeField] protected Transform m_ledgeCheck;

        [SerializeField][Min(0)] protected float m_groundCheckRadius;
        [SerializeField][Min(0)] protected float m_wallCheckDistance;

        [SerializeField] protected LayerMask m_groundLayer;
        [SerializeField] protected LayerMask m_platform;

        protected Vector2 m_workspace;

        public bool CheckGrounded()
        {
            CheckTouchinMovingPlatform();
            return Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_groundLayer) ||
            Physics2D.OverlapCircle(m_groundCheck.position, m_groundCheckRadius, m_platform);
        }


        public bool CheckWallTouch() =>
            Physics2D.Raycast(m_wallCheck.position, Vector2.right * flipController.facingDirection,
                m_wallCheckDistance, m_groundLayer);


        private void CheckTouchinMovingPlatform()
        {
            var colliders = Physics2D.OverlapCircleAll(m_groundCheck.position, m_groundCheckRadius, m_groundLayer);
            if(colliders.Length > 0)
            {
                foreach(var collider in colliders)
                {
                    if(collider.tag == "MovingPlatform")
                    {
                        transform.parent = collider.transform;
                    }
                }
            }
            else
            {
                transform.parent = null;
            }
        }
    }
}
