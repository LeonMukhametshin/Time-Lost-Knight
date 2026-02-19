using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image m_bar;
    [SerializeField] private HealthSystem m_healthComponent;

    private void OnEnable()
    {
        m_healthComponent.valueChanged += SetValue;
    }

    private void OnDisable()
    {
        m_healthComponent.valueChanged -= SetValue;
    }

    private void SetValue() =>
        m_bar.fillAmount = (float)m_healthComponent.value / (float)m_healthComponent.maxValue;
}
