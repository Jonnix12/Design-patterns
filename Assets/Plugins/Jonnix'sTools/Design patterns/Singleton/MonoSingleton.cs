using UnityEngine;

namespace JonnixTools.DesignPatterns.Singleton
{
    public class MonoSingleton<T> : MonoBehaviour where T : Component
    {
        private static T _instance;
        public static T Instance => _instance;

        public virtual void Awake() {

            if (isActiveAndEnabled)
            {
                if (Instance == null)
                {
                    _instance = this as T;
                    DontDestroyOnLoad(_instance);
                }
                else if (Instance != this as T)
                    Destroy(this);
            }
        }
    }
}