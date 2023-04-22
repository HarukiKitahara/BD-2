using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using MyProject.Database;
namespace MyProject.World
{
    public class WorldManager : MonoBehaviour
    {
        [SerializeField] private int _size = 100;
        [SerializeField] private int _seaLevel = 20;
        [SerializeField] private int _maxAltitude = 50;
        [SerializeField] private float _noiseScale = 0.05f;
        public World World { get; private set; }
        public WorldMeshData WorldMeshData { get; private set; }
        private VoxelMeshRenderer _voxelMeshRenderer;
        
        private void Start()
        {
            World = new World(_size, _seaLevel);
            //Debug.Log(World.worldTiles.Count(o => o.databaseAsset == DatabaseManager.Instance.WorldTileDatabaseAssets[0]));

            WorldGenerator.GenerateWorld(World, _noiseScale, _maxAltitude);

            WorldMeshData = new WorldMeshData(WorldMeshGenerator.GenerateWorldMesh(World), WorldMeshGenerator.GenerateWaterMesh(World));

            _voxelMeshRenderer = GetComponent<VoxelMeshRenderer>();
            _voxelMeshRenderer.RenderVoxelMesh(WorldMeshData.mainMesh);
        }
    }
}

