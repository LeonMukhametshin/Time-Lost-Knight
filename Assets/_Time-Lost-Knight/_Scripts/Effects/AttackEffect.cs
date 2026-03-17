using Game.Buffs.Interfaces;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Effects
{
    [Serializable]
    [MovedFrom("")]
    public class AttackEffect : IEffect
    {
        [SerializeField][Min(0)] private float m_damage;

        public void Apply(IEffectable effectable)
        {
            if(effectable is IDamageable damageable)
            {
                damageable.TakeDamage(m_damage);
            }
        }
    }
}
