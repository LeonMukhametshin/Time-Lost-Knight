using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image m_bar;

    [SerializeField] private HealthComponent m_health;

    public void OnEnable()
    {
        m_health.valueChanged += SetValue;
        SetValue();
    }

    public void OnDisable() => 
        m_health.valueChanged -= SetValue;

    private void SetValue() =>
        m_bar.fillAmount = m_health.value / m_health.maxValue;
}