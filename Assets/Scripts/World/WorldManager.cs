using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyProject.Database;
using System;
using MyProject.Core;
using MyProject.VoxelEngine;
namespace MyProject.World
{
    #region SaveLoad
    [Serializable]
    public class WorldManagerSaveData
    {
        public WorldCharacterControllerSaveData worldCharacterSaveData;
        public WorldSaveData worldSaveData;
        public WorldManagerSaveData(WorldSaveData worldSaveData, WorldCharacterControllerSaveData worldCharacterSaveData)
        {
            this.worldSaveData = worldSaveData;
            this.worldCharacterSaveData = worldCharacterSaveData;
        }
    }
    #endregion
    /// <summary> 控制生成世界、渲染世界（没有具体逻辑，主要是调用管理其他类） </summary>
    public class WorldManager : MonoBehaviour
    {
        public World World { get; private set; }
        [SerializeField]
        private MeshFilter _meshFilter;
        [SerializeField]
        private MeshCollider _meshCollider;
        [SerializeField]
        private WorldGenerationDatabaseAsset _worldGenerationDatabaseAsset;
        [SerializeField]
        private GameObject _playerCharacterGO;
        [SerializeField]
        private GameObject _SelectionHintGO;
        private WorldCharacterController _playerCharacter;
        public int SelectedTileIndex { get; private set; }
        // 一定不可能有多个world在发这种事件，所以一定是static
        public static event Action OnTileSelected;
        public static event Action OnTileInteracted;
        public static event Action OnEnterSite;
        /// <summary> 计算Mesh、提交渲染一条龙服务 </summary>
        private void RenderWorld()
        {
            var meshDatas = VoxelSpaceMeshGenerator.GenerateMesh(World);
            var meshs = VoxelMeshHelper.BuildMeshs(meshDatas);
            _meshFilter.mesh = VoxelMeshHelper.MergeMesh(false, meshs);
            _meshCollider.sharedMesh = meshs[0];        // 只有地面需要collider
        }
        /// <summary> 随机新世界，并且刷新角色 </summary>
        public void NewWorld()
        {
            World = WorldGenerator.GenerateWorld(_worldGenerationDatabaseAsset);
            var playerTileIndex = World.GetRandomTileIndexAboveSeaLevel();
            SpawnCharacterAtTileIndex(playerTileIndex);
            RenderWorld();
        }
        public void LoadWorld(WorldManagerSaveData worldManagerDataPersistace)
        {
            World = new World(worldManagerDataPersistace.worldSaveData);
            LoadCharacter(worldManagerDataPersistace.worldCharacterSaveData);
            RenderWorld();
        }
        public WorldManagerSaveData SaveWorld()
        {
            return new WorldManagerSaveData(World.GetSaveData(), _playerCharacter.DoDataPersistance());
        }
        /// <summary> 在指定tileIndex刷新角色（非读档） </summary>
        private void SpawnCharacterAtTileIndex(int tileIndex)
        {
            _playerCharacterGO.SetActive(true);
            _playerCharacter = new(World, _playerCharacterGO.transform, tileIndex);
        }
        private void LoadCharacter(WorldCharacterControllerSaveData dataPersistance)
        {
            _playerCharacterGO.SetActive(true);
            _playerCharacter = new(World, _playerCharacterGO.transform, dataPersistance);
        }
        /// <summary> 玩家数选中某个Tile </summary>
        public void SelectTile(int index)
        {
            if (index == SelectedTileIndex) return;
            _SelectionHintGO.transform.position = World.GetVoxelOriginPositionByIndex(index);
            SelectedTileIndex = index;
            OnTileSelected?.Invoke();
        }
        /// <summary> 玩家尝试与某个Tile点击交互 </summary>
        public void InteractTile()
        {
            if(_playerCharacter.TileIndex == SelectedTileIndex)
            {
                // 弹窗询问是否进入
                OnTileInteracted?.Invoke();
            }
            else
            {
                // Move!
                _playerCharacter?.MoveTo(SelectedTileIndex);
            }
        }
        /// <summary> 进入区域地图 </summary>
        public void EnterSelectedSite()
        {
            OnEnterSite?.Invoke();  // 存档
            GameManager.Instance.EnterLevelScene();
        }
    }
}

