using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyProject.Database;
namespace MyProject.World
{
    [System.Serializable]
    public class WorldManagerDataPersistance
    {
        public int playerTileIndex;
        public WorldDataPersistance worldDataPersistance;
        public WorldManagerDataPersistance(WorldDataPersistance worldDataPersistance, int playerTileIndex)
        {
            this.playerTileIndex = playerTileIndex;
            this.worldDataPersistance = worldDataPersistance;
        }
    }
    /// <summary>
    /// 控制生成世界、渲染世界（没有具体逻辑，主要是调用管理其他类）
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        public World World { get; private set; }
        public WorldMeshData WorldMeshData { get; private set; }
        private VoxelMeshRenderer _voxelMeshRenderer;
        [SerializeField]
        private WorldGenerationDatabaseAsset _worldGenerationDatabaseAsset;
        [SerializeField]
        private GameObject _playerCharacterPrefab;
        private int _playerTileIndex;
        private GameObject _playerCharacterGO;
        private void Start()
        {
            _voxelMeshRenderer = GetComponent<VoxelMeshRenderer>();
        }
        /// <summary> 计算Mesh、提交渲染一条龙服务 </summary>
        private void RenderWorld()
        {
            WorldMeshData = WorldMeshGenerator.GenerateWorldMesh(World);
            _voxelMeshRenderer.RenderWorldMesh(WorldMeshData);
        }
        /// <summary> 随机新世界，并且刷新角色 </summary>
        public void NewWorld()
        {
            World = WorldGenerator.GenerateWorld(_worldGenerationDatabaseAsset);
            _playerTileIndex = World.GetRandomTileIndexAboveSeaLevel();
            SpawnCharacterAtTileIndex(_playerTileIndex);
            RenderWorld();
        }
        public void LoadWorld(WorldManagerDataPersistance worldManagerDataPersistace)
        {
            World = new World(worldManagerDataPersistace.worldDataPersistance);
            LoadCharacter(worldManagerDataPersistace.playerTileIndex);
            RenderWorld();
        }
        public WorldManagerDataPersistance SaveWorld()
        {
            return new WorldManagerDataPersistance(World.DoDataPersistance(), _playerTileIndex);
        }
        /// <summary> 在指定tileIndex刷新角色 </summary>
        private void SpawnCharacterAtTileIndex(int tileIndex)
        {
            Destroy(_playerCharacterGO);    // 先杀掉已有的角色，防止刷重了
            _playerCharacterGO = Instantiate(_playerCharacterPrefab,
                World.GetTileGroundCenterPositionByIndex(tileIndex), 
                Quaternion.identity);
        }
        private void LoadCharacter(int playerTileIndex)
        {
            SpawnCharacterAtTileIndex(playerTileIndex);
        }
    }
}

