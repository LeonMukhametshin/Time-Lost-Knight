using System;
using UnityEngine;

[Serializable]
public class AttackEffect : IEffect
{
    [SerializeField][Min(0)] private float m_damage;

    public void Apply(IEffectable effectable)
    {
        if(effectable is IDamageable damageable)
        {
            damageable.TakeDamage(m_damage);
            Debug.Log(m_damage);
        }
    }
}