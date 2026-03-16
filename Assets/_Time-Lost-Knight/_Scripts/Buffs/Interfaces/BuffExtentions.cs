using Game.Buffs;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Buffs.Interfaces
{
    [MovedFrom("")]
    public static class BuffExtentions
    {
        public static void Refresh(this IBuff buff, BuffContainer container)
        {
            buff.Deinitialize();
            buff.Initialize(container);
        }
    }
}
