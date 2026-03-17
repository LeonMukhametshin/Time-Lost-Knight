using Game.Buffs;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Buffs.Interfaces
{
    [MovedFrom("")]
    public interface IBuff
    {
        public string id { get; }
        public Sprite icon { get;  }
        public BuffType type { get; }

        public void Initialize(BuffContainer buffContainer);
        public void Deinitialize();

        public void Update(float deltaTime);

        public IBuff Clone();
    }
}
