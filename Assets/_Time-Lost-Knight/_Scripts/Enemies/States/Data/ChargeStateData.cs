using UnityEngine;

[CreateAssetMenu(fileName = "ChargeState_Data", menuName = "Scriptable Objects/State Data/ChargeState_Data")]
public class ChargeStateData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float chargeSpeed { get; private set; }
    [field: SerializeField][Min(0)] public float chargeTime { get; private set; }
}