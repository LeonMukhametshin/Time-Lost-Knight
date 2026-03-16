using Game.Core.ServiceLocatorSpace;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Core.CoreComponents
{
    [MovedFrom("")]
    public class Death : CoreComponent
    {
        private HealthComponent m_healthComponent;
        protected HealthComponent healthComponent =>
            m_healthComponent ??= core.GetCoreComponent<HealthComponent>();

        [SerializeField] private GameObject[] deathParticles;

       private void OnEnable() =>
            healthComponent.died += Die;

        private void OnDisable() =>
            healthComponent.died -= Die;

        private void Die()
        {
            ServiceLocator
                .Get<ParticleManager>()
                .StartParticles(deathParticles, transform.position, Quaternion.identity);

            this.gameObject.SetActive(false);
        }
    }
}
