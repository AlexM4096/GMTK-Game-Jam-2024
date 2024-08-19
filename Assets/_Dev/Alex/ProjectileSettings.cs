using Flyweight;
using UnityEngine;

namespace Alex
{
    [CreateAssetMenu(fileName = "FlyweightSettings", menuName = "Flyweight/Create ProjectileSettings")]
    public class ProjectileSettings : FlyweightSettings
    {
        [field: SerializeField] public float Velocity { get; set; }
        [field: SerializeField] public float LifeTime { get; set; }
    }
}