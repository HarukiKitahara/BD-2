using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using MyProject.VoxelEngine;
namespace MyProject.World
{
    /// <summary>
    /// ����VoxelSpace�����ݣ�����VoxelMeshData�����������κ�����
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

            // �����ӽ�MeshData
            void HandleMeshDataAt(int x, int y)
            {
                var voxel = world.GetVoxelAt(x, y);
                // ����ˮƽ���档�ض���ʾ������ֱ�Ӽ���
                meshData.AddVoxelSurface(
                    new Vector3(x, voxel.altitude, y),
                    DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.Up,
                    10, EVoxelSurface.up);

                // ����ˮ��SubMesh
                if (voxel.altitude < world.seaLevel)
                {
                    subMeshData.AddVoxelSurface(new Vector3(x, world.seaLevel - 0.2f, y), waterAsset.TextureInfo.Up, 10, EVoxelSurface.up);
                }

                // ���δ����ĸ����棨һ������������������ֻ��һ��Voxel��
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x - 1, y), EVoxelSurface.left);
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x, y + 1), EVoxelSurface.forward);
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x + 1, y), EVoxelSurface.right);
                AddFullSufaceToVoxelMeshData(world.GetVoxelAt(x, y - 1), EVoxelSurface.back);

                // ��ȡ�ĸ�����Ĺ�ͬ�����������������
                void AddFullSufaceToVoxelMeshData(Voxel neighbourVoxel, EVoxelSurface surface)
                {
                    if (voxel.altitude <= 0) return;
                    if (neighbourVoxel == null)  // û�ھӾ�һ·ˢ����
                    {
                        meshData.AddVoxelSurface(new Vector3(x, voxel.altitude, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.Side, 10, surface);
                        for (int i = 1; i < voxel.altitude; i++)
                        {
                            meshData.AddVoxelSurface(new Vector3(x, voxel.altitude - i, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                    else if(neighbourVoxel.altitude < voxel.altitude) // ֻ�е��ھӱ��Ұ�һ��ͷʱ���ż���ˢ��ȥ
                    {
                        meshData.AddVoxelSurface(new Vector3(x, voxel.altitude, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.Side, 10, surface);
                        
                        for (int i = 1; i < voxel.altitude - neighbourVoxel.altitude; i++) // һֱˢ���ھӵĸ߶ȣ������Ҫ�˷Ѷ������� 
                        {
                            meshData.AddVoxelSurface(new Vector3(x, voxel.altitude - i, y), DatabaseManager.Instance.Voxels.Assets[voxel.voxelID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                }
            }
        }
    }
}
