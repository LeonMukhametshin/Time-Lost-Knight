using UnityEngine;

public class StaticSpikes : Trap
{
    [SerializeField][Range(0, 10)] private float duration;

    private float m_timer;

    private void Update()
    {
        if (Time.time >= m_timer + duration)
        {
            Activate();

            m_timer = Time.time;
        }
    }

    public override void Activate()
    {
        base.Activate();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Damage(collision);
    }

    public override void Damage(Collider2D collision)
    {
        base.Damage(collision);

        Debug.Log($"{collision.name} damage from {this.name} in amount of {damage}");
    }
}