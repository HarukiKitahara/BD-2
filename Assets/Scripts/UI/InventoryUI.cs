using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Gameplay;
using TMPro;
namespace MyProject.UI
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private GameObject _itemGroupGO;
        [SerializeField] private GameObject _itemGO;
        [SerializeField] private InventoryEntity _inventoryEntity;
        private Inventory _inventory;
        private readonly Dictionary<Item, GameObject> _itemGOs = new();

        private void Start()
        {
            Bind(_inventoryEntity.Inventory);
        }

        public void Bind(Inventory inventory)
        {
            _inventory = inventory;
            inventory.OnItemStored += UpdateItem;
            UpdateInventory();
        }

        private void UpdateInventory()
        {
            _inventory.items.ForEach(o => UpdateItem(o));
        }
        private void UpdateItem(Item item)
        {
            if (_itemGOs.ContainsKey(item))
            {
                UpdateItemNumber(item);
                return;
            }
            AddItem(item);
        }
        private void UpdateItemNumber(Item item)
        {
            _itemGOs[item].transform.Find("Number").GetComponent<TMP_Text>().text = item.Quantity.ToString();
        }
        private void AddItem(Item item)
        {
            var itemGO = Instantiate(_itemGO, _itemGroupGO.transform);
            itemGO.SetActive(true);
            _itemGOs.Add(item, itemGO);
            _itemGOs[item].transform.Find("Name").GetComponent<TMP_Text>().text = item.Data.Name;
            UpdateItemNumber(item);
        }
    }
}
