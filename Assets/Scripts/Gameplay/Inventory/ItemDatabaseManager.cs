using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Gameplay
{
    [CreateAssetMenu(fileName = "ItemDatabaseManager", menuName = "MyDatabase/Managers")]
    public class ItemDatabaseManager : ScriptableObject
    {
        [field: SerializeField] public ItemDatabaseAsset[] assets { get; private set; }
    }
}
