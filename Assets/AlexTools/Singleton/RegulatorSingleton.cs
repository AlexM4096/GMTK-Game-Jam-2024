using UnityEngine;

namespace AlexTools.Singleton
{
    public class RegulatorSingleton<T> : MonoBehaviour where T : RegulatorSingleton<T>
    {
        private static readonly string Name = $"{typeof(T).Name} (Auto-Generated)";
        
        private static T _instance;

        public static bool HasInstance => _instance != null;

        public float InitializationTime { get; private set; }

        public static T Instance 
        {
            get 
            {
                if (HasInstance) return _instance;
                
                _instance = FindAnyObjectByType<T>();
                if (HasInstance) return _instance;
                
                var go = new GameObject(Name);
                go.hideFlags = HideFlags.HideAndDontSave;
                _instance = go.AddComponent<T>();

                return _instance;
            }
            set => _instance = value;
        }

        protected virtual void Awake() 
        {
            InitializeSingleton();
        }

        protected virtual void InitializeSingleton() 
        {
            if (!Application.isPlaying) return;
            
            InitializationTime = Time.time;
            DontDestroyOnLoad(gameObject);

            var oldInstances = FindObjectsByType<RegulatorSingleton<T>>(FindObjectsSortMode.None);
            foreach (var oldInstance in oldInstances) 
            {
                if (oldInstance.InitializationTime < InitializationTime)
                    Destroy(oldInstance.gameObject);
            }

            if (!HasInstance) 
                Instance = this as T;
        }
    }
}