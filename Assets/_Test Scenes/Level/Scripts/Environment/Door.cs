using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Door : MonoBehaviour // Script created by AI for testing levels with doors
{
    [Header("Assign")]
    [Tooltip("The other door to which the player will be teleported")]
    public Door targetDoor;

    [Tooltip("The point where the player's BOTTOM CENTER will appear (place this directly in front of the door, outside)")]
    public Transform spawnPoint;

    [Header("Auto-avoid settings")]
    [Tooltip("The 'push-out' direction from the door, local (used if there's a collision). " +
             "For a side-scrolling view usually Vector2.right or Vector2.left (local axis).")]
    public Vector2 ejectDirection = Vector2.right;

    [Tooltip("Step distance when searching for a free position")]
    public float stepDistance = 0.1f;

    [Tooltip("Maximum number of steps to search for a free position")]
    public int maxAttempts = 40;

    [Tooltip("Seconds to block repeated teleport to avoid loops")]
    public float teleportCooldown = 0.18f;

    // Internal flag to prevent the door from accepting teleports temporarily
    bool canTeleport = true;

    Collider2D myCollider;

    void Awake()
    {
        myCollider = GetComponent<Collider2D>();
        myCollider.isTrigger = true; // Must be a trigger
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!canTeleport) return;
        if (targetDoor == null || spawnPoint == null) return;
        if (!other.CompareTag("Player")) return;

        // Start the teleport coroutine
        StartCoroutine(TeleportCoroutine(other));
    }

    IEnumerator TeleportCoroutine(Collider2D playerCollider)
    {
        // Block both doors temporarily
        canTeleport = false;
        targetDoor.canTeleport = false;

        // Find a safe bottom-center position at the target door (world space).
        // NOTE: FindSafePosition returns the desired bottom-center world position.
        Vector2 safeBottomCenter = targetDoor.FindSafePosition(playerCollider);

        // We need to place the player's transform so that the player's bottom-center equals safeBottomCenter.
        // Compute current bottom-center and offset from transform position to bottom-center (delta).
        Bounds bounds = playerCollider.bounds;
        Vector2 currentBottomCenter = new Vector2(bounds.min.x + bounds.size.x * 0.5f, bounds.min.y);
        Vector2 deltaFromTransformToBottom = currentBottomCenter - (Vector2)playerCollider.transform.position;

        // New transform position that makes bottom-center == safeBottomCenter:
        Vector2 newTransformPos = safeBottomCenter - deltaFromTransformToBottom;

        // Move the player and reset velocity if they have a Rigidbody2D
        Rigidbody2D rb = playerCollider.attachedRigidbody;
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            // Use physics-positioning
            rb.position = newTransformPos;
        }
        else
        {
            playerCollider.transform.position = newTransformPos;
        }

        // Small delay to prevent immediate retrigger
        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
        targetDoor.canTeleport = true;
    }

    // Finds a bottom-center position (world) where the player does not overlap other non-trigger colliders.
    // Returns spawnPoint (bottom-center) if free, or an offset bottom-center position if necessary.
    public Vector2 FindSafePosition(Collider2D playerCollider)
    {
        Vector2 baseBottom = spawnPoint.position; // This is the desired bottom-center location (world)
        if (playerCollider == null) return baseBottom;

        // Get player's bounds to compute the relation between bottom-center and collider center
        Bounds bounds = playerCollider.bounds;
        Vector2 size = bounds.size;

        // current bottom center (world)
        Vector2 currentBottomCenter = new Vector2(bounds.min.x + bounds.size.x * 0.5f, bounds.min.y);
        // vector from bottom-center to bounds.center (world): bounds.center - bottomCenter
        Vector2 centerFromBottom = (Vector2)bounds.center - currentBottomCenter;
        // Now, when we test a candidate bottom-center (attemptBottom), the corresponding overlap-center is:
        // overlapCenter = attemptBottom + centerFromBottom

        // Try zero offset first
        Vector2 overlapCenter = baseBottom + centerFromBottom;
        if (!IsOverlappingAt(overlapCenter, size, playerCollider))
            return baseBottom;

        // Move in the local ejectDirection, considering the door's rotation
        Vector2 worldDir = (Vector2)transform.TransformDirection(ejectDirection.normalized);

        for (int i = 1; i <= maxAttempts; i++)
        {
            Vector2 attemptBottom = baseBottom + worldDir * (stepDistance * i);
            overlapCenter = attemptBottom + centerFromBottom;
            if (!IsOverlappingAt(overlapCenter, size, playerCollider))
                return attemptBottom;
        }

        // If not found, try the opposite direction
        for (int i = 1; i <= maxAttempts; i++)
        {
            Vector2 attemptBottom = baseBottom - worldDir * (stepDistance * i);
            overlapCenter = attemptBottom + centerFromBottom;
            if (!IsOverlappingAt(overlapCenter, size, playerCollider))
                return attemptBottom;
        }

        // Could not find a free spot — return the base bottom (might need manual adjustment)
        return baseBottom;
    }

    // Checks if there is an overlap with any non-trigger colliders at the given overlapCenter position (which should be the collider's center).
    bool IsOverlappingAt(Vector2 overlapCenter, Vector2 size, Collider2D playerCollider)
    {
        // Get all colliders that overlap the box
        Collider2D[] hits = Physics2D.OverlapBoxAll(overlapCenter, size, 0f);

        foreach (var c in hits)
        {
            if (c == null) continue;
            if (c == playerCollider) continue; // ignore the player itself
            if (c == myCollider) continue; // ignore the door's collider
            if (c.isTrigger) continue; // ignore triggers
            // Found an invalid collider -> position is unsafe
            return true;
        }
        return false;
    }

    // Editor visualization — shows spawnPoint and eject direction
    void OnDrawGizmosSelected()
    {
        if (spawnPoint != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(spawnPoint.position, 0.05f);
            Vector3 worldDir3 = transform.TransformDirection(ejectDirection.normalized);
            Gizmos.DrawLine(spawnPoint.position, spawnPoint.position + worldDir3 * 0.5f);
        }
    }
}
