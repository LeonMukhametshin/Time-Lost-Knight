using UnityEngine;

public class Death : CoreComponent
{
    private HealthComponent m_healthComponent;
    protected HealthComponent healthComponent =>
        m_healthComponent ??= core.GetCoreComponent<HealthComponent>();

    [SerializeField] private GameObject entityGameObject;
    [SerializeField] private GameObject[] deathParticles;

    private ParticleManager m_particleManager;

    private ParticleManager particleManager => 
        m_particleManager ??= core.GetCoreComponent<ParticleManager>();  
    private void OnEnable() =>
        healthComponent.died += Die;

    private void OnDisable() =>
        healthComponent.died -= Die;

    private void Die()
    {
        foreach (var particle in deathParticles)
        {
            particleManager.StartParticles(particle);
        }

        //TODO: remove 
        entityGameObject.SetActive(false);  
    }
}