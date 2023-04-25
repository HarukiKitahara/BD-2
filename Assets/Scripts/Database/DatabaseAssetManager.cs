using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Reflection;
namespace MyProject.Database
{
    public class DatabaseAssetManager<T> where T: DatabaseAssetBase
    {
        public T[] Assets { get; private set; }
        private readonly Dictionary<string, int> _queryDictionary = new();

        public DatabaseAssetManager()
        {
            Assets = Resources.LoadAll<T>("DatabaseAsset");
            BuildQueryDictionary();
        }
        /// <summary> 建立快速查询字典(key -> index) </summary>
        private void BuildQueryDictionary()
        {
            var propInfo = typeof(T).GetProperty(GetKeyName());
            if (propInfo == null)
            {
                Debug.LogError("DatabaseKey这个Attribute的keyName可能写错了，你再查查？\n哦对，也有可能这个key是field，目前只支持property");
                return;
            }
            for (int i=0; i < Assets.Length; i++)
            {
                _queryDictionary.Add((string)propInfo.GetValue(Assets[i]), i);
            }
        }
        /// <summary> 根据[DatabaseKey(keyName)]获取DatabaseAsset的keyName </summary>
        private string GetKeyName()
        {
            var attribute = typeof(T).GetCustomAttribute(typeof(DatabaseKey)) as DatabaseKey;   // 【学到虚脱】用GetCustomAttribute，不要加s!
            if (attribute == null)
            {
                Debug.LogWarning("没找到DatabaseKey，你没忘了给DatabaseAsset的class加上标注吧");
                return default;
            }     
            return attribute.keyName;
        }
        public bool TryGetIndexByKey(string key, out int index)
        {
            return _queryDictionary.TryGetValue(key, out index);
        }
        public bool TryGetAssetByKey(string key, out T asset)
        {
            if (TryGetIndexByKey(key, out int index))
            {
                asset = Assets[index];
                return true;
            }
            else
            {
                asset = null;
                return false;
            }
        }
    }
}
