using AlexTools.Extensions;
using UnityEngine;

namespace FlyweightSystem
{
    [CreateAssetMenu(fileName = "FlyweightSettings", menuName = "FlyweightSystem/Create FlyweightSettings")]
    public class FlyweightSettings : ScriptableObject
    {
        [field: SerializeField]
        public bool CollectionCheck { get; private set; } = true;

        [field: SerializeField]
        public int DefaultCapacity { get; private set; } = 10;

        [field: SerializeField]
        public int MaxPoolSize { get; private set; } = 10000;

        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        public virtual Flyweight Create()
        {
            var go = Instantiate(Prefab);
            go.name = Prefab.name;

            var flyweight = go.GetOrAddComponent<Flyweight>();
            flyweight.Initialize(this);

            return flyweight;
        }

        public virtual void OnGet(Flyweight f)
        {
            f.gameObject.Enable();
            f.OnGet();
        }

        public virtual void OnRelease(Flyweight f)
        {
            f.OnRealese();
            f.gameObject.Disable();
        }

        public virtual void OnDestroyPoolObject(Flyweight f) => Destroy(f.gameObject);
    }
}