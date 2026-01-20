using UnityEngine;

public class Knockback : IEffect
{
    [SerializeField] private float m_knockbackForce;

    public void Apply(IEffectable effectable)
    {
        if(effectable is IPhysics rigidbody)
        {
            rigidbody.AddForce(Vector2.up, ForceMode2D.Impulse);
        }
    }
}