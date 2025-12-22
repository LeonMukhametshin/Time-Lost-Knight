public sealed class PlayerHealthSystem : PlayerSystemBase
{
    private HealthSystem m_healthSystem;

    protected override void OnInitialize()
    {
        m_healthSystem = context.health;
    }
}