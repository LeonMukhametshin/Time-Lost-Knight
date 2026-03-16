using Game.Buffs.Interfaces;
using Game.Core.CoreComponents;
using Game.Core.ServiceLocatorSpace;
using Game.UI;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Traps
{
    [MovedFrom("")]
    public class TimedSpikes : Trap
    {
        [SerializeReferenceDropdown]
        [SerializeReference] private IEffect[] m_effects;

        [SerializeField][Range(0, 10)] private float duration;

        private float m_timer;

        private void Update()
        {
            if (ServiceLocator.Get<Pause>().isPaused)
            {
                return;
            }

            if(Time.time >= m_timer + duration)
            {
                Activate();

                m_timer = Time.time;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            ApplyEffects(collision);
        }

        protected override void ApplyEffects(Collider2D collision)
        {
            if (collision.gameObject.TryGetComponent<CoreSystem>(out var core))
            {
                m_effects.ApplyEffect(core.effectables);
            }
        }
    }
}
