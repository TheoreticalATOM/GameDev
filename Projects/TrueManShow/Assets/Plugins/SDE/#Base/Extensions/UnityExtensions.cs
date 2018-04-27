using UnityEngine;

namespace SDE
{
    public static class UnityExtensions
    {
        public static float Range(this Vector2 v)
        {
            return Random.Range(v.x, v.y);
        }

        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            T comp = go.GetComponent<T>();
            if (!comp) comp = go.AddComponent<T>();
            return comp;
        }

        public static T AddComponent<T>(this GameObject go, System.Action<T> SetProperties) where T : Component
        {
            T comp = go.AddComponent<T>();
            SetProperties(comp);
            return comp;
        }
        

        public static T RandomValue<T>(this T[] array)
        {
            return array[Random.Range(0, array.Length)];
        }
        
    }
}