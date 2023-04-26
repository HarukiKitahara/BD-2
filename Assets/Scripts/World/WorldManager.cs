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
    /// <summary> �����������硢��Ⱦ���磨û�о����߼�����Ҫ�ǵ��ù��������ࣩ </summary>
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
        // һ���������ж��world�ڷ������¼�������һ����static
        public static event Action OnTileSelected;
        public static event Action OnTileInteracted;
        public static event Action OnEnterSite;
        /// <summary> ����Mesh���ύ��Ⱦһ�������� </summary>
        private void RenderWorld()
        {
            var meshDatas = VoxelSpaceMeshGenerator.GenerateMesh(World);
            var meshs = VoxelMeshHelper.BuildMeshs(meshDatas);
            _meshFilter.mesh = VoxelMeshHelper.MergeMesh(false, meshs);
            _meshCollider.sharedMesh = meshs[0];        // ֻ�е�����Ҫcollider
        }
        /// <summary> ��������磬����ˢ�½�ɫ </summary>
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
        /// <summary> ��ָ��tileIndexˢ�½�ɫ���Ƕ����� </summary>
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
        /// <summary> �����ѡ��ĳ��Tile </summary>
        public void SelectTile(int index)
        {
            if (index == SelectedTileIndex) return;
            _SelectionHintGO.transform.position = World.GetVoxelOriginPositionByIndex(index);
            SelectedTileIndex = index;
            OnTileSelected?.Invoke();
        }
        /// <summary> ��ҳ�����ĳ��Tile������� </summary>
        public void InteractTile()
        {
            if(_playerCharacter.TileIndex == SelectedTileIndex)
            {
                // ����ѯ���Ƿ����
                OnTileInteracted?.Invoke();
            }
            else
            {
                // Move!
                _playerCharacter?.MoveTo(SelectedTileIndex);
            }
        }
        /// <summary> ���������ͼ </summary>
        public void EnterSelectedSite()
        {
            OnEnterSite?.Invoke();  // �浵
            GameManager.Instance.EnterLevelScene();
        }
    }
}

