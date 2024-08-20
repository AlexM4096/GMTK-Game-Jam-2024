using AlexTools.Singleton;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace FlyweightSystem
{
    public class FlyweightFactory : Singleton<FlyweightFactory>
    {
        [SerializeField] private bool createIfNotExist = true;
        [SerializeField] private List<FlyweightSettings> startSettings = new();

        private readonly Dictionary<FlyweightSettings, int> _idDictionary = new();
        private readonly Dictionary<int, IObjectPool<Flyweight>> _poolDictionary = new();

        protected override void Awake()
        {
            base.Awake();

            startSettings.ForEach(x => CreatePool(x));
        }

        public IObjectPool<Flyweight> CreatePool(FlyweightSettings settings)
        {
            if (_idDictionary.TryGetValue(settings, out var id) &&
                _poolDictionary.TryGetValue(id, out var pool)) 
                return pool;

            pool = new ObjectPool<Flyweight>(
                settings.Create,
                settings.OnGet,
                settings.OnRelease,
                settings.OnDestroyPoolObject,
                settings.CollectionCheck,
                settings.DefaultCapacity,
                settings.MaxPoolSize
            );

            id = settings.GetInstanceID();
            _idDictionary.Add(settings, id);
            _poolDictionary.Add(id, pool);

            return pool;
        }

        public IObjectPool<Flyweight> GetPool(FlyweightSettings settings)
        {
            if (_idDictionary.TryGetValue(settings, out var id))
                return _poolDictionary[id];

            if (createIfNotExist)
                return CreatePool(settings);

            return null;
        }

        public Flyweight Get(FlyweightSettings settings)
        {
            var pool = GetPool(settings);
            return pool.Get();
        }

        public void Release(Flyweight flyweight)
        {
            var pool = GetPool(flyweight.Settings);
            pool.Release(flyweight);
        }
    }
}