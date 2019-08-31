using System;
using System.Collections.Generic;
using UnityEngine;

namespace PoolAttendant
{
    public class PoolSettings : ScriptableObject
    {
        public List<DefaultPoolItem> items = new List<DefaultPoolItem>();
    }
    
    [Serializable]
    public class DefaultPoolItem
    {
        public GameObject prefab;
        public int size = 5;
    }
}