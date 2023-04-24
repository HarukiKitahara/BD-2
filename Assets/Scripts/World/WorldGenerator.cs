using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
namespace MyProject.World
{
    /// <summary>
    /// ��ι���Ҳ������Ұ�����������
    /// </summary>
    public static class WorldGenerator
    {
        private static Vector2 _sampleOrigin;   // ���PerlinNoise������ԭ��
        private static float _edgeLength;   // ��Ե�жϣ�Ϊ��ȷ���߽綼�Ǻ���TODO���㷨���Ż���Ŀǰ������˥����0
        public static World GenerateWorld(WorldGenerationDatabaseAsset worldGenerationDatabaseAsset)
        {
            var world = new World(worldGenerationDatabaseAsset.Size, worldGenerationDatabaseAsset.SeaLevel);
            _sampleOrigin = Random.insideUnitCircle * Random.Range(0, 20);  // ÿ���������綼��һ���µĲ���ԭ��
            _edgeLength = world.length * worldGenerationDatabaseAsset.EdgeRatio;
            world.IterateAllCoordinates(GenerateTileAt);
            return world;

            // ��������ÿһ���㡣 TODO��Ŀǰ��������㷨̫��ª�ˣ�Ҫ��չ��̬Ⱥϵ��ʹ��Ƶ����ӵ�PerlinNoise
            void GenerateTileAt(int x, int y)
            {
                var generatedAltitude = GetAltitudeAtCoordinates(x, y);     // ���ɺ���
                int desiredTileID;  // ���ݺ������ɵؿ�����
                if (generatedAltitude <= world.seaLevel - 3)
                {
                    desiredTileID = 2;  // ���ں�ƽ��3�����ʯͷ
                }
                else if (generatedAltitude >= world.seaLevel)
                {
                    desiredTileID = 0;  // ��ƽ�����Ͼ��ǲݵ�
                }
                else
                {
                    desiredTileID = 1;  // ��ƽ��������ɳ��
                }

                var generatedTile = new WorldTile(desiredTileID, generatedAltitude);
                world.worldTiles[world.GetIndexAt(x, y)] = generatedTile;
            }
            // �����õ����Ρ�TODO���Ż��㷨�������Ǵ�����PerlinNoise
            int GetAltitudeAtCoordinates(int x, int y)
            {
                var sample = Mathf.PerlinNoise(x * worldGenerationDatabaseAsset.NoiseScale + _sampleOrigin.x, y * worldGenerationDatabaseAsset.NoiseScale + _sampleOrigin.y) * worldGenerationDatabaseAsset.MaxAltitude;
                // ��_edgeLength��ʼ�����Ե����ź��Σ�ȷ���߽綼�ں��TODO���Ż��㷨
                if (x <= _edgeLength) sample *= x / _edgeLength;
                if (y <= _edgeLength) sample *= y / _edgeLength;
                if (x >= world.length - _edgeLength) sample *= (world.length - x) / _edgeLength;
                if (y >= world.length - _edgeLength) sample *= (world.length - y) / _edgeLength;
                return (int)sample;
            }
        }
    }
}
