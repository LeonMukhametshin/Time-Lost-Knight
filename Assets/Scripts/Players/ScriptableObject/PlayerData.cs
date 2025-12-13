using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    [Header("Ground Check")]
    [SerializeField, Tooltip("Слои, считающиеся землёй")]
    private LayerMask m_groundLayer;

    [SerializeField, Min(0.01f), Tooltip("Дистанция луча проверки земли")]
    private float m_groundCheckDistance = 0.1f;

    [SerializeField, Range(0f, 1f), Tooltip("Время coyote time (прыжок после схода с земли)")]
    private float m_coyoteTime = 0.15f;

    [Header("Jump")]
    [SerializeField, Min(0.1f), Tooltip("Сила импульса прыжка")]
    private float m_jumpForce = 12f;

    [SerializeField, Range(0f, 1f), Tooltip("Время input buffer для прыжка")]
    private float m_jumpInputBufferTime = 0.15f;

    [Header("Movement")]
    [SerializeField, Min(0.1f), Tooltip("Максимальная скорость бега")]
    private float m_runMaxSpeed = 8f;

    [SerializeField, Min(0.1f), Tooltip("Ускорение на земле")]
    private float m_runAcceleration = 50f;

    [SerializeField, Range(0f, 1f), Tooltip("Множитель ускорения в воздухе")]
    private float m_airAccelMultiplier = 0.5f;

    [Header("Dash")]
    [SerializeField, Range(0, 5), Tooltip("Максимальное количество дэшей до приземления")]
    private int m_maxDashes = 1;

    [SerializeField, Min(0.1f), Tooltip("Скорость дэша")]
    private float m_dashSpeed = 20f;

    [SerializeField, Min(0.01f), Tooltip("Длительность атакующей фазы дэша")]
    private float m_dashAttackTime = 0.15f;

    [SerializeField, Range(0f, 1f), Tooltip("Input buffer для дэша")]
    private float m_dashInputBufferTime = 0.2f;

    [Header("Gravity")]
    [SerializeField, Min(0.1f), Tooltip("Базовая гравитация")]
    private float m_gravityScale = 1f;

    [SerializeField, Min(1f), Tooltip("Множитель гравитации при падении")]
    private float m_fallGravityMultiplier = 2f;

    [SerializeField, Tooltip("Ограничение скорости падения")]
    private float m_maxFallSpeed = -25f;

    [Header("Physics Materials")]
    [SerializeField, Tooltip("Базовый физический материал")]
    private PhysicsMaterial2D m_baseMaterial;

    [SerializeField, Tooltip("Физический материал для прыжка")]
    private PhysicsMaterial2D m_jumpMaterial;

    #region Ground Check
    public LayerMask GroundLayer => m_groundLayer;
    public float GroundCheckDistance => m_groundCheckDistance;
    public float CoyoteTime => m_coyoteTime;
    #endregion

    #region Jump
    public float JumpForce => m_jumpForce;
    public float JumpInputBufferTime => m_jumpInputBufferTime;
    #endregion

    #region Movement
    public float RunMaxSpeed => m_runMaxSpeed;
    public float RunAcceleration => m_runAcceleration;
    public float AirAccelMultiplier => m_airAccelMultiplier;
    #endregion

    #region Dash
    public int MaxDashes => m_maxDashes;
    public float DashSpeed => m_dashSpeed;
    public float DashAttackTime => m_dashAttackTime;
    public float DashInputBufferTime => m_dashInputBufferTime;
    #endregion

    #region Gravity
    public float GravityScale => m_gravityScale;
    public float FallGravityMultiplier => m_fallGravityMultiplier;
    public float MaxFallSpeed => m_maxFallSpeed;
    #endregion

    #region Physics Materials
    public PhysicsMaterial2D BaseMaterial => m_baseMaterial;
    public PhysicsMaterial2D JumpMaterial => m_jumpMaterial;
    #endregion
}