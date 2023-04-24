using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyProject.Database;
using System;
using MyProject.Core;
namespace MyProject.World
{
    [Serializable]
    public class WorldManagerDataPersistance
    {
        public WorldCharacterControllerDataPersistance worldCharacterDataPersistance;
        public WorldDataPersistance worldDataPersistance;
        public WorldManagerDataPersistance(WorldDataPersistance worldDataPersistance, WorldCharacterControllerDataPersistance worldCharacterDataPersistance)
        {
            this.worldDataPersistance = worldDataPersistance;
            this.worldCharacterDataPersistance = worldCharacterDataPersistance;
        }
    }
    /// <summary> �����������硢��Ⱦ���磨û�о����߼�����Ҫ�ǵ��ù��������ࣩ </summary>
    public class WorldManager : MonoBehaviour
    {
        public World World { get; private set; }
        public WorldMeshData WorldMeshData { get; private set; }
        [SerializeField]
        private VoxelMeshRenderer _voxelMeshRenderer;
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
            WorldMeshData = WorldMeshGenerator.GenerateWorldMesh(World);
            _voxelMeshRenderer.RenderWorldMesh(WorldMeshData);
        }
        /// <summary> ��������磬����ˢ�½�ɫ </summary>
        public void NewWorld()
        {
            World = WorldGenerator.GenerateWorld(_worldGenerationDatabaseAsset);
            var playerTileIndex = World.GetRandomTileIndexAboveSeaLevel();
            SpawnCharacterAtTileIndex(playerTileIndex);
            RenderWorld();
        }
        public void LoadWorld(WorldManagerDataPersistance worldManagerDataPersistace)
        {
            World = new World(worldManagerDataPersistace.worldDataPersistance);
            LoadCharacter(worldManagerDataPersistace.worldCharacterDataPersistance);
            RenderWorld();
        }
        public WorldManagerDataPersistance SaveWorld()
        {
            return new WorldManagerDataPersistance(World.DoDataPersistance(), _playerCharacter.DoDataPersistance());
        }
        /// <summary> ��ָ��tileIndexˢ�½�ɫ���Ƕ����� </summary>
        private void SpawnCharacterAtTileIndex(int tileIndex)
        {
            _playerCharacterGO.SetActive(true);
            _playerCharacter = new(World, _playerCharacterGO.transform, tileIndex);
        }
        private void LoadCharacter(WorldCharacterControllerDataPersistance dataPersistance)
        {
            _playerCharacterGO.SetActive(true);
            _playerCharacter = new(World, _playerCharacterGO.transform, dataPersistance);
        }
        /// <summary> �����ѡ��ĳ��Tile </summary>
        public void SelectTile(int index)
        {
            if (index == SelectedTileIndex) return;
            _SelectionHintGO.transform.position = World.GetTileOriginPositionByIndex(index);
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

