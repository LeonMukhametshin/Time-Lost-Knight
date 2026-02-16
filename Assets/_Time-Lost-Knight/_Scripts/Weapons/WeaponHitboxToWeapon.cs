using UnityEngine;

public class WeaponHitboxToWeapon : MonoBehaviour
{
    [SerializeField] private AttackingWeapon m_weapon;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("OnTriggerEnter2D " + collision.name);
        m_weapon.AddToDetected(collision);  
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("OnTriggerExit2D " + collision.name);
        m_weapon.RemoveToDetected(collision);   
    }
}