using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityEntry
{
    public PlayerState abilityType;
    public bool enabled = true;
}

public class PlayerAbilities : MonoBehaviour
{
    [SerializeField] private List<PlayerAbilityEntry> m_abilities = new();

    private PlayerFSM m_playerFSM;
    private readonly Dictionary<PlayerState, bool> m_lookup =
        new Dictionary<PlayerState, bool>();

    public PlayerAbilities(PlayerFSM fsm) 
    {
        m_playerFSM = fsm;
    }

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

    public bool IsEnabled<T>(T abilityType) where T : PlayerState
    {
        if (m_lookup.TryGetValue(abilityType, out var enabled))
        {

            return enabled;
        }

        return false;
    }

    public void SetEnabled<T>(T abilityType) where T : PlayerState
    {
        if (m_playerFSM.CheckContainesState(abilityType))
        {
            m_playerFSM.AddState(abilityType);
        }

        else
        {
            Debug.Log("You have this ability");
        }
    }

    public void Enable(PlayerState abilityType) =>
        SetEnabled(abilityType);
}
