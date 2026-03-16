using Game.Observer;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Teleport
{
    [MovedFrom("")]
    public class TeleportMover
    {
        public void Move(Collider2D subject, Vector2 position)
        {
            var rb = subject.attachedRigidbody;
            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.position = position;
            }
            else
            {
                subject.transform.position = position;
            }
        }
    }
}
