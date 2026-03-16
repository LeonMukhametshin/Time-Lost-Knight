using Game.Core;
using Game.Player;
using System.Collections;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Teleport
{
    [RequireComponent(typeof(Collider2D))]
    [MovedFrom("")]
    public class DoorTeleporterCore : MonoBehaviour
    {
        [Header("Assign")]
        [SerializeField] private DoorTeleporterCore m_targetDoor;
        [SerializeField] private Transform m_spawnPoint;
        [SerializeField] private TeleportPositionCalculator m_positionCalculator;
        [SerializeField] private TeleportNotifier m_notifier;

        private bool m_canTeleport = true;
        [SerializeField][Min(0f)] private float m_teleportCooldown = 0.18f;

        private TeleportMover m_mover;

        private void Awake()
        {
            var col = GetComponent<Collider2D>();
            m_mover ??= new TeleportMover();
            col.isTrigger = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!m_canTeleport) return;
            if (!other.CompareTag(Tags.Player)) return;

            StartCoroutine(TeleportCoroutine(other));
        }

        private IEnumerator TeleportCoroutine(Collider2D teleporteeCollider)
        {
            LockBothDoors(m_teleportCooldown);

            Vector2 newTransformPos =
                m_positionCalculator.CalculateNewPosition(
                    teleporteeCollider,
                    m_targetDoor.m_spawnPoint
                );

            m_mover.Move(teleporteeCollider, newTransformPos);
            m_notifier.Notify(teleporteeCollider.gameObject, newTransformPos);

            yield return new WaitForSeconds(m_teleportCooldown);
        }

        private void LockBothDoors(float seconds)
        {
            m_targetDoor.Lock(seconds);
            Lock(seconds);
        }

        public void Lock(float seconds)
        {
            if (!m_canTeleport) return;
            StartCoroutine(LockCoroutine(seconds));
        }

        private IEnumerator LockCoroutine(float seconds)
        {
            m_canTeleport = false;
            yield return new WaitForSeconds(seconds);
            m_canTeleport = true;
        }

        public void AddTeleportedListener(System.Action<GameObject, Vector2> listener)
        {
            if (m_notifier != null)
                m_notifier.Teleported += listener;
        }

        public void RemoveTeleportedListener(System.Action<GameObject, Vector2> listener)
        {
            if (m_notifier != null)
                m_notifier.Teleported -= listener;
        }

        private void OnDrawGizmosSelected()
        {
            if (m_spawnPoint == null) return;
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(m_spawnPoint.position, 0.05f);
        }
    }
}
