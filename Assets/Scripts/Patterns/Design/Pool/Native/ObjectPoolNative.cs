using UnityEngine;
using UnityEngine.Pool;
using Utility.Extensions;

namespace Patterns.Design.Pool.Native
{
    /// <summary>
    /// Unity has a native object pooling system
    /// https://docs.unity3d.com/ScriptReference/Pool.IObjectPool_1.html
    /// </summary>
    /// <typeparam name="T">The component of object</typeparam>
    public class ObjectPoolNative<T> : MonoBehaviour where T : Component
    {
        /// <summary>
        /// The component prefab we instantiate
        /// If is null, instance new GameObject with <c>T</c> component
        /// </summary>
        public T ItemPrefab;

        /// <summary>
        /// The parent to spawn object
        /// </summary>
        public Transform ItemParent;

        public virtual int CountAll => allItems.CountAll;
        public virtual int CountActive => allItems.CountActive;
        public virtual int CountInactive => allItems.CountInactive;

        /// <summary>
        /// Unity's native object pool
        /// </summary>
        protected ObjectPool<T> allItems;

        /// <summary>
        /// Collection checks will throw errors if we try to release an item that is already in the pool
        /// </summary>
        protected bool collectionCheck = true;
        protected int defaultCapacity = 10;
        protected int maxSize = 10000;

        protected virtual void Start()
        {
            if (ItemPrefab == null)
                ItemPrefab = new GameObject().AddComponent<T>();

            if (ItemParent == null)
                ItemParent = this.transform;

            CreateObjectPool();
        }

        protected virtual void CreateObjectPool()
            => allItems = new ObjectPool<T>(CreatePolledItem,
                                            OnTakeFromPool,
                                            OnReturnedToPool,
                                            OnDestroyObject,
                                            collectionCheck,
                                            defaultCapacity,
                                            maxSize);
        /// <summary>
        /// Add new item to the pool
        /// </summary>
        /// <returns>The component of new object</returns>
        protected virtual T CreatePolledItem()
        {
            var newObject = Instantiate(ItemPrefab.gameObject, ItemParent);
            var item = newObject.GetComponent<T>();
            if (item == null)
                item = newObject.AddComponent<T>();

            return item;
        }

        /// <summary>
        /// Called when an item is taken from the pool using pool.Get()
        /// </summary>
        /// <param name="item">item of pool</param>
        protected virtual void OnTakeFromPool(T item)
            => item.TrySetGameObjectActive(true);

        /// <summary>
        /// Called when an item is returned to the pool using pool.Release()
        /// </summary>
        /// <param name="item">item of pool</param>
        protected virtual void OnReturnedToPool(T item)
            => item.TrySetGameObjectActive(false);

        /// <summary>
        /// If the pool capacity is reached then any items returned will be destroyed
        /// </summary>
        /// <param name="item">item to destroy</param>
        protected virtual void OnDestroyObject(T item)
            => item.TryDestroyGameObject();

        public virtual void Clear() => allItems?.Clear();
        public virtual void Dispose() => allItems?.Dispose();
        public virtual T Get() => allItems?.Get();
        public virtual PooledObject<T> Get(out T item) => allItems.Get(out item);
        public virtual void Release(T item) => allItems?.Release(item);

        /// <summary>
        /// Get an instance from the pool.
        /// If the pool is empty then a new instance will be created
        /// </summary>
        /// <returns>Unity Game Object</returns>
        public virtual GameObject GetItemFromPool()
            => allItems.Get().gameObject;
    }
}