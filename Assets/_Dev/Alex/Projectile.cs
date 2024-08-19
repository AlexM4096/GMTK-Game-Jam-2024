using AlexTools;
using System.Collections;
using UnityEngine;

namespace Alex
{
    public class Projectile : Flyweight.Flyweight
    {
        new ProjectileSettings Settings => (ProjectileSettings)base.Settings;

        public IAttackable Parent { get; set; }

        private Coroutine _coroutine;

        public override void OnGet() => _coroutine = StartCoroutine(DespawnRoutine());
        public override void OnRealese()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);
        }

        private IEnumerator DespawnRoutine()
        {
            yield return Waiters.GetWaitForSeconds(Settings.LifeTime);
            ReleaseSelf();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IDamageable>(out var damageable))
                return;

            Parent.Attack(damageable);

            ReleaseSelf();
        }
    }
}