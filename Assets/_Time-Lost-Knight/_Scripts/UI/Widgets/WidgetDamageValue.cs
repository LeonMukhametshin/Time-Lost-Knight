using TMPro;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.UI.Widgets
{
    [MovedFrom("")]
    public class WidgetDamageValue : MonoBehaviour
    {
        [SerializeField] private TextMeshPro m_textValue;
        [SerializeField] private float m_destroyTime = 2f;

        private void OnEnable()
        {
            Destroy(gameObject, m_destroyTime);
        }

        public void SetValue(string value)
        {
            m_textValue.text = value;
        }

        public void SetColor(Color color)
        {
            m_textValue.color = color;
        }
    }
}
