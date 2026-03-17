using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Audio
{
    [CreateAssetMenu(fileName = "Audio Config", menuName = "Scriptable Objects/Interaction/Audio Config")]
    [MovedFrom("")]
    public class InteractionAudioConfig : ScriptableObject
    {
        [field: SerializeField] public AudioClip clip { get; private set; }
        [field: SerializeField][Range(0f, 1f)] public float volume { get; private set; } = 1f;
    }
}
