using UnityEngine;

public class PlayerFSM : EntityFSM
{
    public void AddState<T>(T ability) where T : PlayerState
    {
        m_states.Add(ability.GetType(), ability);
    }

    public void RemoveState<T>(T ability) where T : PlayerState
    {
        m_states.Remove(ability.GetType());
    }

    public bool CheckContainesState(PlayerState ability) =>
        m_states.ContainsKey(ability.GetType());

    public bool CheckCanUseState(PlayerState ability) =>
        ability.CheckAbilityUseState();


    public override void ChangeState<T>()
    {
        if (m_states.ContainsKey(typeof(T)))
        {
            base.ChangeState<T>();
        }
    }
}
