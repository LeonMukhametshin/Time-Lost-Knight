using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Idle", menuName = "Scriptable Objects/Enemy/States/Idle")]
public sealed class IdleStateData : ScriptableObject
{
    [field: SerializeField][Range(0f, 8f)] public float minIdleTime { get; private set; }
    [field: SerializeField][Range(0f, 8f)] public float maxIdleTime { get; private set; }
}