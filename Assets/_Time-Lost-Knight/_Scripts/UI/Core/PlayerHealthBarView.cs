public class PlayerHealthBarView : HealthBarView
{
    public void Initialized()
    {
        m_healthSystem = ServiceLocator.Get<Player>().healthSystem;

        Subscribe();
    }

    public new virtual void OnEnable() { }  
    public new virtual void OnDisable() { }
}