using UnityEngine;

public class PresentationTeleportPoint : MonoBehaviour
{
    [field: SerializeField]
    [field: Range(1, 9)]
    public int shortcutNumber { get; private set; } = 1;

    public Vector2 position =>
        transform.position;

#if UNITY_EDITOR
    private void OnValidate() =>
        shortcutNumber = Mathf.Clamp(shortcutNumber, 1, 9);
#endif
}
