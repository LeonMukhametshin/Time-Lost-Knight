using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Enemies.States.Datas
{
    [CreateAssetMenu(fileName = "Stun", menuName = "Scriptable Objects/Enemy/States/Stun")]
    [MovedFrom("")]
    public sealed class StunStateData : ScriptableObject
    {
        [field: SerializeField] public float stunTime { get; private set; }
        [field: SerializeField] public float stunKnockbactTime {  get; private set; }
    }
}
