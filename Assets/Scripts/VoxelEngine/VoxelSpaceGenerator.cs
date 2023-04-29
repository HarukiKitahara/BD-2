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

            DatabaseManager.Instance.Voxels.TryGetIndexByKey("Grass", out var grassID); // Ĭ���ֲ�
            DatabaseManager.Instance.Voxels.TryGetIndexByKey("InvisibleWall", out var invisibleWallID); // �߽�ſ���ǽ

            space.IterateAllCoordinates(GenerateVoxelAt);
            return space;

            void GenerateVoxelAt(int x, int y)
            {
                if(space.IsOnEdge(x, y))
                {
                    space.voxels[space.GetIndexAt(x, y)] = new Voxel(invisibleWallID, 10);
                }
                else
                {
                    space.voxels[space.GetIndexAt(x, y)] = new Voxel(grassID, 1);
                }
            }
        }
    }
}
