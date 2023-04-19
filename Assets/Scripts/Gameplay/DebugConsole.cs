using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class DebugConsole : MonoBehaviour
    {
        [SerializeField]
        private ItemDatabaseManager _itemDatabaseManager;
        private void Start()
        {
            TestInventory();
        }
        public void TestInventory()
        {
            var item1 = new Item(_itemDatabaseManager.assets[0], 10);
            var item2 = new Item(_itemDatabaseManager.assets[1], 1);

            var inventory = new Inventory(10000);

            inventory.Store(item1);
            inventory.Store(item2);

            Debug.Log(inventory.PrintAllItems());
        }
    }
}
