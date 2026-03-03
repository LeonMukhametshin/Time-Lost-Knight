using System;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerAbilityType
{
    WallGrab,
    WallClimb,
    ForwardDash,
    OmnidirectionalDash,
    Crouch,
    DropDown,
}

[Serializable]
public class PlayerAbilityEntry
{
    public PlayerAbilityType abilityType;
    public bool enabled = true;
}

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private List<PlayerAbilityEntry> m_abilities = new();

    private readonly Dictionary<PlayerAbilityType, bool> m_lookup =
        new Dictionary<PlayerAbilityType, bool>();

    private void Awake()
    {
        m_lookup.Clear();

        foreach (var entry in m_abilities)
        {
            if (!m_lookup.ContainsKey(entry.abilityType))
            {
                m_lookup.Add(entry.abilityType, entry.enabled);
            }
            else
            {
                m_lookup[entry.abilityType] = entry.enabled;
            }
        }
    }

    public bool IsEnabled(PlayerAbilityType abilityType)
    {
        if (m_lookup.TryGetValue(abilityType, out var enabled))
        {
            return enabled;
        }

        return false;
    }

    public void SetEnabled(PlayerAbilityType abilityType, bool enabled)
    {
        if (m_lookup.ContainsKey(abilityType))
        {
            m_lookup[abilityType] = enabled;
        }
        else
        {
            m_lookup.Add(abilityType, enabled);
        }

        for (int i = 0; i < m_abilities.Count; i++)
        {
            if (m_abilities[i].abilityType == abilityType)
            {
                m_abilities[i].enabled = enabled;
                return;
            }
        }

        m_abilities.Add(new PlayerAbilityEntry
        {
            abilityType = abilityType,
            enabled = enabled
        });
    }

    public void Enable(PlayerAbilityType abilityType) =>
        SetEnabled(abilityType, true);

    public void Disable(PlayerAbilityType abilityType) =>
        SetEnabled(abilityType, false);
}
