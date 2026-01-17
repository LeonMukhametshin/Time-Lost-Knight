using UnityEngine;

public class AbilityFactory
{
    public IPlayerAbility Create(string key, Rigidbody2D rigidbody, PlayerMovementData data)
    {
        switch(key)
        {
            case AbilityKey.WALK: 
                return new WalkAbility(data.m_moveData, rigidbody);
            case AbilityKey.JUMP:
                return new JumpAbility(data.m_jumpData, rigidbody);
            case AbilityKey.DASH:
                return new DashAbility(data.m_dashData, rigidbody);
            default:
                throw new System.Exception("There is no necessary ability");
        }
    }
}