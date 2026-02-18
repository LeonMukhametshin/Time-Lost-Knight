using NUnit.Framework.Internal;
using UnityEngine;

public interface IKnockbackable
{
    void Knockback(Vector2 angle, float strength);

    void Knockback(Vector2 angle, float strength, int direction);
}