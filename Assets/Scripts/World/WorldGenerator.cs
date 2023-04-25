using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using MyProject.VoxelEngine;
namespace MyProject.World
{
    /// <summary>
    /// 你喂给我参数，我帮你生成世界
    /// </summary>
    public static class WorldGenerator
    {
        private static Vector2 _sampleOrigin;   // 随机PerlinNoise采样的原点
        private static float _edgeLength;   // 边缘判断，为了确保边界都是海洋。TODO：算法待优化，目前纯线性衰减至0
        public static World GenerateWorld(WorldGenerationDatabaseAsset worldGenerationDatabaseAsset)
        {
            var world = new World(worldGenerationDatabaseAsset.Size, worldGenerationDatabaseAsset.SeaLevel);
            _sampleOrigin = Random.insideUnitCircle * Random.Range(0, 20);  // 每次生成世界都随一个新的采样原点
            _edgeLength = world.length * worldGenerationDatabaseAsset.EdgeRatio;
            world.IterateAllCoordinates(GenerateTileAt);
            return world;

            // 依次生成每一个点。 TODO：目前随机生成算法太简陋了，要拓展生态群系、使用频域叠加的PerlinNoise
            void GenerateTileAt(int x, int y)
            {
                var generatedAltitude = GetAltitudeAtCoordinates(x, y);     // 生成海拔
                int desiredTileID;  // 根据海拔生成地块类型
                if (generatedAltitude <= world.seaLevel - 1)
                {
                    DatabaseManager.Instance.WorldTiles.TryGetIndexByKey("Stone", out desiredTileID);  // 低于海平面3格就是石头
                }
                else if (generatedAltitude > world.seaLevel)
                {
                    DatabaseManager.Instance.WorldTiles.TryGetIndexByKey("Grass", out desiredTileID);  // 海平面往上就是草地
                }
                else
                {
                    DatabaseManager.Instance.WorldTiles.TryGetIndexByKey("Sand", out desiredTileID);  // 海平面往下是沙子
                }

                var generatedVoxel = new Voxel(desiredTileID, generatedAltitude);
                world.voxels[world.GetIndexAt(x, y)] = generatedVoxel;
            }
            // 采样得到海拔。TODO：优化算法，现在是纯纯的PerlinNoise
            int GetAltitudeAtCoordinates(int x, int y)
            {
                var sample = Mathf.PerlinNoise(x * worldGenerationDatabaseAsset.NoiseScale + _sampleOrigin.x, y * worldGenerationDatabaseAsset.NoiseScale + _sampleOrigin.y) * worldGenerationDatabaseAsset.MaxAltitude;
                // 从_edgeLength开始，线性地缩放海拔，确保边界都在海里。TODO：优化算法
                if (x <= _edgeLength) sample *= x / _edgeLength;
                if (y <= _edgeLength) sample *= y / _edgeLength;
                if (x >= world.length - _edgeLength) sample *= (world.length - x) / _edgeLength;
                if (y >= world.length - _edgeLength) sample *= (world.length - y) / _edgeLength;
                return (int)sample;
            }
        }
    }
}
