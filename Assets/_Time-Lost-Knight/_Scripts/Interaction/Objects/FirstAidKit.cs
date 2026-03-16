using Game.Buffs;
using Game.Buffs.Interfaces;
using Game.Core.ServiceLocatorSpace;
using Game.Player;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Interaction.Objects
{
    [MovedFrom("")]
    public class FirstAidKit : MonoBehaviour
    {
        [SerializeField] private BuffEffect[] buff;

        private BuffContainer health;

        private void Start()
        {
            health = ServiceLocator
                .Get<IPlayerFactory>().Create()
                .GetComponent<BuffContainer>();
        }

        public void Heal()
        {
            buff.ApplyEffect(health);
            Destroy(gameObject);
        }
    }
}
