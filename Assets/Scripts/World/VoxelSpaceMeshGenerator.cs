using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using MyProject.VoxelEngine;
namespace MyProject.World
{
    /// <summary>
    /// 解析VoxelSpace的数据，生成VoxelMeshData，不干其他任何事情
    /// </summary>
    public static class VoxelSpaceMeshGenerator
    {
        public static VoxelMeshData[] GenerateMesh(World world)
        {
            var meshData = new VoxelMeshData();
            var subMeshData = new VoxelMeshData();

            DatabaseManager.Instance.Voxels.TryGetAssetByKey("Water", out var waterAsset);

            world.IterateAllCoordinates(HandleMeshDataAt);
            return new VoxelMeshData[2] { meshData, subMeshData };

            // 挨个加进MeshData
            void HandleMeshDataAt(int x, int y)
            {
                var voxel = world.GetVoxelAt(x, y);
                // 处理水平表面。必定显示，所以直接加入
                meshData.AddVoxelSurface(
                    new Vector3(x, voxel.altitude, y),
                    DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.Up,
                    10, EVoxelSurface.up);

                // 处理水面SubMesh
                if (voxel.altitude < world.seaLevel)
                {
                    subMeshData.AddVoxelSurface(new Vector3(x, world.seaLevel - 0.2f, y), waterAsset.TextureInfo.Up, 10, EVoxelSurface.up);
                }

                // 依次处理四个侧面（一整根四棱柱，不单单只是一个Voxel）
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x - 1, y), EVoxelSurface.left);
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x, y + 1), EVoxelSurface.forward);
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x + 1, y), EVoxelSurface.right);
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x, y - 1), EVoxelSurface.back);

                // 提取四个方向的共同操作，以免疯狂复制黏贴
                void AddFullSufaceToVoxelMeshData(Voxel neighbourVoxel, EVoxelSurface surface)
                {
                    if (voxel.altitude <= 0) return;
                    if (neighbourVoxel == null)  // 没邻居就一路刷到底
                    {
                        meshData.AddVoxelSurface(new Vector3(x, voxel.altitude, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.Side, 10, surface);
                        for (int i = 1; i < voxel.altitude; i++)
                        {
                            meshData.AddVoxelSurface(new Vector3(x, voxel.altitude - i, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                    else if(neighbourVoxel.altitude < voxel.altitude) // 只有当邻居比我矮一个头时，才继续刷下去
                    {
                        meshData.AddVoxelSurface(new Vector3(x, voxel.altitude, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.Side, 10, surface);
                        
                        for (int i = 1; i < voxel.altitude - neighbourVoxel.altitude; i++) // 一直刷到邻居的高度，否则就要浪费顶点数了 
                        {
                            meshData.AddVoxelSurface(new Vector3(x, voxel.altitude - i, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                }
            }
        }
    }
}
