using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using MyProject.Utils;
namespace MyProject.World
{
    /// <summary>
    /// 你喂给我参数，我帮你生成世界
    /// </summary>
    public static class WorldGenerator
    {
        private static Vector2 _sampleOrigin;
        public static void GenerateWorld(World world, float noiseScale, int maxAltidue)
        {
            _sampleOrigin = Random.insideUnitCircle * Random.Range(0, 20);
            world.IterateAllCoordinates(GenerateTileAt);

            void GenerateTileAt(int x, int y)
            {
                var generatedTile = new WorldTile(DatabaseManager.Instance.worldTileDatabaseAssets[0], GetAltitudeAtCoordinates(x, y));
                world.worldTiles[world.GetTileIndexByCoordinates(x, y)] = generatedTile;
            }

            int GetAltitudeAtCoordinates(int x, int y)
            {
                return (int)(Mathf.PerlinNoise(x * noiseScale + _sampleOrigin.x, y * noiseScale + _sampleOrigin.y) * maxAltidue);
            }
        }
    }
}
