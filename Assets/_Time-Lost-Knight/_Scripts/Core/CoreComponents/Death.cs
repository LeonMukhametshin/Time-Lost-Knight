using UnityEngine;

public class Death : CoreComponent
{
    [SerializeField] private GameObject entityGameObject;
    [SerializeField] private GameObject[] deathParticles;

    private ParticleManager m_particleManager;
    private Stats m_stats;

    private void OnEnable()
    {
        m_particleManager = core.GetCoreComponent<ParticleManager>();
        m_stats = core.GetCoreComponent<Stats>();

        m_stats.died += Die;
    }

    private void OnDisable()
    {
        m_stats.died -= Die;
    }

    public void Die()
    {
        foreach (var particle in deathParticles)
        {
            m_particleManager.StartParticles(particle);
        }

        entityGameObject.SetActive(false);  
    }
}