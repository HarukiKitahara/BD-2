using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.VoxelEngine
{
    /// <summary>
    /// 整个体素空间的MeshData集合，分渲染透不透明以及是否需要collider两个维度
    /// </summary>
    public class VoxelSpaceMeshData
    {
        public readonly VoxelMeshData opaque = new();
        public readonly VoxelMeshData transparent = new();
        public readonly VoxelMeshData collider = new();
    }
}
