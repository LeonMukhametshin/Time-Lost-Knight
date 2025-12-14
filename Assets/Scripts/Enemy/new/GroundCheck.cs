using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [Header("Ground Check Settings")]
    [SerializeField] private float groundCheckForwardDistance = 0.5f; 
    [SerializeField] private float groundCheckRayLength = 1.0f; 
    [SerializeField] private float groundCheckHeightOffset = -0.2f; 
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private bool visualizeChecks = true;

    [Header("Obstacle Check Settings")]
    [SerializeField] private float obstacleCheckDistance = 0.3f;
    [SerializeField] private float obstacleCheckHeight = 0.2f; 
    [SerializeField] private LayerMask obstacleLayer;

    // Цвета для визуализации
    private Color groundCheckColor = Color.green;
    private Color noGroundColor = Color.red;
    private Color obstacleCheckColor = Color.yellow;

    public bool HasGroundAhead(int direction)
    {
        Vector2 rayOrigin = CalculateRayOrigin(direction);
        Vector2 rayDirection = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            rayDirection,
            groundCheckRayLength,
            groundLayer
        );
        return hit.collider != null;
    }

    public bool HasObstacleAhead(int direction)
    {
        Vector2 rayOrigin = CalculateObstacleRayOrigin(direction);
        Vector2 rayDirection = new Vector2(direction, 0);

        RaycastHit2D hit = Physics2D.Raycast(
            rayOrigin,
            rayDirection,
            obstacleCheckDistance,
            obstacleLayer
        );

        return hit.collider != null && hit.collider.gameObject != gameObject;
    }

    public bool IsPathBlocked(int direction)
    {
        return HasObstacleAhead(direction) || !HasGroundAhead(direction);
    }

    private Vector2 CalculateRayOrigin(int direction)
    {
        Vector2 origin = transform.position;

        origin.x += direction * groundCheckForwardDistance;

        origin.y += groundCheckHeightOffset;

        return origin;
    }

    private Vector2 CalculateObstacleRayOrigin(int direction)
    {
        Vector2 origin = transform.position;

        origin.y += obstacleCheckHeight;

        return origin;
    }

    #region Визуализация для отладки
    private void OnDrawGizmosSelected()
    {
        if (!visualizeChecks) return;

        DrawGroundCheckGizmos(1); 
        DrawGroundCheckGizmos(-1); 

        DrawObstacleCheckGizmos(1);
        DrawObstacleCheckGizmos(-1);
    }

    private void DrawGroundCheckGizmos(int direction)
    {
        Vector2 rayOrigin = CalculateRayOrigin(direction);
        Vector2 rayEnd = rayOrigin + (Vector2.down * groundCheckRayLength);

        bool hasGround = false;
        if (Application.isPlaying)
        {
            hasGround = HasGroundAhead(direction);
        }

        Gizmos.color = hasGround ? groundCheckColor : noGroundColor;

        Gizmos.DrawLine(rayOrigin, rayEnd);

        Gizmos.DrawSphere(rayOrigin, 0.05f);

        Gizmos.DrawWireSphere(rayEnd, 0.03f);

#if UNITY_EDITOR
        UnityEditor.Handles.Label(
            rayOrigin + new Vector2(0, 0.1f),
            $"Ground Check ({((direction > 0) ? "Right" : "Left")})"
        );
#endif
    }

    private void DrawObstacleCheckGizmos(int direction)
    {
        Vector2 rayOrigin = CalculateObstacleRayOrigin(direction);
        Vector2 rayEnd = rayOrigin + new Vector2(direction * obstacleCheckDistance, 0);

        Gizmos.color = obstacleCheckColor;
        Gizmos.DrawLine(rayOrigin, rayEnd);
        Gizmos.DrawSphere(rayOrigin, 0.05f);
    }
    #endregion
}
