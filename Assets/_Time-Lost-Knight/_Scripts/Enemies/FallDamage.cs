using UnityEngine;

public class FallDamage : MonoBehaviour
{
    [SerializeField] private Rigidbody2D m_entitnyRigidbody;
    [SerializeField] private HealthSystem m_healthSystem;

    [SerializeField][Min(0)] private float m_timeToDamage;
    [SerializeField][Min(0)] private float m_fallVelocity = 2f;

    private bool m_hasFallen;
    private float m_fallTime = 0;

    private void Update()
    {
        if (m_entitnyRigidbody.linearVelocityY < -m_fallVelocity)
        {
            m_fallTime += Time.deltaTime;
            m_hasFallen = true;
        }
        else if(m_hasFallen)
        {
            m_healthSystem.TakeDamage(CalculateFallDamage());
            Reset();
        }
    }

    private float CalculateFallDamage() => 
        m_fallTime * m_timeToDamage;

    private void Reset()
    {
        m_hasFallen = false;
        m_fallTime = 0f;
    }
}