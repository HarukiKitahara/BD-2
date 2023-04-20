using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Database
{
    [CreateAssetMenu(fileName = "DatabaseAsset_WorldTile_Default", menuName = "MyDatabase/WorldTile")]
    public class WorldTileDatabaseAsset : ScriptableObject
    {
        //[field: SerializeField] public ItemDatabaseAsset[] assets { get; private set; }
        // Voxeldata
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public float SpawnWeight { get; private set; }
    }
}
