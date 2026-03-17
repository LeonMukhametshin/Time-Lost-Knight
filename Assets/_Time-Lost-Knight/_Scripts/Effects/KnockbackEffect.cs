using Game.Buffs.Interfaces;
using Game.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Effects
{
    [Serializable]
    [MovedFrom("")]
    public class KnockbackEffect : IEffect
    {
        [SerializeField] private float m_knokbackStringht;
        [SerializeField] private Vector2 m_angle;

        public void Apply(IEffectable effectable)
        {
            if (effectable is IKnockbackable knockbackable)
            {
                knockbackable.Knockback(m_angle, m_knokbackStringht);
            }
        }
    }
}
