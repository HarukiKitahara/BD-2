using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
namespace MyProject.World
{
    /// <summary>
    /// ����World�����ݣ�����WorldMeshData�����������κ�����
    /// </summary>
    public static class WorldMeshGenerator
    {
        public static WorldMeshData GenerateWorldMesh(World world)
        {
            var mainMeshData = new VoxelMeshData();
            var waterMeshData = new VoxelMeshData();

            if(!DatabaseManager.Instance.WorldTiles.TryGetAssetByKey("Water", out var waterAsset))  // TODO���о�ֱ����������waterҲ���ԣ��Ժ���˵�ɡ���
            {
                throw new System.Exception("ˮ���ز��Ҳ����ˣ���Ⱦʧ��");
            }

            world.IterateAllCoordinates(AddWorldTileToVoxelMeshData);
            return new WorldMeshData(mainMeshData, waterMeshData);

            // �����ӽ�MeshData
            void AddWorldTileToVoxelMeshData(int x, int y)
            {
                var tile = world.GetWorldTileAt(x, y);
                // ����ˮƽ���档�ض���ʾ������ֱ�Ӽ���
                mainMeshData.AddVoxelSurface(
                    new Vector3(x, tile.altitude, y),
                    DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.Up,
                    10, EVoxelSurface.up);

                // ����ˮ��SubMesh
                if (tile.altitude < world.seaLevel)
                {
                    waterMeshData.AddVoxelSurface(new Vector3(x, world.seaLevel - 0.2f, y), waterAsset.TextureInfo.Up, 10, EVoxelSurface.up);
                }

                // ���δ����ĸ����棨һ������������������ֻ��һ��Voxel��
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x - 1, y), EVoxelSurface.left);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y + 1), EVoxelSurface.forward);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x + 1, y), EVoxelSurface.right);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y - 1), EVoxelSurface.back);

                // ��ȡ�ĸ�����Ĺ�ͬ�����������������
                void AddFullSufaceToVoxelMeshData(WorldTile neighbourTile, EVoxelSurface surface)
                {
                    if (tile.altitude <= 0) return;
                    if (neighbourTile == null)  // û�ھӾ�һ·ˢ����
                    {
                        mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.Side, 10, surface);
                        for (int i = 1; i < tile.altitude; i++)
                        {
                            mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                    else if(neighbourTile.altitude < tile.altitude) // ֻ�е��ھӱ��Ұ�һ��ͷʱ���ż���ˢ��ȥ
                    {
                        mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.Side, 10, surface);
                        
                        for (int i = 1; i < tile.altitude - neighbourTile.altitude; i++) // һֱˢ���ھӵĸ߶ȣ������Ҫ�˷Ѷ������� 
                        {
                            mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                }
            }
        }
    }
}
