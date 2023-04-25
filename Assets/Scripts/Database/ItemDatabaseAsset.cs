using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Database
{
    [CreateAssetMenu(fileName = "ItemDatabaseAsset_DEFAULT", menuName = "MyDatabase/Item")]
    [DatabaseKey("ID")]
    public class ItemDatabaseAsset : DatabaseAssetBase
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public float Mass { get; private set; }
        [field: SerializeField] public float Volume { get; private set; }
        [field: SerializeField] public bool Stackable { get; private set; }
    }
}
