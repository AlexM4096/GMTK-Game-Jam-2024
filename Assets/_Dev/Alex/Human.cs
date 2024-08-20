using FlyweightSystem;
using UnityEngine;

namespace Alex
{
    [RequireComponent(typeof(Health))]
    public class Human : MonoBehaviour
    {
        private Health health;
        private Flyweight flyweight;

        private void Awake()
        {
            health = GetComponent<Health>();
            flyweight = GetComponent<Flyweight>();
        }

        private void OnEnable()
        {
            health.DeathEvent += OnDeath;
        }

        private void OnDisable()
        {
            health.DeathEvent += OnDeath;
        }

        private void OnDeath()
        {
            Destroy(gameObject);
            // flyweight.ReleaseSelf();
        }
    }
}