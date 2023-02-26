using UnityEngine;
using Utility.Extensions;

namespace Patterns.Design.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (!IsInitialized && searchForInstance)
                {
                    searchForInstance = false;
                    T[] objects = FindObjectsOfType<T>();
                    
                    if (objects.Length > 1)
                        Debug.LogError($"Expected exactly 1 {typeof(T).Name} but found {objects.Length}.");

                    if (objects.Length == 0)
                        Instantiate(new GameObject()).AddComponent<T>();

                    if (objects.Length == 1)
                    {
                        instance = objects[0];
                        DontDestroyOnLoad(instance.gameObject.GetParentRoot());
                    }
                }
                return instance;
            }
        }
        public static bool IsInitialized => instance != null;

        private static bool searchForInstance = true;

        public static void AssertIsInitialized() =>
            Debug.Assert(IsInitialized, string.Format("The {0} singleton has not been initialized.", typeof(T).Name));

        protected virtual void Awake()
        {
            if (IsInitialized && instance != this)
            {
                if (Application.isEditor)
                    DestroyImmediate(this);
                else
                    Destroy(this);

                Debug.LogWarning($"Trying to instantiate a second instance of singleton class {GetType().Name}. Additional Instance was destroyed");
            }
            else if (!IsInitialized)
            {
                instance = (T)this;
                searchForInstance = false;
                DontDestroyOnLoad(gameObject?.GetParentRoot());
            }
        }

        protected virtual void OnDestroy()
        {
            if (instance == this)
            {
                instance = null;
                searchForInstance = true;
            }
        }
    }
}
