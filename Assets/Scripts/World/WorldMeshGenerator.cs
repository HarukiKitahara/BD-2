using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
namespace MyProject.World
{
    /// <summary>
    /// 解析数据，生成WorldMesh，不干其他任何事情
    /// </summary>
    public static class WorldMeshGenerator
    {
        public static WorldMeshData GenerateWorldMesh(World world)
        {
            var mainMeshData = new VoxelMeshData();
            var waterMeshData = new VoxelMeshData();
            world.IterateAllCoordinates(AddWorldTileToVoxelMeshData);
            return new WorldMeshData(mainMeshData, waterMeshData);

            void AddWorldTileToVoxelMeshData(int x, int y)
            {
                var tile = world.GetWorldTileAt(x, y);
                mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), tile.databaseAsset.TextureInfo.Up, 10, EVoxelSurface.up);

                // 生成水面SubMesh
                if (tile.altitude < world.seaLevel)
                {
                    waterMeshData.AddVoxelSurface(new Vector3(x, world.seaLevel - 0.2f, y), DatabaseManager.Instance.waterDatabaseAssets.TextureInfo.Up, 10, EVoxelSurface.up);
                }

                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x - 1, y), EVoxelSurface.left);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y + 1), EVoxelSurface.forward);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x + 1, y), EVoxelSurface.right);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y - 1), EVoxelSurface.back);

                void AddFullSufaceToVoxelMeshData(WorldTile neighbourTile, EVoxelSurface surface)
                {
                    if (tile.altitude <= 0) return;
                    if (neighbourTile == null) 
                    {
                        mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), tile.databaseAsset.TextureInfo.Side, 10, surface);
                        for (int i = 1; i < tile.altitude; i++)
                        {
                            mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), tile.databaseAsset.TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                    else if(neighbourTile.altitude < tile.altitude)
                    {
                        mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), tile.databaseAsset.TextureInfo.Side, 10, surface);
                        
                        for (int i = 1; i < tile.altitude - neighbourTile.altitude; i++)
                        {
                            mainMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), tile.databaseAsset.TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                }
            }
        }
    }
}
