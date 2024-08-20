using UnityEngine;

namespace Alex
{
    [CreateAssetMenu(menuName = "DamageCriteria/Create TagDamageCriteria")]
    public class TagDamageCriteria : DamageCriteria
    {
        [SerializeField] private string tag;

        public override bool CanTakeDamageFrom(MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.CompareTag(tag);
        }
    }
}