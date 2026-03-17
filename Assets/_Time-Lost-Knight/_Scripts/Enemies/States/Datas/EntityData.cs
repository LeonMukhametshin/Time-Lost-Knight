using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [MovedFrom("")]
    public abstract class EntityData : ScriptableObject
    {
        [field: SerializeField][Min(0)] public float maxHealth { get; private set; }
    }
}
