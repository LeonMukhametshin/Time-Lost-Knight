using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Scriptable Objects/Interaction/Audio Config")]
public class InteractionAudioConfig : ScriptableObject
{
    [field: SerializeField] public AudioClip clip { get; private set; }
    [field: SerializeField][Range(0f, 1f)] public float volume { get; private set; } = 1f;
}