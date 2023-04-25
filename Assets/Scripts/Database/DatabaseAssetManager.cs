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
        /// <summary> �������ٲ�ѯ�ֵ�(key -> index) </summary>
        private void BuildQueryDictionary()
        {
            var propInfo = typeof(T).GetProperty(GetKeyName());
            if (propInfo == null)
            {
                Debug.LogError("DatabaseKey���Attribute��keyName����д���ˣ����ٲ�飿\nŶ�ԣ�Ҳ�п������key��field��Ŀǰֻ֧��property");
                return;
            }
            for (int i=0; i < Assets.Length; i++)
            {
                _queryDictionary.Add((string)propInfo.GetValue(Assets[i]), i);
            }
        }
        /// <summary> ����[DatabaseKey(keyName)]��ȡDatabaseAsset��keyName </summary>
        private string GetKeyName()
        {
            var attribute = typeof(T).GetCustomAttribute(typeof(DatabaseKey)) as DatabaseKey;   // ��ѧ�����ѡ���GetCustomAttribute����Ҫ��s!
            if (attribute == null)
            {
                Debug.LogWarning("û�ҵ�DatabaseKey����û���˸�DatabaseAsset��class���ϱ�ע��");
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
