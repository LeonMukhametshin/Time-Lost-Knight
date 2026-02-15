using UnityEngine;

[CreateAssetMenu(fileName = "ColliderControllerData", menuName = "Scriptable Objects/ColliderControllerData")]
public class ColliderControllerData : ScriptableObject
{
    [field: SerializeField] public LayerMask playerLayerMask { get; private set; }
    [field: SerializeField] public LayerMask platformLayerMask { get; private set; }
    [field: SerializeField] public float ignoreLayerDuration { get; private set; }
}