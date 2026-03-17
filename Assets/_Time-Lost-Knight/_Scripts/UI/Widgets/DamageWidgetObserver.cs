using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.UI.Widgets
{
    [MovedFrom("")]
    public class DamageWidgetObserver : MonoBehaviour
    {
        [SerializeField] private GameObject m_floatingText;
        [SerializeField] private Transform m_damageWidgetContainer;

        public void CreateWidgetDamageValue(float damage)
        {
            var go = Instantiate(m_floatingText, m_damageWidgetContainer);
            var text  = go.GetComponent<WidgetDamageValue>();
            text.SetValue(damage.ToString());
        }
    }
}
