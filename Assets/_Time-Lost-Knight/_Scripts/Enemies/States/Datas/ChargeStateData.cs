using UnityEngine;

[CreateAssetMenu(fileName = "Charge", menuName = "Scriptable Objects/Enemy/States/Charge")]
public sealed class ChargeStateData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float chargeSpeed { get; private set; }
    [field: SerializeField][Min(0)] public float chargeTime { get; private set; }
}