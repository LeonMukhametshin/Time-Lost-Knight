using Game.Observer;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Teleport
{
    [Serializable]
    [MovedFrom("")]
    public class TeleportPositionCalculator
    {
        public Vector2 CalculateNewPosition(Collider2D subject, Transform targetSpawn)
        {
            var bounds = subject.bounds;
            Vector2 currentBottomCenter =
                new Vector2(bounds.min.x + bounds.size.x * 0.5f, bounds.min.y);

            Vector2 delta = currentBottomCenter - (Vector2)subject.transform.position;
            Vector2 safeBottomCenter = targetSpawn.position;

            return safeBottomCenter - delta;
        }
    }
}
