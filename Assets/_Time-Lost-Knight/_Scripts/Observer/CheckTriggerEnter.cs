
//TODO remove
using Game.Interaction.UI;
using Game.Player;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Observer
{
    [MovedFrom("")]
    public class CheckTriggerEnter : MonoBehaviour
    {
        [SerializeField] private InteractionPromptView m_promptView;

        private void Start()
        {
            m_promptView.Hide();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                m_promptView.Show();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                m_promptView.Hide();
            }
        }
    }
}
