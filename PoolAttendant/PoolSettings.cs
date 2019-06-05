using System;
using UnityEngine;

namespace PoolAttendant
{
    public class PoolSettings : ScriptableObject
    {
        public DefaultPoolItem[] items;
    }
    
    [Serializable]
    public class DefaultPoolItem
    {
        public GameObject prefab;
        public int size = 5;
    }
}