using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;
using Component = UnityEngine.Component;

namespace PoolAttendant
{
    public class Pool
    {
        private static Pool instance;
        
        private const string PoolName = "Pool";
        private const string SettingsLoadPath = "PoolSettings";
        private const string MenuItem = "Tools/Pool/Create PoolSettings";
        
        private Transform container;
        private PoolSettings settings;
        
        private readonly Dictionary<int, List<GameObject>> items = new Dictionary<int, List<GameObject>>();
        private readonly Dictionary<int, Transform> parents = new Dictionary<int, Transform>();

        public static GameObject Initialize()
        {
            return Instance.container.gameObject;
        }

        public static Pool Instance {
            get {
                if (instance != null) {
                    return instance;
                }

                instance = new Pool {
                    container = new GameObject(PoolName).transform,
                    settings = Resources.Load<PoolSettings>(SettingsLoadPath)
                };

                instance.container.position = new Vector3(0,0,0);

                if (instance.settings == null) {
                    
                    Debug.LogWarning(
                        "PoolSettings not found at path \"Resources\\PoolSettings\", " +
                        "if you want to populate the pool with initial values you can create the" +
                        "settings with [\"Tools\\Pool\\Create PoolSettings\"]");
                }
                
                instance.CreateDefaultItems();
                
                return instance;
            }
        }

        private void CreateDefaultItems()
        {
            foreach (DefaultPoolItem item in settings.items) {
                InstantiateDefaultItems(item);
            }
            
            SceneManager.activeSceneChanged += DeactivateAllObjects;
        }

        private void InstantiateDefaultItems(DefaultPoolItem item)
        {
            if (item == null) {
                return;
            }

            if (item.prefab == null) {
                return;
            }
            
            parents.Add(item.prefab.GetInstanceID(), CreateParent(item.prefab.name));

            InstantiateListAndGetFirst(item.prefab, item.size);
        }

        private void DeactivateAllObjects(Scene currentScene, Scene newScene)
        {
            foreach (List<GameObject> objects in items.Values) {
                foreach (GameObject obj in objects) {
                    if (obj != null) {
                        obj.SetActive(false);
                    }
                }
            }
        }
        
        private GameObject InstantiateListAndGetFirst(GameObject prefab, int size = 1)
        {
            List<GameObject> list = new List<GameObject>();
            
            for (int i = 0; i < size; i++) {
                list.Add(CreateNew(prefab));
            }
            
            items.Add(prefab.GetInstanceID(), list);

            return list.FirstOrDefault();
        }

        private GameObject CreateNew(GameObject prefab)
        {
            int id = prefab.GetInstanceID();

            if (!parents.TryGetValue(id, out Transform parent)) {
                parent = CreateParent(prefab.name);
                parents.Add(prefab.GetInstanceID(), parent);
            }
            
            GameObject obj = Object.Instantiate(
                prefab, 
                prefab.transform.position,
                prefab.transform.rotation,
                parent);
            
            obj.AddComponent<PoolEntity>().SetPrefab(prefab);

            obj.SetActive(false);
            return obj;
        }

        private Transform CreateParent(string name)
        {
            Transform childContainer = new GameObject(name + string.Empty + PoolName).transform;
            childContainer.transform.SetParent(container);

            return childContainer;
        }

        public GameObject Get(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            GameObject obj;
            
            if (items.TryGetValue(prefab.GetInstanceID(), out List<GameObject> list)) {
                obj = list.Find(o => !o.activeInHierarchy);

                if (obj == null) {
                    obj = CreateNew(prefab);
                    
                    list.Add(obj);
                }
                
            } else {
                obj = InstantiateListAndGetFirst(prefab);
            }
            
            obj.transform.position = position;
            obj.transform.rotation = rotation;
            obj.transform.localScale = scale;
            
            obj.SetActive(true);

            return obj;
        }

        public T Get<T>(GameObject prefab, Vector3 position, Quaternion rotation, Vector3 scale) where T : Component
        {
            return Get(prefab, position, rotation, scale).GetComponent<T>();
        }

        public void Reparent(GameObject obj, int instanceId)
        {
            if (!parents.TryGetValue(instanceId, out Transform parentTransform)) {
                return;
            }

            if (obj.transform.parent.Equals(parentTransform)) {
                return;
            }
                
            obj.transform.SetParent(parentTransform, true);
        }

        public void Remove(GameObject gameObject, int instanceId)
        {
            if (!items.TryGetValue(instanceId, out List<GameObject> list)) {
                return;
            }

            list.Remove(gameObject);
        }
        
        #region Editor
#if UNITY_EDITOR
        [MenuItem(MenuItem)]
        public static void CreatePoolSettings()
        {
            PoolSettings settings = Resources.Load<PoolSettings>(SettingsLoadPath);

            if (settings != null) {
                Debug.LogWarning("PoolSettings already exists at Path \"Resources\\PoolSettings\"");
                return;
            }

            if(!AssetDatabase.IsValidFolder("Assets\\Resources")) {
                AssetDatabase.CreateFolder("Assets", "Resources");
            }

            settings = ScriptableObject.CreateInstance<PoolSettings>();
            string path = AssetDatabase
                .GenerateUniqueAssetPath (
                    $"Assets\\Resources\\{SettingsLoadPath}.asset");
            
            AssetDatabase.CreateAsset(settings, path);
            AssetDatabase.SaveAssets();
        }
#endif
        #endregion

    }
}