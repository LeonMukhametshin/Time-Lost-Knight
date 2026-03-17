using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Level.Teleport
{
    [MovedFrom("")]
    public interface ITeleportable
    {
        void OnTeleported(Vector2 newPosition);
    }
}
