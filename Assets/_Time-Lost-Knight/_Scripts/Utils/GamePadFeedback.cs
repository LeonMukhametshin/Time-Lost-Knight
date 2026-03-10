using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using XInputDotNetPure;

public class GamePadFeedback : MonoBehaviour
{
    [SerializeField] private HealthComponent m_healSystem;
    
    [SerializeField][Min(0)] private float m_duration;

    [SerializeField][Min(0)] private float m_leftMotor;
    [SerializeField][Min(0)] private float m_rightMotor;
    
    private PlayerIndex m_playerIndex;

    private Coroutine m_сoroutine;

    private void OnEnable()
    {
        m_healSystem.valueChanged += StartVibration;
        m_healSystem.died += StopVibration;
    }

    private void OnDisable()
    {
        m_healSystem.valueChanged -= StartVibration;
        m_healSystem.died -= StopVibration;
        StopCoroutine(VibrationRoutine());
    }

    private void StopVibration()
    {
        if (m_сoroutine is not null)
        {
            StopCoroutine(m_сoroutine);
            m_сoroutine = null;
        }

        GamePad.SetVibration(m_playerIndex, 0f, 0f);
    }

    private void StartVibration()
    {
        if (m_сoroutine is not null)
        {
            StopCoroutine(m_сoroutine);
        }

        m_сoroutine = StartCoroutine(VibrationRoutine());
    }

    private IEnumerator VibrationRoutine()
    {
        GamePad.SetVibration(m_playerIndex, m_leftMotor, m_rightMotor);

        yield return new WaitForSeconds(m_duration);

        GamePad.SetVibration(m_playerIndex, 0f, 0f);
        m_сoroutine = null;
    }
}