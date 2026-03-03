using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour, IProjectile
{
    protected IReadOnlyList<IEffect> effects;

    [SerializeField] protected Rigidbody2D projectileRigidbody;
    [SerializeField] protected Transform damagePosition;

    protected float speed => m_speed;

    private float m_speed;
    private float m_targetDistance;
    private float m_traveledDistance;

    [SerializeField][Min(0)] protected float damageRadius;
    [SerializeField][Min(0)] protected float gravity;

    [SerializeField] protected LayerMask groundLayer;

    protected RangeAttackData data;
    protected CircleCollider2D hitTrigger;
    protected bool isGravityOn;
    protected bool hasHitGround;

    private Vector3 m_direction;
    private Vector3 m_targetPosition;

    public void Initialize(Vector3 targetPosition, float speed, IReadOnlyList<IEffect> effects)
    {
        this.effects = effects;
        m_speed = speed;

        m_targetPosition = targetPosition;
        m_direction = (m_targetPosition - transform.position).normalized;
        m_traveledDistance = 0f;

    }


    protected virtual void Awake() => 
        EnsureTriggerCollider();

    protected virtual void Start()
    {
        projectileRigidbody.gravityScale = 0f;
        SetLinearVelocity();
    }

    private void SetLinearVelocity() =>
        projectileRigidbody.linearVelocity = m_direction * speed;

    protected virtual void Update()
    {
        if (!hasHitGround && isGravityOn)
        {
            UpdateRotation();
        }  
    }

    protected virtual void FixedUpdate()
    {
        if (hasHitGround)
        {
            return;
        }
        if (ShouldEnableGravity())
        {
            EnableGravity();
        }
    }

    protected virtual bool ShouldEnableGravity()
    {
        return false;
    }

    private void EnableGravity()
    {
        isGravityOn = true;
        projectileRigidbody.gravityScale = gravity;
    }

    protected virtual void UpdateRotation()
    {
        float angle = Mathf.Atan2(projectileRigidbody.linearVelocityY, projectileRigidbody.linearVelocityX) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other) => 
        HandleTrigger(other);

    protected virtual void OnTriggerStay2D(Collider2D other) => 
        HandleTrigger(other);

    private void HandleTrigger(Collider2D other)
    {
        if (hasHitGround)
        {
            return;
        }

        SelectDetectedEntities(other);
    }

    public virtual void SelectDetectedEntities(Collider2D other)
    {
        int layer = other.gameObject.layer;
        if (other.gameObject.TryGetComponent(out Core core))
        {
            OnHit(core.effectables);
        }
    }

    protected virtual void OnHit(IReadOnlyList<IEffectable> effectables)
    {
        effects.ApplyEffect(effectables);
        DestroyProjectile();
    }

    protected virtual void OnHitGround() =>
         DestroyProjectile();

    private void EnsureTriggerCollider()
    {
        if (!TryGetComponent(out hitTrigger))
            hitTrigger = gameObject.AddComponent<CircleCollider2D>();

        hitTrigger.isTrigger = true;
        hitTrigger.radius = damageRadius;

        Vector3 worldDamagePos = damagePosition != null 
            ? damagePosition.position 
            : transform.position;

        hitTrigger.offset = transform.InverseTransformPoint(worldDamagePos);
    }

    public void DestroyProjectile() => 
        Destroy(this.gameObject);
}