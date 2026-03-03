using UnityEngine;

public class ExplosionProjectile : BaseProjectile
{
    [Header("Explosion")]
    [SerializeField] private float m_explosionRadius = 2f;
    [SerializeField] private GameObject[] m_explosionVfx;

  
}