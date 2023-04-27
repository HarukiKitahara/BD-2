using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
namespace MyProject.Gameplay
{
    public class ItemEntity : MonoBehaviour
    {
        public Item Item { get; private set; }
        [SerializeField] private ItemDatabaseAsset _data;
        private void Start()
        {
            Item = new Item(_data, 1);
        }
    }
}
