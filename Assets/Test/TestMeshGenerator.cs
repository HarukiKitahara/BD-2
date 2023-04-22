using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Unmanaged
{
    /*
     *  F        2
     * LDRT     1035
     *  B        4
     * 立方体展开测试
     * 左手系测normal方向
     */
    [RequireComponent(typeof(MeshFilter))]
    public class TestMeshGenerator : MonoBehaviour
    {
        private MeshFilter _meshFilter;
        void Start()
        {
            _meshFilter = GetComponent<MeshFilter>();
            //_meshFilter.mesh = GetCubeMesh();
            _meshFilter.mesh = GetRepeatingPlaneMesh();
        }
        private Mesh GetCubeMesh()
        {
            Mesh mesh = new();
            Vector3[] vertices = new Vector3[]
            {
                // Down 按这个顺序normal朝内
                //new Vector3(0,0,0),
                //new Vector3(0,0,1),
                //new Vector3(1,0,1),
                //new Vector3(1,0,0),

                // Down
                new Vector3(0,0,0),
                new Vector3(1,0,0),
                new Vector3(1,0,1),
                new Vector3(0,0,1),
                
                // Left
                new Vector3(0,0,1),
                new Vector3(0,1,1),
                new Vector3(0,1,0),
                new Vector3(0,0,0),
                
                // Forward
                new Vector3(1,0,1),
                new Vector3(1,1,1),
                new Vector3(0,1,1),
                new Vector3(0,0,1),
                
                // Right
                new Vector3(1,0,0),
                new Vector3(1,1,0),
                new Vector3(1,1,1),
                new Vector3(1,0,1),
                
                // Back
                new Vector3(0,0,0),
                new Vector3(0,1,0),
                new Vector3(1,1,0),
                new Vector3(1,0,0),
                
                // Top
                new Vector3(0,1,0),
                new Vector3(0,1,1),
                new Vector3(1,1,1),
                new Vector3(1,1,0)
            };
            mesh.SetVertices(vertices);
            Vector2[] uvs = new Vector2[]
            {
                new Vector2(0.25f,0.25f),
                new Vector2(0.25f,0.5f),
                new Vector2(0.5f,0.5f),
                new Vector2(0.5f,0.25f),

                new Vector2(0f,0.25f),
                new Vector2(0f,0.5f),
                new Vector2(0.25f,0.5f),
                new Vector2(0.25f,0.25f),

                new Vector2(0.25f,0.5f),
                new Vector2(0.25f,0.75f),
                new Vector2(0.5f,0.75f),
                new Vector2(0.5f,0.5f),

                new Vector2(0.5f,0.25f),
                new Vector2(0.5f,0.5f),
                new Vector2(0.75f,0.5f),
                new Vector2(0.75f,0.25f),

                new Vector2(0f,0.25f),
                new Vector2(0f,0.5f),
                new Vector2(0.25f,0.5f),
                new Vector2(0.25f,0.25f),

                new Vector2(0.75f,0.25f),
                new Vector2(0.75f,0.5f),
                new Vector2(1f,0.5f),
                new Vector2(1f,0.25f)
            };
            mesh.SetUVs(0, uvs);

            //int[] indices = new int[] { 0, 0,0,0,1,1,1,1, 2,2,2,2,3,3,3, 3, 4,4,4,4,5,5,5, 5 };
            //int[] indices = new int[] { 0, 1, 2, 3, 4, 5 };
            int[] indices = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23 };
            mesh.SetIndices(indices, MeshTopology.Quads, 0);
            mesh.RecalculateNormals();
            return mesh;
        }
        /*
         * 7654
         * 0123
         */
        private Mesh GetRepeatingPlaneMesh()
        {
            Mesh mesh = new();
            Vector3[] vertices = new Vector3[]
            {
                new Vector3(0,0,0),
                new Vector3(1,0,0),
                new Vector3(2,0,0),
                new Vector3(3,0,0),

                new Vector3(3,0,1),
                new Vector3(2,0,1),
                new Vector3(1,0,1),
                new Vector3(0,0,1),
            };
            Vector2[] uvs = new Vector2[]
            {
                new Vector2(0.25f,0.25f),
                new Vector2(0.5f,0.25f),
                new Vector2(0.5f,0.5f),
                new Vector2(0.25f,0.5f),

                new Vector2(0.25f,0.25f),
                new Vector2(0.5f,0.25f),
                new Vector2(0.5f,0.5f),
                new Vector2(0.25f,0.5f)
            };
            int[] triangles = new int[]
            {
                0,7,6,0,6,1,2,5,4,2,4,3,1,6,5,1,5,2
            };
            mesh.SetVertices(vertices);
            mesh.SetUVs(0, uvs);
            mesh.SetTriangles(triangles,0);

            return mesh;
        }
    }
}

