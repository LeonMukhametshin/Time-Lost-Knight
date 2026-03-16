using UnityEngine.Scripting.APIUpdating;

namespace Game.Buffs.Interfaces
{
    [MovedFrom("")]
    public interface IAcceleration
    {
        void IncreaseAcceleration(float delta);
        void DecreaseAcceleration(float delta);
    }
}
