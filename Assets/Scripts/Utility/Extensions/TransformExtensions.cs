using System.Collections;
using UnityEngine;

namespace Utility.Extensions
{
    public static class TransformExtensions
    {
        public static void Move(this Transform transform, MonoBehaviour monoBehaviour, Vector3 source, Vector3 dest, float moveTime = 1f)
        {
            monoBehaviour.StartCoroutine(MoveTransform(transform, source, dest, moveTime));
        }

        public static IEnumerator Move(this Transform transform, Vector3 start, Vector3 end, float animTime = 1f)
        {
            yield return MoveTransform(transform, start, end, animTime);
        }

        public static IEnumerator Scale(this Transform transform, Vector3 start, Vector3 end, float animTime = 1f)
        {
            yield return ScaleTransform(transform, start, end, animTime);
        }

        private static IEnumerator MoveTransform(Transform target, Vector3 start, Vector3 end, float animTime = 1f)
        {
            float t = 0f;
            while (t <= 1f)
            {
                target.localPosition = Vector3.Lerp(start, end, t);
                t += Time.deltaTime / animTime;
                if (t > 1f && target.localPosition != end) t = 1f;
                yield return null;
            }
        }

        private static IEnumerator ScaleTransform(Transform target, Vector3 start, Vector3 end, float animTime = 1f)
        {
            float t = 0f;
            while (t <= 1f)
            {
                target.localScale = Vector3.Lerp(start, end, t);
                t += Time.deltaTime / animTime;
                if (t > 1f && target.localScale != end) t = 1f;
                yield return null;
            }
        }
    }
}
