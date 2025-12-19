using Attacks;

namespace Attack
{
    public class SwordWithCritDamage : CritDamageDecorator
    {
        private CritConfig critConfig;

        public SwordWithCritDamage(IWeapon weapon, CritConfig critConfig) : base(weapon, critConfig)
        {
        }


    }
}
