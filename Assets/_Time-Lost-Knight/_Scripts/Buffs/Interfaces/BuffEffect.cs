using Game.Buffs;
using System;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Buffs.Interfaces
{
    [Serializable]
    [MovedFrom("")]
    public class BuffEffect : IEffect
    {
        [SerializeReferenceDropdown] [SerializeReference] private IBuff[] m_buff;

        public void Apply(IEffectable effectable)
        {
            if (effectable is BuffContainer container)
            {
                foreach (var buff in m_buff)
                {
                    container.Add(buff.Clone());
                }
            }
        }
    }
}
