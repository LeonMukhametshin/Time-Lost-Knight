using UnityEngine;
using System.Collections;

public class OneWayPlatformCollisionController
{
    private CoroutineRunner m_coroutineRunner;
    private int m_playerLayer;
    private int m_platformLayer;
    private float m_duration;

    private Coroutine m_ignoreRoutine;

    public OneWayPlatformCollisionController(CoroutineRunner coroutineRunner, 
        int playerLayer, int platformLayer, float duration)
    {
        m_coroutineRunner = coroutineRunner;
        m_playerLayer = playerLayer;
        m_platformLayer = platformLayer;
        m_duration = duration;
    }

    public void SetIgnorePlatform()
    {
        Physics2D.IgnoreLayerCollision(m_platformLayer, m_playerLayer, true);

        if (m_ignoreRoutine != null)
        {
            m_coroutineRunner.StopCoroutine(m_ignoreRoutine);
        }

        m_ignoreRoutine = m_coroutineRunner.StartCoroutine(IgnoreRoutine());
    }

    public void ResetCollision()
    {
        if (m_ignoreRoutine != null)
        {
            m_coroutineRunner.StopCoroutine(m_ignoreRoutine);
            m_ignoreRoutine = null;
        }

        Physics2D.IgnoreLayerCollision(m_platformLayer, m_playerLayer, false);
    }

    private IEnumerator IgnoreRoutine()
    {
        yield return new WaitForSeconds(m_duration);
        Physics2D.IgnoreLayerCollision(m_platformLayer, m_playerLayer, false);
        m_ignoreRoutine = null;
    }
}