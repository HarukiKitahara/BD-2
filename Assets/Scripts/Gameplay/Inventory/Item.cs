using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class Item
    {
        public ItemDatabaseAsset Data { get; private set; }
        public int Quantity { get; private set; }
        public Item(ItemDatabaseAsset data, int quantity)
        {
            if (quantity <= 0) throw new System.Exception("��һ������֮��");
            Data = data;
            Quantity = Data.Stackable ? quantity : 1;
        }
        /// <summary>
        /// �ϲ�ͬ���������Ҫ�ֶ�ɾ�����Ͻ�����item
        /// </summary>
        public bool Merge(Item other)
        {
            if (Data != other.Data)
            {
                Debug.LogWarning("����һ��Item��û��Merge");
                return false;
            }
            Quantity += other.Quantity;
            return true;
        }
    }
}
