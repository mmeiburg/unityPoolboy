using UnityEngine;

namespace TinyTools.PoolAttendant
{
    public static class PoolAttendantExtention
    {
        // GameObject Extention
        
        public static GameObject GetPooledInstance(this GameObject prefab, bool inactive = false)
        {
            return prefab.GetPooledInstance(Vector3.zero, inactive);
        }
        
        public static GameObject GetPooledInstance(this GameObject prefab, Vector3 position, bool inactive = false)
        {
            return prefab.GetPooledInstance(position, prefab.transform.rotation, inactive);
        }
        
        public static GameObject GetPooledInstance(this GameObject prefab, Vector3 position, Quaternion rotation, bool inactive = false)
        {
            return prefab.GetPooledInstance(position, rotation, prefab.transform.localScale, inactive);
        }
        
        public static GameObject GetPooledInstance(this GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, bool inactive = false)
        {
            return Pool.Instance.Get(prefab, position, rotation, scale, inactive);
        }
        
        // GameObject Extention
        
        public static T GetPooledInstance<T>(this GameObject prefab, bool inactive = false) where T : Component
        {
            return prefab.GetPooledInstance<T>(Vector3.zero, inactive);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab, Vector3 position, bool inactive = false) where T : Component
        {
            return prefab.GetPooledInstance<T>(position, prefab.transform.rotation, inactive);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab, Vector3 position, Quaternion rotation, bool inactive = false) where T : Component
        {
            return prefab.GetPooledInstance<T>(position, rotation, prefab.transform.localScale, inactive);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale, bool inactive = false) where T : Component
        {
            return Pool.Instance.Get<T>(prefab, position, rotation, scale, inactive);
        }
        
        // GameObject Generic Extention
        
        public static T GetPooledInstance<T>(this T prefab, bool inactive = false) where T : Component
        {
            return prefab.GetPooledInstance(Vector3.zero, inactive);
        }
        
        public static T GetPooledInstance<T>(this T prefab, Vector3 position, bool inactive = false) where T : Component
        {
            return prefab.GetPooledInstance(position, prefab.transform.rotation, inactive);
        }
        
        public static T GetPooledInstance<T>(this T prefab, Vector3 position, Quaternion rotation, bool inactive = false) where T : Component
        {
            return prefab.GetPooledInstance<T>(position, rotation, prefab.transform.localScale, inactive);
        }
        
        public static T GetPooledInstance<T>(this T prefab, Vector3 position, Quaternion rotation, Vector3 scale, bool inactive = false) where T : Component
        {
            return Pool.Instance.Get<T>(prefab.gameObject, position, rotation, scale, inactive);
        }
    }
}