using UnityEngine;

namespace PoolAttendant
{
    public static class PoolAttendantExtention
    {
        public static GameObject GetPooledInstance(this MonoBehaviour mono)
        {
            return mono.gameObject.GetPooledInstance();
        }

        public static GameObject GetPooledInstance(this MonoBehaviour mono, Vector3 position)
        {
            return mono.gameObject.GetPooledInstance(position, Quaternion.identity);
        }
        
        public static GameObject GetPooledInstance(this MonoBehaviour mono, Vector3 position, Quaternion rotation)
        {
            return mono.gameObject.GetPooledInstance(position, rotation, Vector3.one);
        }
        
        public static GameObject GetPooledInstance(this MonoBehaviour mono, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return Pool.Instance.Get(mono.gameObject, position, rotation, scale);
        }
        
        public static T GetPooledInstance<T>(this MonoBehaviour mono) where T : Component
        {
            return mono.gameObject.GetPooledInstance<T>(Vector3.zero);
        }
        
        public static T GetPooledInstance<T>(this MonoBehaviour mono, Vector3 position) where T : Component
        {
            return mono.gameObject.GetPooledInstance<T>(Vector3.zero, Quaternion.identity);
        }
        
        public static T GetPooledInstance<T>(this MonoBehaviour mono, Vector3 position, Quaternion rotation) where T : Component
        {
            return mono.gameObject.GetPooledInstance<T>(Vector3.zero, Quaternion.identity, Vector3.one);
        }
        
        public static T GetPooledInstance<T>(this MonoBehaviour mono, Vector3 position, Quaternion rotation, Vector3 scale) where T : Component
        {
            return Pool.Instance.Get<T>(mono.gameObject, position, rotation, scale);
        }        
        
        public static GameObject GetPooledInstance(this GameObject prefab)
        {
            return prefab.GetPooledInstance(Vector3.zero);
        }
        
        public static GameObject GetPooledInstance(this GameObject prefab, Vector3 position)
        {
            return prefab.GetPooledInstance(position, Quaternion.identity);
        }
        
        public static GameObject GetPooledInstance(this GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return prefab.GetPooledInstance(position, rotation, Vector3.one);
        }
        
        public static GameObject GetPooledInstance(this GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            return Pool.Instance.Get(prefab, position, rotation, scale);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab) where T : Component
        {
            return prefab.GetPooledInstance<T>(Vector3.zero);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab, Vector3 position) where T : Component
        {
            return prefab.GetPooledInstance<T>(Vector3.zero, Quaternion.identity);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab, Vector3 position, Quaternion rotation) where T : Component
        {
            return prefab.GetPooledInstance<T>(Vector3.zero, Quaternion.identity, Vector3.one);
        }
        
        public static T GetPooledInstance<T>(this GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : Component
        {
            return Pool.Instance.Get<T>(prefab, position, rotation, scale);
        }
    }
}