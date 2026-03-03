using UnityEngine;

public abstract class EntityData : ScriptableObject
{
    [field: SerializeField][Min(0)] public float maxHealth { get; private set; }
}