using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public GameObject StartParticles(GameObject particlePrefab, Vector2 position, Quaternion rotation) => 
        Instantiate(particlePrefab, position, rotation, null);

    public GameObject StartParticles(GameObject particlePrefab) => 
        StartParticles(particlePrefab, transform.position, Quaternion.identity);

    public GameObject StartParticlesWithRandomRotation(GameObject particlePrefab, Vector2 position)
    {
        var random = Quaternion.Euler(0f, 0f, Random.Range(0, 360));
        return Instantiate(particlePrefab, position, random);
    }

    public void StartParticles(GameObject[] particlePrefabs, Vector2 position, Quaternion rotation)
    {
        foreach(var particle in particlePrefabs)
        {
            StartParticles(particle, position, rotation);
        }
    }
}