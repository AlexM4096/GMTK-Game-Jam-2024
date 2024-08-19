using AlexTools.Extensions;
using Flyweight;
using UnityEngine;

namespace Alex
{
    [CreateAssetMenu(fileName = "FlyweightSettings", menuName = "Flyweight/Create ProjectileSettings")]
    public class ProjectileSettings : FlyweightSettings
    {
        [field: SerializeField] public float Velocity { get; set; }
        [field: SerializeField] public float LifeTime { get; set; }

        public override Flyweight.Flyweight Create()
        {
            var go = Instantiate(Prefab);
            go.name = Prefab.name;

            var flyweight = go.GetOrAddComponent<Projectile>();
            flyweight.Initialize(this);

            return flyweight;
        }
    }
}