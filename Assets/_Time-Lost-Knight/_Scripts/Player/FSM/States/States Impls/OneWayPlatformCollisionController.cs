using UnityEngine;

public class OneWayPlatformCollisionController : CoreComponent
{
    [SerializeField] private float m_duration;

    [SerializeField] private int m_playerLayer;
    [SerializeField] private int m_platformLayer;

    private float m_timer;

    private bool m_ignorePlatform = true;

    private void Update()
    {
        if(Time.time >= m_timer + m_duration && m_ignorePlatform)
        {
            ResetCollision();
        }
    }

    public void SetIgnorePlatform()
    {
        m_ignorePlatform = true;
        Physics2D.IgnoreLayerCollision(m_platformLayer, m_playerLayer, m_ignorePlatform);

        m_timer = Time.time;
    }

    private void ResetCollision()
    {
        m_ignorePlatform = false;
        Physics2D.IgnoreLayerCollision(m_platformLayer, m_playerLayer, m_ignorePlatform);
    }
}