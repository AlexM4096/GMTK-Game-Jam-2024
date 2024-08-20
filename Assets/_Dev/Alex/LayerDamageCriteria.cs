using UnityEngine;

namespace Alex
{
    [CreateAssetMenu(menuName = "DamageCriteria/Create LayerDamageCriteria")]
    public class LayerDamageCriteria : DamageCriteria
    {
        [SerializeField] private int layer;

        public override bool CanTakeDamageFrom(MonoBehaviour monoBehaviour)
        {
            return monoBehaviour.gameObject.layer == layer;
        }
    }
}