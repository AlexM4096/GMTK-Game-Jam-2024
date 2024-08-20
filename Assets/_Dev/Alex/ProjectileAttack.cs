using AlexTools.Extensions;
using FlyweightSystem;
using UnityEngine;

namespace Alex
{
    public class ProjectileAttack : MonoBehaviour, IAttackable
    {
        [SerializeField] private ProjectileSettings settings;
        [SerializeField] private Transform spawnPoint;
        [field: SerializeField] public float Damage { get; set; } 

        public bool CanAttack { get; set; } = true;
        public ITargetable Target { get; set; }

        void IAttackable.Attack()
        {
            var projectile = FlyweightFactory.Instance.Get(settings) as Projectile;
            projectile.transform.position = spawnPoint.position;
            projectile.Parent = this;

            var moveable = projectile.Moveable;
            var predcatedPosition = PredicatePosition(moveable);
            moveable.MoveAndLook(predcatedPosition);
        }

        private Vector3 PredicatePosition(IMoveable moveable)
        {
            return Target.Position;
        }
    }
}