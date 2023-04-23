using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyProject.World
{
    /// <summary>
    /// 把MeshData转化为真正的Mesh，填进MeshFilter
    /// 虽然名字里带Renderer，但其实没对Renderer进行任何操作。注意要在Scene里配好SubMesh的材质。
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
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;    // 必须修改位数，否则只能分成一个个小chunk
            mesh.subMeshCount = 2;  // 分别是main和water

            // 把main和water的顶点还有uv都合并起来（Unity只能这么操作）
            mesh.vertices = worldMeshData.mainMesh.vertices.Concat(worldMeshData.waterMesh.vertices).ToArray(); 
            mesh.uv = worldMeshData.mainMesh.uv.Concat(worldMeshData.waterMesh.uv).ToArray();

            // 告诉unity合并之后，前面的是main，后面的是water
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
