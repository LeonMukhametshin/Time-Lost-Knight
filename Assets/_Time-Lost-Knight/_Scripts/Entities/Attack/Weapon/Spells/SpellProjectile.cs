using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class SpellProjectile : MonoBehaviour, ISpellProjectile
{
    [SerializeField] private Rigidbody2D m_rigidbody;

    private IReadOnlyList<IEffect> m_effects;
    private AnimationCurve m_timingCurve;
    private Tween m_flightTween;

    private bool m_initialized;

    private void OnValidate()
    {
        if (!m_rigidbody)
        {
            m_rigidbody = GetComponent<Rigidbody2D>();
        }
    }

    private void Awake()
    {
        m_rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        m_rigidbody.gravityScale = 0f;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!m_initialized) return;

        if (other.TryGetComponent<IEffectable>(out var effectable))
        {
            m_effects.ApplyEffect(effectable);
        }

        m_effects.ApplyEffect(other.GetComponents<IEffectable>());

        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        m_flightTween?.Kill();
    }

    public void Initialize(
        float speed,
        AnimationCurve timingCurve,
        IReadOnlyList<IEffect> effects)
    {
        //DOTWEEN
    }
}