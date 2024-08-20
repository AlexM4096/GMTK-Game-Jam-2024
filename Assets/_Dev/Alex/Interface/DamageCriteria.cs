using UnityEngine;

namespace Alex
{
    public abstract class DamageCriteria : ScriptableObject
    {
        public abstract bool CanTakeDamageFrom(MonoBehaviour monoBehaviour);
    }
}