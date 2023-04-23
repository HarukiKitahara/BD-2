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
    /// �����������硢��Ⱦ���磨û�о����߼�����Ҫ�ǵ��ù��������ࣩ
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
        /// <summary> ��ָ��tileIndexˢ�½�ɫ </summary>
        private void SpawnCharacterAtTileIndex(int tileIndex)
        {
            Destroy(_playerCharacterGO);    // ��ɱ�����еĽ�ɫ����ֹˢ����
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

