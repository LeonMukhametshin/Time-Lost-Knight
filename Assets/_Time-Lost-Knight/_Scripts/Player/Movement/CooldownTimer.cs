using UnityEngine;
using System.Collections;

public sealed class CooldownTimer
{
    private readonly float m_duration;
    private readonly CoroutineRunner m_coroutineRunner;
    private Coroutine m_activeCoroutine;
    private bool m_isReady = true;

    public CooldownTimer(float duration, CoroutineRunner coroutineRunner)
    {
        m_duration = duration;
        m_coroutineRunner = coroutineRunner;
    }

    public bool IsReady => m_isReady;

    public void Trigger()
    {
        if (m_duration <= 0f)
        {
            m_isReady = true;
            return;
        }

        m_isReady = false;
        RestartCoroutine();
    }

    public void Reset()
    {
        m_isReady = true;
        StopCoroutine();
    }

    private void RestartCoroutine()
    {
        StopCoroutine();
        m_activeCoroutine = m_coroutineRunner.StartCoroutine(CooldownRoutine());
    }

    private void StopCoroutine()
    {
        if (m_activeCoroutine == null)
        {
            return;
        }

        m_coroutineRunner.StopCoroutine(m_activeCoroutine);
        m_activeCoroutine = null;
    }

    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(m_duration);
        m_isReady = true;
        m_activeCoroutine = null;
    }
}