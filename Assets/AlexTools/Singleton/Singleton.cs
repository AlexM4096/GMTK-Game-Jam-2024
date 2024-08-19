using UnityEngine;

namespace AlexTools.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Component 
    {
        private static readonly string Name = $"{typeof(T).Name} (Auto-Generated)";
        
        private static T _instance;

        public static bool HasInstance => _instance != null;
        public static T TryGetInstance() => HasInstance ? _instance : null;

        public static T Instance 
        {
            get 
            {
                if (HasInstance) return _instance;
                
                _instance = FindAnyObjectByType<T>();
                if (HasInstance) return _instance;
                
                var go = new GameObject(Name);
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

            Instance = this as T;
        }       
    }
}