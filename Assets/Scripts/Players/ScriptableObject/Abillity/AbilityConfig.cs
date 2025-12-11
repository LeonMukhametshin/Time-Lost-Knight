using UnityEngine;

[CreateAssetMenu(fileName = "AbilityConfig", menuName = "Scriptable Objects/AbilityConfig")]
public class AbilityConfig : ScriptableObject
{
    [field: SerializeField] public bool IsEnabledByDefault { get; set; } = true;
    [field: SerializeField] public string Key { get; set; } = "Mouse0";
}