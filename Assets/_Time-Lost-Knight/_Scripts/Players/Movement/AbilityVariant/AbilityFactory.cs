using UnityEngine;

public class AbilityFactory
{
    private readonly CoroutineRunner m_coroutineRunner;

    public AbilityFactory(CoroutineRunner coroutineRunner)
    {
        m_coroutineRunner = coroutineRunner;
    }

    public IPlayerAbility Create(AbilityKey key, Rigidbody2D rigidbody, PlayerMovementData data)
    {
        switch(key)
        {
            case AbilityKey.Walk: 
                return new WalkAbility(data.m_moveData, rigidbody);
            case AbilityKey.Jump:
                return new JumpAbility(data.m_jumpData, rigidbody);
            case AbilityKey.Dash:
                return new DashAbility(data.m_dashData, rigidbody, m_coroutineRunner);
            default:
                throw new System.Exception("There is no necessary ability");
        }
    }
}