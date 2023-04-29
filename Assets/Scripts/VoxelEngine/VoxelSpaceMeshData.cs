using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.VoxelEngine
{
    /// <summary>
    /// �������ؿռ��MeshData���ϣ�����Ⱦ͸��͸���Լ��Ƿ���Ҫcollider����ά��
    /// </summary>
    public class VoxelSpaceMeshData
    {
        public readonly VoxelMeshData opaque = new();
        public readonly VoxelMeshData transparent = new();
        public readonly VoxelMeshData collider = new();
    }
}
