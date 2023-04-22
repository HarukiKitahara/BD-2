using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;

namespace MyProject.Database
{
    public class DatabaseManager : MonoBehaviourSingletonBase<DatabaseManager>
    {
        public WorldTileDatabaseAsset[] worldTileDatabaseAssets;
        public WorldTileDatabaseAsset waterDatabaseAssets;
        //protected override void InitOnAwake()
        //{
        //    WorldTileDatabaseAssets = Resources.LoadAll<WorldTileDatabaseAsset>("DatabaseAsset/WorldTile");
        //}
    }
}
