using Game.Weapons;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player
{
    [MovedFrom("")]
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] public Weapon[] weapons;
    }
}
