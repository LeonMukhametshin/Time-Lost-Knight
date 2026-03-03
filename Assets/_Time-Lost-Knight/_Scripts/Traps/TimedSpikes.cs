using UnityEngine;

public class TimedSpikes : Trap
{
    [SerializeReferenceDropdown]
    [SerializeReference] public IEffect[] effects;

    [SerializeField][Range(0, 10)] private float duration;

    private float m_timer;

    private void Update()
    {
        if (Pause.instants.isPaused)
        {
            return;
        }

        if(Time.time >= m_timer + duration)
        {
            Activate();

            m_timer = Time.time;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ApplyEffects(collision);
    }

    public override void ApplyEffects(Collider2D collision)
    {
        if (collision.TryGetComponent<IEffectable>(out var effectable))
        {
            effects.ApplyEffect(effectable);
        }
    }
}