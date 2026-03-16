using NUnit.Framework.Internal;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interfaces
{
    [MovedFrom("")]
    public interface IKnockbackable
    {
        void Knockback(Vector2 angle, float strength);

        void Knockback(Vector2 angle, float strength, int direction);
    }
}
