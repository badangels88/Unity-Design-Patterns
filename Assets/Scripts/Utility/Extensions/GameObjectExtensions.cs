using UnityEngine;

namespace Utility.Extensions
{
    public static class GameObjectExtensions
    {
        public static void TrySetActive(this GameObject gameObject, bool value)
        {
            if (gameObject != null) gameObject.SetActive(value);
        }

        public static void TryDestroy(this GameObject gameObject)
        {
            if (gameObject != null) Object.Destroy(gameObject);
        }

        public static string GetHierarchyPath(this GameObject gameObject)
        {
            var path = string.Empty;

            if (gameObject != null)
            {
                path = "/" + gameObject.name;
                while (gameObject.transform.parent != null)
                {
                    gameObject = gameObject.transform.parent.gameObject;
                    path = "/" + gameObject.name + path;
                }
            }

            return path;
        }

        public static GameObject GetParentRoot(this GameObject child) =>
            child.transform.parent == null ? child : GetParentRoot(child.transform.parent.gameObject);
    }
}