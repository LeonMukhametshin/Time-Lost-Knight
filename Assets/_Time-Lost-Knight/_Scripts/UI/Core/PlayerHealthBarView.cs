using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBarView : MonoBehaviour
{
    [SerializeField] private Image m_bar;
    
    private IHealth m_healthSystem;

    public void Initialize(IHealth healthSystem)
    {
        m_healthSystem = healthSystem;
        m_healthSystem.valueChanged += SetValue;
        SetValue();
    }

    public void Unsubscribe()
    {
        m_healthSystem.valueChanged -= SetValue;
    }

    private void SetValue() =>
        m_bar.fillAmount = m_healthSystem.value / m_healthSystem.maxValue;
}