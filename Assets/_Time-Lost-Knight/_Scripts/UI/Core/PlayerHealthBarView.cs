using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarView : MonoBehaviour
{
    [SerializeField] private Image m_bar;
    
    private HealthSystem m_healthSystem;

    public void Initialize()
    {
        m_healthSystem = ServiceLocator.Get<Player>().healthSystem;
        m_healthSystem.valueChanged += SetValue;
        SetValue();
    }

    public void OnDisable()
    {
        m_healthSystem.valueChanged -= SetValue;
    }

    private void SetValue() =>
        m_bar.fillAmount = m_healthSystem.value / m_healthSystem.maxValue;
}