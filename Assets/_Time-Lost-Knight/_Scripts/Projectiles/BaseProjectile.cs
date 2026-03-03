using UnityEngine;

public abstract class BaseProjectile : MonoBehaviour, IProjectile
{
    [SerializeReferenceDropdown][SerializeReference] public IEffect[] effects;

    [SerializeField] protected Rigidbody2D projectileRigidbody;
    [SerializeField] protected Transform damagePosition;

    [SerializeField][Min(0)] protected float speed;
    [SerializeField][Min(0)] protected float damageRadius;
    [SerializeField][Min(0)] protected float gravity;

    [SerializeField] protected LayerMask groundLayer;
    [SerializeField] protected LayerMask playerLayer;

    protected RangeAttackData data;
    protected CircleCollider2D hitTrigger;
    protected bool isGravityOn;
    protected bool hasHitGround;
    protected float xStartPosition;

    public Vector3 position => transform.position;

    public virtual void Initialize(RangeAttackData attackData)
    {
        if (data is not null)
        {
            return;
        }
        data = attackData;
    }

    protected virtual void Awake() => 
        EnsureTriggerCollider();

    protected virtual void Start()
    {
        projectileRigidbody.gravityScale = 0f;
        projectileRigidbody.linearVelocity = transform.right * speed;
        xStartPosition = transform.position.x;
    }

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

    protected virtual bool ShouldEnableGravity() =>
        Mathf.Abs(xStartPosition - transform.position.x) >= data.travelDistance 
        && !isGravityOn;

    protected virtual void EnableGravity()
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

        int layer = other.gameObject.layer;
        if (IsInLayerMask(layer, playerLayer) && other.gameObject.TryGetComponent(out Core core))
        {
            OnHit(core);
        }
        else if (IsInLayerMask(layer, groundLayer))
        {
            OnHitGround();
        }
    }

    protected virtual void OnHit(Core core)
    {
        effects.ApplyEffect(core.effectables);
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

    protected static bool IsInLayerMask(int layer, LayerMask mask) =>
        (mask.value & (1 << layer)) != 0;

    public void DestroyProjectile() => 
        Destroy(this.gameObject);
}