using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject entityGameObject;
    [SerializeField] private GameObject[] deathParticles;

    [SerializeField] private HealthSystem m_healthSystem;

    private ParticleManager m_particleManager;

    private ParticleManager particleManager
    {
        get => m_particleManager ??= core.GetCoreComponent<ParticleManager>();  
    }

    private void OnEnable()
    {
        m_healthSystem.died += Die;
    }

    private void OnDisable()
    {
        m_healthSystem.died -= Die;
    }

    private void Die()
    {
        foreach (var particle in deathParticles)
        {
            particleManager.StartParticles(particle);
        }

        entityGameObject.SetActive(false);  
    }
}