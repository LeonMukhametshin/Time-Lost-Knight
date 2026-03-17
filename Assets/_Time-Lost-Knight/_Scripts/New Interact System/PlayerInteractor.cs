using Game.Interaction.NewSystem.Interfaces;
using Game.Player.Input;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.NewSystem
{
    [MovedFrom("")]
    public class PlayerInteractor : MonoBehaviour
    {
        [SerializeField] private float m_radius = 2f;
        [SerializeField] private LayerMask m_interactableLayers;

        private InteractPrompt m_prompt;

        private Collider2D[] m_buffer = new Collider2D[5];
        private IInteract focused;

        private PlayerInputHandler m_inputHandler;

        private bool m_isInitialize;

        public void Initialize(PlayerInputHandler inputHandler, InteractPrompt interactPrompt)
        {
            if(m_isInitialize)
            {
                return;
            }

            m_inputHandler = inputHandler;
            m_prompt = interactPrompt;

            m_isInitialize = true;
        }

        private void Update()
        {
            if(!m_isInitialize)
            {
                return;
            }

            IInteract nearest = FindNearestInteractable();
            UpdateFocus(nearest);

            if(focused is not null
                && m_inputHandler.interactInput
                && focused.CanInteract())
            {
                focused.Interact();
            }
        }


        private IInteract FindNearestInteractable()
        {
            m_buffer = Physics2D.OverlapCircleAll(transform.position, m_radius, m_interactableLayers);
            int count = m_buffer.Length;
            IInteract nearest = null;
            float bestDistanceSqrt = float.MaxValue;

            for(int i = 0; i < count; i++)
            {
                Collider2D collider = m_buffer[i];
                if(collider is null)
                {
                    continue;
                }

                IInteract interactable = collider.GetComponentInParent<IInteract>();
                if (interactable is null || !interactable.CanInteract())
                {
                    continue;
                }

                float distSqrt = (collider.transform.position - transform.position).sqrMagnitude;
                if(distSqrt < bestDistanceSqrt)
                {
                    bestDistanceSqrt = distSqrt;
                    nearest = interactable;
                }
            }

            return nearest;
        }

        private void UpdateFocus(IInteract nearest)
        {
            if(ReferenceEquals(focused, nearest))
            {
                return;
            }
            focused?.OnFocusLost();
            focused = nearest;
            if(focused != null)
            {
                focused.OnFocusGained();
                m_prompt.Show(focused);
            }
            else
            {
                m_prompt.Hide();
            }
        }
    }
}

