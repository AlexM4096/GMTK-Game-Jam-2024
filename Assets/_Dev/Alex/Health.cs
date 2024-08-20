using System;
using System.Collections.Generic;
using System.Linq;
using AlexTools.Extensions;
using UnityEngine;

namespace Alex
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeReference]
        private List<DamageCriteria> criterias;

        [SerializeField]
        private float currentHealth;

        [SerializeField]
        private float maxHealth;

        public float CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value.AtMost(maxHealth);

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    DeathEvent?.Invoke();
                }

                HealthChangeEvent?.Invoke(currentHealth);
            }
        }

        public float MaxHealth
        {
            get => maxHealth;
            set
            {
                var precntage = currentHealth / maxHealth;
                maxHealth = value;
                CurrentHealth *= precntage;
            }
        }

        public event Action<float> HealthChangeEvent;
        public event Action DeathEvent;

        private void Awake()
        {
            currentHealth = maxHealth;
        }

        public void TakeDamage(float amount, IAttackable source)
        {
            if (criterias != null && criterias.Count > 0)
            {
                if (source is not MonoBehaviour monoBehaviour)
                    return;

                if (!criterias.Any(x => x.CanTakeDamageFrom(monoBehaviour)))
                    return;
            }

            CurrentHealth -= amount;
            print($"Me {name} take damage {amount} from {source}");
        }

#if UNITY_EDITOR
        [ContextMenu("Take Damage")]
        public void TakeDamage()
        {
            TakeDamage(10, null);
        }
#endif
    }
}
