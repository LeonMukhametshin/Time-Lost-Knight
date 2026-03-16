using Game.Enemies.States;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "MoveState", menuName = "Scriptable Objects/Enemy/States/MoveState")]
    [MovedFrom("")]
    public sealed class MoveStateData : ScriptableObject
    {
        [field: SerializeField][Min(0f)] public float movementSpeed { get; private set; }
    }
}
