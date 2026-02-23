using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image m_bar;
    [SerializeField] private HealthSystem m_healthSystem;

    private void OnEnable()
    { 
        m_healthSystem.valueChanged += SetValue;
        SetValue();
    }

    private void OnDisable() => 
        m_healthSystem.valueChanged -= SetValue;

    private void SetValue() =>
        m_bar.fillAmount = m_healthSystem.value / m_healthSystem.maxValue;
}