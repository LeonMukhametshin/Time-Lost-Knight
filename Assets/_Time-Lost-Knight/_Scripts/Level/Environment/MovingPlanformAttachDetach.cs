using UnityEngine;

public class MovingPlanformAttachDetach : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player))
        {
            return;
        }

        Attach(collision.transform, collision.rigidbody);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag(Tags.Player))
        {
            return;
        }

        Detach(collision.transform, collision.rigidbody);
    }

    private void Attach(Transform target, Rigidbody2D rb)
    {
        target.SetParent(transform);

        if (rb is not null)
        {
            rb.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void Detach(Transform target, Rigidbody2D rb)
    {
        target.SetParent(null);

        if (rb is not null)
        {
            rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }
}