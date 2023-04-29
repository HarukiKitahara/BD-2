using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Core;
using System.Linq;
namespace MyProject.Database
{
    /// <summary>
    /// �ṩ����Database��ȫ�ַ��ʵ�
    /// TODO: �ع�Ϊ����չ���ɲ�ѯ�ġ������ϡ���
    /// </summary>
    public class DatabaseManager : MonoBehaviourSingletonBase<DatabaseManager>
    {
        public DatabaseAssetManager<VoxelDatabaseAsset> Voxels { get; private set; }
        public DatabaseAssetManager<ItemDatabaseAsset> Items { get; private set; }
        /// <summary> ��������DatabaseAsset���Manager </summary>
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
