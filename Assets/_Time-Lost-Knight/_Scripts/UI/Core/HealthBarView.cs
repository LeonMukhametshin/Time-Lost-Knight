using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private Image m_bar;
    [SerializeField] protected HealthSystem m_healthSystem;

    public virtual void OnEnable() => 
        Subscribe();

    public virtual void OnDisable() =>
        UnSubscribe();

    protected void Subscribe()
    {
        m_healthSystem.valueChanged += SetValue;
        SetValue();
    }

    protected void UnSubscribe()
    {
        m_healthSystem.valueChanged -= SetValue;
    }

    private void SetValue() =>
        m_bar.fillAmount = m_healthSystem.value / m_healthSystem.maxValue;
}