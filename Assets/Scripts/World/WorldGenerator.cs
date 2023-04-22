using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using MyProject.Utils;
namespace MyProject.World
{
    /// <summary>
    /// ��ι���Ҳ������Ұ�����������
    /// </summary>
    public static class WorldGenerator
    {
        private static Vector2 _sampleOrigin;
        private static float _edgeLength;
        public static void GenerateWorld(World world, float noiseScale, int maxAltidue, float edgeRatio = 0.1f)
        {
            _sampleOrigin = Random.insideUnitCircle * Random.Range(0, 20);
            _edgeLength = world.length * edgeRatio;
            world.IterateAllCoordinates(GenerateTileAt);

            void GenerateTileAt(int x, int y)
            {
                var generatedAltitude = GetAltitudeAtCoordinates(x, y);
                WorldTileDatabaseAsset desiredTile;
                if(generatedAltitude <= world.seaLevel - 3)
                {
                    desiredTile = DatabaseManager.Instance.worldTileDatabaseAssets[2];  // ʯͷ
                }
                else if(generatedAltitude >= world.seaLevel)
                {
                    desiredTile = DatabaseManager.Instance.worldTileDatabaseAssets[0];  // ��
                }
                else
                {
                    desiredTile = DatabaseManager.Instance.worldTileDatabaseAssets[1];  // ɳ��
                }

                var generatedTile = new WorldTile(desiredTile, generatedAltitude);
                world.worldTiles[world.GetTileIndexByCoordinates(x, y)] = generatedTile;
            }

            int GetAltitudeAtCoordinates(int x, int y)
            {
                var sample = Mathf.PerlinNoise(x * noiseScale + _sampleOrigin.x, y * noiseScale + _sampleOrigin.y) * maxAltidue;
                // ȷ����Χ���ں���
                if (x <= _edgeLength) sample *= x / _edgeLength;
                if (y <= _edgeLength) sample *= y / _edgeLength;
                if (x >= world.length - _edgeLength) sample *= (world.length - x) / _edgeLength;
                if (y >= world.length - _edgeLength) sample *= (world.length - y) / _edgeLength;
                return (int)sample;
            }
        }
    }
}
