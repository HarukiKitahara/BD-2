using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyProject.World
{
    /// <summary>
    /// ��MeshDataת��Ϊ������Mesh�����MeshFilter
    /// ��Ȼ�������Renderer������ʵû��Renderer�����κβ�����ע��Ҫ��Scene�����SubMesh�Ĳ��ʡ�
    /// </summary>
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
        public void RenderWorldMesh(WorldMeshData worldMeshData)
        {
            Mesh mesh = new();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;    // �����޸�λ��������ֻ�ֳܷ�һ����Сchunk
            mesh.subMeshCount = 2;  // �ֱ���main��water

            // ��main��water�Ķ��㻹��uv���ϲ�������Unityֻ����ô������
            mesh.vertices = worldMeshData.mainMesh.vertices.Concat(worldMeshData.waterMesh.vertices).ToArray(); 
            mesh.uv = worldMeshData.mainMesh.uv.Concat(worldMeshData.waterMesh.uv).ToArray();

            // ����unity�ϲ�֮��ǰ�����main���������water
            mesh.SetTriangles(worldMeshData.mainMesh.triangles, 0);
            mesh.SetTriangles(worldMeshData.waterMesh.triangles.Select(o => o + worldMeshData.mainMesh.vertices.Count).ToArray(), 1);

            mesh.RecalculateNormals();
            _meshFilter.mesh = mesh;


            Mesh collisionMesh = new();
            collisionMesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
            collisionMesh.vertices = worldMeshData.mainMesh.vertices.ToArray();
            collisionMesh.triangles = worldMeshData.mainMesh.triangles.ToArray();
            collisionMesh.RecalculateNormals();
            _meshCollider.sharedMesh = collisionMesh;
        }
        
        //public void RenderVoxelMesh(VoxelMeshData voxelMeshData)
        //{
        //    Mesh mesh = new();
        //    mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;
        //    mesh.Clear();
        //    mesh.vertices = voxelMeshData.vertices.ToArray();
        //    mesh.triangles = voxelMeshData.triangles.ToArray();
        //    mesh.uv = voxelMeshData.uv.ToArray();
        //    mesh.RecalculateNormals();
        //    _meshFilter.mesh = mesh;
        //    //_meshCollider.sharedMesh = null;
        //    //_colliderMesh.Clear();
        //    //_colliderMesh.vertices = _voxelMeshData.colliderVertices.ToArray();
        //    //_colliderMesh.triangles = _voxelMeshData.colliderTriangles.ToArray();
        //    //_colliderMesh.RecalculateNormals();
        //}
    }
}
