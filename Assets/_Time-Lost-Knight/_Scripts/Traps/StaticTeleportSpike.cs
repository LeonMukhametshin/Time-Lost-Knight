using UnityEngine;

public class StaticTeleportSpike : StaticTrap
{
    [SerializeField] private bool m_teleportPlayerAfterDamage;
    [SerializeField] private bool m_useRuntimeTeleportPoint;

    [SerializeField] private Transform m_teleportPoint;
    [SerializeField] private Vector2 m_runtimeTeleportPoint;
    
    private TeleportMover m_teleportMover;

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyEffects(collision);
    }

    protected override void ApplyEffects(Collider2D collision)
    {
        if (!collision.gameObject.TryGetComponent<Core>(out var core))
        {
            return;
        }

        effects.ApplyEffect(core.effectables);

        if (!m_teleportPlayerAfterDamage || !collision.CompareTag(Tags.Player))
        {
            return;
        }

        var targetPoint = GetTeleportPoint();
        if (targetPoint == null)
        {
            return;
        }

        m_teleportMover ??= new TeleportMover();
        m_teleportMover.Move(collision, targetPoint.Value);
    }

    public void SetTeleportPoint(Vector2 teleportPoint)
    {
        m_runtimeTeleportPoint = teleportPoint;
        m_useRuntimeTeleportPoint = true;
    }

    private Vector2? GetTeleportPoint()
    {
        if (m_useRuntimeTeleportPoint)
        {
            return m_runtimeTeleportPoint;
        }

        if (m_teleportPoint != null)
        {
            return m_teleportPoint.position;
        }

        return null;
    }
}