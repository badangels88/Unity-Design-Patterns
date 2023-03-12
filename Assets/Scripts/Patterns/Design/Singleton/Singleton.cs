using UnityEngine;
using Utility.Extensions;

namespace Patterns.Design.Singleton
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        /// <summary>
        /// Instance variable <c>instance</c> store instance of this MonoBehaviour.
        /// </summary>
        private static T instance;

        /// <summary>
        /// Instance variable <c>searchForInstance</c> causes a scene search to find the <c>T</c> instance.
        /// </summary>
        private static bool searchForInstance = true;

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
        public static bool IsInitialized => instance != null;

        /// <summary>
        /// 
        /// </summary>
        public static void AssertIsInitialized() =>
            Debug.Assert(IsInitialized, string.Format("The {0} singleton has not been initialized.", typeof(T).Name));

        /// <summary>
        /// 
        /// </summary>
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

        /// <summary>
        /// 
        /// </summary>
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
