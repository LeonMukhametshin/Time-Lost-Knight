using UnityEngine.Scripting.APIUpdating;

namespace Game.Buffs.Interfaces
{
    [MovedFrom("")]
    public interface IEffect
    {
        void Apply(IEffectable effectable);
    }
}
