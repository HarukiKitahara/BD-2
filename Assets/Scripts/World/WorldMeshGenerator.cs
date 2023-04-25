using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
namespace MyProject.World
{
    /// <summary>
    /// 解析World的数据，生成WorldMeshData，不干其他任何事情
    /// </summary>
    public static class WorldMeshGenerator
    {
        public static WorldMeshData GenerateWorldMesh(World world)
        {
            var mainMeshData = new VoxelMeshData();
            var waterMeshData = new VoxelMeshData();

            if(!DatabaseManager.Instance.WorldTiles.TryGetAssetByKey("Water", out var waterAsset))  // TODO：感觉直接在这里填water也不对，以后再说吧。。
            {
                throw new System.Exception("水面素材找不到了，渲染失败");
            }

            world.IterateAllCoordinates(AddWorldTileToVoxelMeshData);
            return new WorldMeshData(mainMeshData, waterMeshData);

            // 挨个加进MeshData
            void AddWorldTileToVoxelMeshData(int x, int y)
            {
                var tile = world.GetWorldTileAt(x, y);
                // 处理水平表面。必定显示，所以直接加入
                mainMeshData.AddVoxelSurface(
                    new Vector3(x, tile.altitude, y),
                    DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.Up,
                    10, EVoxelSurface.up);

                // 处理水面SubMesh
                if (tile.altitude < world.seaLevel)
                {
                    waterMeshData.AddVoxelSurface(new Vector3(x, world.seaLevel - 0.2f, y), waterAsset.TextureInfo.Up, 10, EVoxelSurface.up);
                }

                // 依次处理四个侧面（一整根四棱柱，不单单只是一个Voxel）
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x - 1, y), EVoxelSurface.left);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y + 1), EVoxelSurface.forward);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x + 1, y), EVoxelSurface.right);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y - 1), EVoxelSurface.back);

                // 提取四个方向的共同操作，以免疯狂复制黏贴
                void AddFullSufaceToVoxelMeshData(WorldTile neighbourTile, EVoxelSurface surface)
                {
                    if (tile.altitude <= 0) return;
                    if (neighbourTile == null)  // 没邻居就一路刷到底
                    {
                        mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.Side, 10, surface);
                        for (int i = 1; i < tile.altitude; i++)
                        {
                            mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                    else if(neighbourTile.altitude < tile.altitude) // 只有当邻居比我矮一个头时，才继续刷下去
                    {
                        mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.Side, 10, surface);
                        
                        for (int i = 1; i < tile.altitude - neighbourTile.altitude; i++) // 一直刷到邻居的高度，否则就要浪费顶点数了 
                        {
                            mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), DatabaseManager.Instance.WorldTiles.Assets[tile.worldTileID].TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                }
            }
        }
    }
}
