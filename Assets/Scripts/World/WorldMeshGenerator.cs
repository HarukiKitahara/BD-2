using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.World
{
    /// <summary>
    /// 解析数据，生成WorldMesh，不干其他任何事情
    /// </summary>
    public static class WorldMeshGenerator
    {
        public static VoxelMeshData GenerateWorldMesh(World world)
        {
            var voxelMeshData = new VoxelMeshData();
            world.IterateAllCoordinates(AddWorldTileToVoxelMeshData);
            return voxelMeshData;

            void AddWorldTileToVoxelMeshData(int x, int y)
            {
                var tile = world.GetWorldTileAt(x, y);
                voxelMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), tile.databaseAsset.TextureInfo.Up, 10, EVoxelSurface.up);

                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x - 1, y), EVoxelSurface.left);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y + 1), EVoxelSurface.forward);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x + 1, y), EVoxelSurface.right);
                AddFullSufaceToVoxelMeshData(world.GetWorldTileAt(x, y - 1), EVoxelSurface.back);

                void AddFullSufaceToVoxelMeshData(WorldTile neighbourTile, EVoxelSurface surface)
                {
                    if (tile.altitude <= 0) return;
                    if (neighbourTile == null) 
                    {
                        voxelMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), tile.databaseAsset.TextureInfo.Side, 10, surface);
                        for (int i = 1; i < tile.altitude; i++)
                        {
                            voxelMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), tile.databaseAsset.TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                    else if(neighbourTile.altitude < tile.altitude)
                    {
                        voxelMeshData.AddVoxelSurface(new Vector3(x, tile.altitude, y), tile.databaseAsset.TextureInfo.Side, 10, surface);
                        
                        for (int i = 1; i < tile.altitude - neighbourTile.altitude; i++)
                        {
                            voxelMeshData.AddVoxelSurface(new Vector3(x, tile.altitude - i, y), tile.databaseAsset.TextureInfo.RepeatingSide, 10, surface);
                        }
                    }
                }
            }
        }
        public static VoxelMeshData GenerateWaterMesh(World world)
        {
            return null;
        }
    }
}
