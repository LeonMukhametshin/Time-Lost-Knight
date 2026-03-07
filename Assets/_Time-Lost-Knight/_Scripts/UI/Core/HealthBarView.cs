using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] private GameObject m_container;
    [SerializeField] private Image m_bar;
    [SerializeField] private HealthComponent m_health;

    public void OnEnable()
    {
        m_health.valueChanged += SetValue;
        SetValue();
        m_container.SetActive(false);  
    }

    public void OnDisable() => 
        m_health.valueChanged -= SetValue;

    private void SetValue()
    {
        if(!m_container.activeSelf)
        {
            m_container.SetActive(true);
        }

        m_bar.fillAmount = m_health.value / m_health.maxValue;
    }
}