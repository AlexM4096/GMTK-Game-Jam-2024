using AlexTools;
using FlyweightSystem;
using System.Collections;
using UnityEngine;

namespace Alex
{
    [RequireComponent(typeof(Collider2D))]
    public class Projectile : FlyweightSystem.Flyweight
    {
        [field: SerializeField]
        public new ProjectileSettings Settings { get; private set; }

        public IAttackable Parent { get; set; }
        public IMoveable Moveable { get; private set; }

        private Coroutine _coroutine;

        public override void Initialize(FlyweightSettings settings)
        {
            base.Initialize(settings);

            Settings = settings as ProjectileSettings;
            Moveable = GetComponent<IMoveable>();
            Moveable.Speed = Settings.Velocity;
        }

        public override void OnGet() => _coroutine = StartCoroutine(DespawnRoutine());
        public override void OnRealese()
        {
            if (_coroutine != null)
                StopCoroutine(_coroutine);

            Moveable.Stop();
        }

        private IEnumerator DespawnRoutine()
        {
            yield return Waiters.GetWaitForSeconds(Settings.LifeTime);
            ReleaseSelf();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent<IDamageable>(out var damageable)) return;
            if (collision.gameObject.TryGetComponent<IAttackable>(out var attackable) && attackable == Parent) return;

            Parent.Attack(damageable);

            ReleaseSelf();
        }
    }
}