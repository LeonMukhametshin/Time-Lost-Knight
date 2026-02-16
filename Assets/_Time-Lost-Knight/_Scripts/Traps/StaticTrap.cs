using UnityEngine;

public class StaticTrap : Trap
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
    }

    public override void Damage(Collider2D collision)
    {
        base.Damage(collision);

        Debug.Log($"{collision.name} damage from {this.name} in amount of {damage}");
    }
}