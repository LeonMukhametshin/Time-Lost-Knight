using System.Collections;
using UnityEngine;

public class DashAbility : IPlayerAbility
{
    public bool isEnabledByDefault => m_isEnabled;
    public bool isActive => m_isActive;

    private bool m_isEnabled = true;
    private bool m_isActive = false;

    private readonly PlayerDashData m_dashData;
    private Rigidbody2D m_rigidbody2D;
    private CoroutineRunner m_coroutineRunner;

    private float m_gravityScale = 0f;

    public DashAbility(PlayerDashData dashData, Rigidbody2D rigidbody2D, CoroutineRunner runner)
    {
        m_dashData = dashData;
        m_rigidbody2D = rigidbody2D;
        m_coroutineRunner = runner;

        m_gravityScale = rigidbody2D.gravityScale;
    }

    public void Activate() =>
        m_isEnabled = true;

    public void Deactivate() =>
        m_isEnabled = false;

    public void Do(AbilityContext contex)
    {
        if(!m_isEnabled)
        {
            return;
        }

        m_coroutineRunner.StartCoroutine(DashRoutine(contex.xScale));
    }

    private IEnumerator DashRoutine(int x)
    {
        m_isActive = true;
        m_rigidbody2D.gravityScale = 0f;
        float timer = 0f;

        while(timer < m_dashData.dashAttackTime)
        {
            m_rigidbody2D.linearVelocity = new Vector2(x * m_dashData.dashSpeed, 0f);

            timer += Time.deltaTime;
            yield return null;
        }

        m_rigidbody2D.gravityScale = m_gravityScale;
        m_isActive = false;
    }
}