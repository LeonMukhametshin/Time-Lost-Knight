using UnityEngine;

[CreateAssetMenu(fileName = "StanState_Data", menuName = "Scriptable Objects/State Data/StanState_Data")]
public class StanStateData : ScriptableObject
{
    [field: SerializeField] public float stunTime { get; private set; }
    [field: SerializeField] public float stunKnockbactTime {  get; private set; }
    [field: SerializeField] public float stunKnockbackSpeed { get; private set; }   

    [field: SerializeField] public Vector2 stunKnockbackAngle { get; private set; }
}