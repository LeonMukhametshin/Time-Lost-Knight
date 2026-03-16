using Game.Weapons;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Player
{
    [MovedFrom("")]
    // There will be inventory here someday, but not today.
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField] public Weapon[] weapons;
    }
}
