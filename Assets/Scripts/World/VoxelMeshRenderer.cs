using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.World
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class VoxelMeshRenderer : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;
        private MeshCollider _meshCollider;
        private Mesh _mesh;
        private Mesh _colliderMesh;

        private VoxelMeshData _voxelMeshData;

        public void Initialize(VoxelMeshData voxelMeshData)
        {
            _voxelMeshData = voxelMeshData;
        }

        private void RenderVoxelMesh()
        {
            _mesh.Clear();
            _mesh.vertices = _voxelMeshData.vertices.ToArray();
            _mesh.triangles = _voxelMeshData.triangles.ToArray();
            _mesh.uv = _voxelMeshData.uv.ToArray();
            _mesh.RecalculateNormals();

            _meshCollider.sharedMesh = null;

            _colliderMesh.Clear();
            _colliderMesh.vertices = _voxelMeshData.colliderVertices.ToArray();
            _colliderMesh.triangles = _voxelMeshData.colliderTriangles.ToArray();
            _colliderMesh.RecalculateNormals();
        }

        public void UpdateVoxelMesh(VoxelMeshData voxelMeshData)
        {
            _voxelMeshData = voxelMeshData;
            RenderVoxelMesh();
        }
    }
}
