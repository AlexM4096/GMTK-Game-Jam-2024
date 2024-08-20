using System;
using System.Collections;
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
        private float baseMaxHealth;

        private float modifiedMaxHealth;

        private Coroutine revertHealthRoutine;

        public float CurrentHealth
        {
            get => currentHealth;
            set
            {
                currentHealth = value.AtMost(modifiedMaxHealth);

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    DeathEvent?.Invoke();
                }

                HealthChangeEvent?.Invoke(currentHealth);
            }
        }

        public float BaseMaxHealth
        {
            get => baseMaxHealth;
            set
            {
                baseMaxHealth = value;
                UpdateModifiedHealth();
            }
        }

        public float ModifiedMaxHealth => modifiedMaxHealth;

        public event Action<float> HealthChangeEvent;
        public event Action DeathEvent;

        private void Awake()
        {
            modifiedMaxHealth = baseMaxHealth;
            currentHealth = modifiedMaxHealth;
        }

        public void ApplyHealthModifier(float multiplier, float duration)
        {
            if (revertHealthRoutine != null)
            {
                StopCoroutine(revertHealthRoutine);
            }

            modifiedMaxHealth = baseMaxHealth * multiplier;
            CurrentHealth *= multiplier;

            revertHealthRoutine = StartCoroutine(RevertHealthAfterTime(duration));
        }

        private IEnumerator RevertHealthAfterTime(float duration)
        {
            yield return new WaitForSeconds(duration);
            modifiedMaxHealth = baseMaxHealth;

            CurrentHealth = currentHealth / modifiedMaxHealth * baseMaxHealth;
        }

        private void UpdateModifiedHealth()
        {
            var percentage = currentHealth / modifiedMaxHealth;
            modifiedMaxHealth = baseMaxHealth;
            CurrentHealth = modifiedMaxHealth * percentage;
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
