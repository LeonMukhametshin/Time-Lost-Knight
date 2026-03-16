using Game.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.Objects
{
    [RequireComponent(typeof(Collider2D))]
    [MovedFrom("")]
    public class DoorTeleporter : MonoBehaviour
    {
        [Header("Assign")]
        [SerializeField] private DoorTeleporter m_targetDoor;
        [SerializeField] private Transform m_spawnPoint;

        private bool m_canTeleport = true;
        private Collider2D m_collider;
        private float m_teleportCooldown = 0.18f;

        private void Awake()
        {
            m_collider = GetComponent<Collider2D>();
            if (m_collider != null)
                m_collider.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_canTeleport) return;
            if (m_targetDoor == null || m_spawnPoint == null) return;
            if (!other.CompareTag("Player")) return;

            StartCoroutine(TeleportCoroutine(other));
        }

        private IEnumerator TeleportCoroutine(Collider2D playerCollider)
        {
            m_canTeleport = false;
            if (m_targetDoor != null) m_targetDoor.m_canTeleport = false;

            Bounds bounds = playerCollider.bounds;
            Vector2 currentBottomCenter = new Vector2(bounds.min.x + bounds.size.x * 0.5f, bounds.min.y);
            Vector2 deltaFromTransformToBottom = currentBottomCenter - (Vector2)playerCollider.transform.position;

            Vector2 safeBottomCenter = (Vector2)m_targetDoor.m_spawnPoint.position;
            Vector2 newTransformPos = safeBottomCenter - deltaFromTransformToBottom;

            Rigidbody2D rb = playerCollider.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.position = newTransformPos;
            }
            else
            {
                playerCollider.transform.position = newTransformPos;
            }

            yield return new WaitForSeconds(m_teleportCooldown);

            m_canTeleport = true;
            if (m_targetDoor != null) m_targetDoor.m_canTeleport = true;
        }

        private void OnDrawGizmosSelected()
        {
            if (m_spawnPoint == null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(m_spawnPoint.position, 0.05f);
        }
    }
}
