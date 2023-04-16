using UnityEngine;

namespace Utility.Extensions
{
    public static class ComponentExtensions
    {
        public static void TrySetGameObjectActive(this Component component, bool active)
        {
            if (component != null)
                component.gameObject.TrySetActive(active);
        }

        public static void TryDestroyGameObject(this Component component)
        {
            if (component != null && component.gameObject != null)
                Object.Destroy(component.gameObject);
        }
    }
}