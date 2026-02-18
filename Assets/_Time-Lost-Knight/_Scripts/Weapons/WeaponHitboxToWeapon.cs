using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    [SerializeField] private AttackingWeapon m_weapon;

    private void OnTriggerEnter2D(Collider2D collision) =>
          m_weapon.AddToDetected(collision);

    private void OnTriggerExit2D(Collider2D collision) =>
         m_weapon.RemoveToDetected(collision);
}