using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Projectiles
{
    [MovedFrom("")]
    public interface IProjectile
    {
        void Initialize(Vector2 targetPosition, float speed);
    }
}
