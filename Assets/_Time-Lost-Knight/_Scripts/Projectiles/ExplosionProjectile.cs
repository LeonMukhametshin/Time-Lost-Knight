using System.Collections.Generic;
using UnityEngine;

public class ExplosionProjectile : BaseProjectile
{
    [Header("Explosion")]
    [SerializeField] private float m_explosionRadius = 2f;
    [SerializeField] private GameObject[] m_explosionVfx;

    public override void SelectDetectedEntities(Collider2D other)
    {
        var colliders
            = Physics2D.OverlapCircleAll(transform.position, m_explosionRadius);

        List<IEffectable> effectablesObject = new();

        foreach(var collider in colliders)
        {
            if(collider.gameObject.TryGetComponent(out IEffectable effectable))
            {
                effectablesObject.Add(effectable);
            }
        }

        int layer = other.gameObject.layer;
        if (IsInLayerMask(layer, groundLayer))
        {
            OnHitGround();
        }

        OnHit(effectablesObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, m_explosionRadius);
    }
}