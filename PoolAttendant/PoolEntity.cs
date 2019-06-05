using UnityEngine;

namespace PoolAttendant
{
    public class PoolEntity : MonoBehaviour
    {
        [SerializeField]
        private GameObject prefab;

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
    }
}