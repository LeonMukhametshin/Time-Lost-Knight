using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

namespace Game.Projectiles
{
    [MovedFrom("")]
    public class ExplosionProjectile : BaseProjectile
    {
        [SerializeField] private float m_explosionRadius = 2f;

        public override void OnTriggerEnter2D(Collider2D collision)
        {
            DestroyProjectile();
        }

        private void HitInRadius()
        {
            Collider2D[] hitObjects = Physics2D.OverlapCircleAll(transform.position, m_explosionRadius);

            foreach (var obj in hitObjects)
            {
                HitObject(obj);
            }
        }

        protected override void DestroyProjectile()
        {
            HitInRadius();
            base.DestroyProjectile();
        }
    }
}
