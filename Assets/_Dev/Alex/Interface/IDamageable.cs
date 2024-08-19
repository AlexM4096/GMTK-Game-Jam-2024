using System.Collections;
using UnityEngine;

namespace Alex
{
    public interface IDamageable
    {
        void TakeDamage(float amount, IAttackable source);
    }
}