using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputConfig", menuName = "Scriptable Objects/PlayerInputConfig")]
public sealed class PlayerInputConfig : ScriptableObject
{
    [field: SerializeField] public string HorizontalInputAxis { get; private set; } = "Horizontal";
    [field: SerializeField] public KeyCode JumpButton { get; private set; } = KeyCode.Space;
    [field: SerializeField] public KeyCode DashButton { get; private set; } = KeyCode.LeftShift;
    [field: SerializeField] public bool UseGamepadInput { get; private set; } = false;
}