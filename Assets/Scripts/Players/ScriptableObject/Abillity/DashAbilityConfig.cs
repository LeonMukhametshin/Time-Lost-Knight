using UnityEngine;

[CreateAssetMenu(fileName = "DashAbilityConfig", menuName = "Scriptable Objects/DashAbilityConfig")]
public class DashAbilityConfig : AbilityConfig
{
    [field: SerializeField] public float DashDistance { get; private set; } = 5.0f;
    [field: SerializeField] public float DashDuration { get; private set; } = 0.5f;
    [field: SerializeField] public float DashCooldown { get; private set; } = 1.0f;

    public float DashSpeed => DashDistance / DashDuration;
}