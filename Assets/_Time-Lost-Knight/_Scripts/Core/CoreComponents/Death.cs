using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject entityGameObject;
    [SerializeField] private GameObject[] deathParticles;

    [SerializeField] private Entity m_entity;

    private ParticleManager m_particleManager;

    private ParticleManager particleManager
    {
        get => m_particleManager ??= core.GetCoreComponent<ParticleManager>();  
    }

    private void OnEnable() => 
        m_entity.healthSystem.died += Die;

    private void OnDisable() => 
        m_entity.healthSystem.died -= Die;

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