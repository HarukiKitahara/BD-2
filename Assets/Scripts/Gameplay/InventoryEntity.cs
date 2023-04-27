using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    public class InventoryEntity : MonoBehaviour
    {
        public Inventory Inventory { get; private set; }
        private void Awake()
        {
            Inventory = new Inventory(500);
        }
        private void OnTriggerEnter(Collider other)
        {
            var itemEntity = other.gameObject.GetComponent<ItemEntity>();
            if (itemEntity != null)
            {
                Inventory.Store(itemEntity.Item);
                Destroy(other.gameObject);
            }
        }
    }
}
