using UnityEngine;

namespace PoolAttendant
{
    public class PoolEntity : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab = null;

        private void OnEnable()
        {
            if (prefab == null) {
                return;
            }
            
            Pool.Instance.Reparent(gameObject, prefab.GetInstanceID());
        }

        public void SetPrefab(GameObject prefab)
        {
            this.prefab = prefab;
        }
        
        private void OnDestroy()
        {
            Pool.Instance.Remove(gameObject, prefab.GetInstanceID());
        }
    }
}