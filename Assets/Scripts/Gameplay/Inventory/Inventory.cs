using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyProject.Database;
namespace MyProject.Gameplay
{
    /// <summary>
    /// ������ʵ��
    /// </summary>
    public class Inventory
    {
        public float Capacity { get; private set; }
        public readonly List<Item> items = new();
        //private readonly Dictionary<int, Item> unstackableItems = new();
        public Inventory(float capacity)
        {
            Capacity = capacity;
        }
        /// <summary>
        /// ����item�������ɺϲ���merge
        /// </summary>
        /// <param name="item"></param>
        public void Store(Item item)
        {
            if (items.Contains(item))
            {
                Debug.LogWarning("�����Ҵ����Լ�");
                return;
            }
            if (item.Data.Stackable)
            {
                var itemInInventory = GetItemByData(item.Data);
                if (itemInInventory == null)
                {
                    items.Add(item);
                }
                else
                {
                    itemInInventory.Merge(item);
                }
            }
            else
            {
                items.Add(item);    // ���ɶѵ���ֱ�Ӽӽ���
            }
        }
        public void Retrive()
        {

        }

        private Item GetItemByData(ItemDatabaseAsset data)
        {
            return items.Find(o => o.Data == data);
        }

        public string PrintAllItems()
        {
            string s = "";
            items.ForEach(o => s += $"{o.Data.name}: {o.Quantity}, ");
            return s;
        }
    }
}
