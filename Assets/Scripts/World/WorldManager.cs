using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyProject.Database;
namespace MyProject.World
{
    /// <summary>
    /// �����������硢��Ⱦ���磨û�о����߼�����Ҫ�ǵ��ù��������ࣩ
    /// </summary>
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private int _size = 100;
        [SerializeField] private int _seaLevel = 20;
        [SerializeField] private int _maxAltitude = 50;
        [SerializeField] private float _noiseScale = 0.05f;
        [SerializeField] private float _edgeRatio = 0.2f;
        public World World { get; private set; }
        public WorldMeshData WorldMeshData { get; private set; }
        private VoxelMeshRenderer _voxelMeshRenderer;
        
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
        public void NewWorld()
        {
            World = new World(_size, _seaLevel);
            WorldGenerator.GenerateWorld(World, _noiseScale, _maxAltitude, _edgeRatio);
            RenderWorld();
        }
        public void LoadWorld(WorldDataPersistace worldDataPersistace)
        {
            World = new World(worldDataPersistace);
            RenderWorld();
        }
        public WorldDataPersistace SaveWorld()
        {
            return World.DoDataPersistance();
        }
    }
}

