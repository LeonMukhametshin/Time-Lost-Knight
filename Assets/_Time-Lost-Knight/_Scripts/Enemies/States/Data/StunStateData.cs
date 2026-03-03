using UnityEngine;

[CreateAssetMenu(fileName = "Stun", menuName = "Scriptable Objects/Enemy/States/Stun")]
public sealed class StunStateData : ScriptableObject
{
    [field: SerializeField] public float stunTime { get; private set; }
    [field: SerializeField] public float stunKnockbactTime {  get; private set; }
}