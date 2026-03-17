using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "Charge", menuName = "Scriptable Objects/Enemy/States/Charge")]
    [MovedFrom("")]
    public sealed class ChargeStateData : ScriptableObject
    {
        [field: SerializeField][Min(0)] public float chargeSpeed { get; private set; }
        [field: SerializeField][Min(0)] public float chargeTime { get; private set; }
    }
}
