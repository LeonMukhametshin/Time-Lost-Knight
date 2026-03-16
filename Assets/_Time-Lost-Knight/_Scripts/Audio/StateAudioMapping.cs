using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StateAudioMapping", menuName = "Scriptable Objects/State Audio Mapping")]
public class StateAudioMapping : ScriptableObject
{
    [SerializeField] private List<StateClip> m_clips = new();
    private Dictionary<string, StateClip> m_lookup;

    private void OnEnable()
    {
        m_lookup = new Dictionary<string, StateClip>(StringComparer.Ordinal);
        foreach (var clip in m_clips)
        {
            if (clip == null || string.IsNullOrWhiteSpace(clip.stateKey) || clip.clip == null)
            {
                continue;
            }

            m_lookup[clip.stateKey] = clip;
        }
    }

    public bool TryGet(string stateKey, out StateClip clip)
    {
        if (m_lookup == null)
        {
            OnEnable();
        }

        return m_lookup.TryGetValue(stateKey, out clip);
    }
}

[Serializable]
public class StateClip
{
    [Tooltip("Имя класса состояния (Type.Name) или animBoolName.")]
    public string stateKey;

    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop;
}
