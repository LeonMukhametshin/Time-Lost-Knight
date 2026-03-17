using Game.Core.ServiceLocatorSpace;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.CoreComponents
{
    [MovedFrom("")]
    public class EntityCombat : Combat
    {
        [SerializeField] private GameObject m_damageParticles;

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            ServiceLocator
                .Get<ParticleManager>()
                .StartParticlesWithRandomRotation(m_damageParticles, transform.position);
        }
    }
}
