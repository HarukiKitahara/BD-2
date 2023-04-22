using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.World
{
    public class WorldMeshData
    {
        public readonly VoxelMeshData mainMesh;
        public readonly VoxelMeshData waterMesh;
        public WorldMeshData(VoxelMeshData mainMesh, VoxelMeshData waterMesh)
        {
            this.mainMesh = mainMesh;
            this.waterMesh = waterMesh;
        }
    }
}
