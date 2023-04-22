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
        private MeshCollider _meshCollider;

        private void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshCollider = GetComponent<MeshCollider>();
            
        }
        public void RenderVoxelMesh(VoxelMeshData voxelMeshData)
        {
            Mesh mesh = new();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            mesh.Clear();
            mesh.vertices = voxelMeshData.vertices.ToArray();
            mesh.triangles = voxelMeshData.triangles.ToArray();
            mesh.uv = voxelMeshData.uv.ToArray();
            mesh.RecalculateNormals();
            _meshFilter.mesh = mesh;
            //_meshCollider.sharedMesh = null;

            //_colliderMesh.Clear();
            //_colliderMesh.vertices = _voxelMeshData.colliderVertices.ToArray();
            //_colliderMesh.triangles = _voxelMeshData.colliderTriangles.ToArray();
            //_colliderMesh.RecalculateNormals();
        }
    }
}
