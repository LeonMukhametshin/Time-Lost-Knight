using System.Collections;
using UnityEngine;
using XInputDotNetPure;

public class VibrationTest : MonoBehaviour
{
    [SerializeField] private HealthComponent m_healSystem;
    
    [SerializeField][Min(0)] private float m_duration;
    [SerializeField][Min(0)] private float m_leftMotor;
    [SerializeField][Min(0)] private float m_rightMotor;

    PlayerIndex playerIndex;
    GamePadState state;
    GamePadState prevState;

    private void OnEnable() => 
        m_healSystem.valueChanged += StartVibration;

    private void OnDisable() => 
        m_healSystem.valueChanged -= StartVibration;

    private void OnDestroy() => 
        GamePad.SetVibration(playerIndex, 0f, 0f);

    private void StartVibration() =>
        StartCoroutine(Vibration());

    private IEnumerator Vibration()
    {
        GamePad.SetVibration(playerIndex, m_leftMotor, m_rightMotor);
        yield return new WaitForSeconds(m_duration);
        GamePad.SetVibration(playerIndex, 0f, 0f);
    }
}