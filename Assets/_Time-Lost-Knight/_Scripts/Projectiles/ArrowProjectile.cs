using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Projectiles
{
    [MovedFrom("")]
    public class ArrowProjectile : BaseProjectile
    {
        protected virtual void UpdateRotation()
        {
            float angle = Mathf.Atan2(projectileRigidbody.linearVelocityY, projectileRigidbody.linearVelocityX) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
