using UnityEngine;

public class Knockback : IEffect
{
    [SerializeField] private float m_knockbackForce;

    public void Apply(IEffectable effectable)
    {
        if(effectable is IPhysics physics)
        {
            physics.AddForce(Vector2.up * m_knockbackForce, ForceMode2D.Impulse);
        }
    }
}