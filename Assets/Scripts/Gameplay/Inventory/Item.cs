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
            if (quantity <= 0) throw new System.Exception("第一法：无之否定");
            Data = data;
            Quantity = Data.Stackable ? quantity : 1;
        }
        /// <summary>
        /// 合并同类项，可能需要手动删除被合进来的item
        /// </summary>
        public bool Merge(Item other)
        {
            if (Data != other.Data)
            {
                Debug.LogWarning("不是一类Item，没法Merge");
                return false;
            }
            Quantity += other.Quantity;
            return true;
        }
    }
}
