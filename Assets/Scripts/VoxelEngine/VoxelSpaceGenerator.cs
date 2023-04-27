using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
namespace MyProject.VoxelEngine
{
    public static class VoxelSpaceGenerator
    {
        public static VoxelSpace GenerateFlatSpace(int size)
        {
            var space = new VoxelSpace(size);

            DatabaseManager.Instance.WorldTiles.TryGetIndexByKey("Grass", out var grassID); // Ĭ���ֲ�

            space.IterateAllCoordinates(GenerateVoxelAt);
            return space;

            void GenerateVoxelAt(int x, int y)
            {
                var generatedAltitude = GetAltitudeAtCoordinates(x, y);     // ���ɺ���
                var generatedVoxel = new Voxel(grassID, generatedAltitude);
                space.voxels[space.GetIndexAt(x, y)] = generatedVoxel;
            }

            int GetAltitudeAtCoordinates(int x, int y)
            {
                return space.IsOnEdge(x, y) ? 2 : 1;
            }
        }
    }
}
