using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
using System.Linq;
namespace MyProject.Database
{
    /// <summary>
    /// 提供所有Database的全局访问点
    /// TODO: 重构为可拓展、可查询的“类的组合”。
    /// </summary>
    public class DatabaseManager : MonoBehaviourSingletonBase<DatabaseManager>
    {
        public DatabaseAssetManager<VoxelDatabaseAsset> Voxels { get; private set; }
        public DatabaseAssetManager<ItemDatabaseAsset> Items { get; private set; }
        /// <summary> 加载所有DatabaseAsset类的Manager </summary>
        protected override void InitOnAwake()
        {
            Voxels = new DatabaseAssetManager<VoxelDatabaseAsset>();
        }
        //public Dictionary<int, WorldTileDatabaseAsset> worldTileDic = new();
        //protected override void InitOnAwake()
        //{
        //    //WorldTileDatabaseAssets = Resources.LoadAll<WorldTileDatabaseAsset>("DatabaseAsset/WorldTile");

        //    foreach(int i)
        //    worldTileDic
        //}
        //public int GetIDByAsset(WorldTileDatabaseAsset asset)
        //{
        //    if (!worldTileDatabaseAssets.Contains(asset)) return -1;
        //}
    }
}
