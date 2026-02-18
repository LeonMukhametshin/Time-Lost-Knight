using UnityEngine;

public class ParticleManager : CoreComponent
{
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation)
    {
        return Instantiate(particlePrefab, position, rotation, null);
    }

    public GameObject StartParticles(GameObject particlePrefab)
    {
        return StartParticles(particlePrefab, transform.position, Quaternion.identity);
    }

    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab)
    {
        var random = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
        return Instantiate(particlePrefab, transform.position, random);
    }
}