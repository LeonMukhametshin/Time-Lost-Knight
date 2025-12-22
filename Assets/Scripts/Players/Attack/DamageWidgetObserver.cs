using UnityEngine;

public class DamageWidgetObserver : MonoBehaviour
{
    [SerializeField] private GameObject m_floatingText;
    [SerializeField] private Transform m_damageWidgetContainer;
    [SerializeField] private DamageableWall m_canBeDamageable;

    public void CreateWidgetDamageValue(int damage)
    {
        var go = Instantiate(m_floatingText, m_damageWidgetContainer);
        var text  = go.GetComponent<WidgetDamageValue>();
        text.SetValue(damage.ToString());
    }

    private void OnEnable()
    {
        m_canBeDamageable.Damaged += CreateWidgetDamageValue;
    }

    private void OnDisable()
    {
        m_canBeDamageable.Damaged -= CreateWidgetDamageValue;
    }
}