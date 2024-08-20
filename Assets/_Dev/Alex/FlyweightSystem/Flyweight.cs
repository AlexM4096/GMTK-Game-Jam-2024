using UnityEngine;

namespace FlyweightSystem
{
    public class Flyweight : MonoBehaviour
    {
        public FlyweightSettings Settings { get; private set; }

        public virtual void Initialize(FlyweightSettings settings)
        {
            Settings = settings;
        }

        public virtual void OnGet() { }
        public virtual void OnRealese() { }

        public void ReleaseSelf() => FlyweightFactory.Instance.Release(this);
    }
}