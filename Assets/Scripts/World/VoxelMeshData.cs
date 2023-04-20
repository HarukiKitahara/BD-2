using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.World
{
    /*
     * 1 - 2
     * | / |
     * 0 - 3
     * 
     * Two Clockwise Triangles!
     * 0, 1, 2
     * 0, 2, 3
     * Clockwise means normal toward outside, which makes quad visible!
     */
    public class VoxelMeshData
    {
        public readonly List<Vector3> vertices = new();
        public readonly List<int> triangles = new();
        public readonly List<Vector2> uv = new();
        public readonly List<Vector3> colliderVertices = new();
        public readonly List<int> colliderTriangles = new();

        public void AddVertex(Vector3 vertex, bool contributesToCollider)
        {
            vertices.Add(vertex);
            if (contributesToCollider) colliderVertices.Add(vertex);
        }
        public void AddQuadTriangles(bool contributesToCollider)
        {
            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 3);
            triangles.Add(vertices.Count - 2);

            triangles.Add(vertices.Count - 4);
            triangles.Add(vertices.Count - 2);
            triangles.Add(vertices.Count - 1);
            if (contributesToCollider)
            {
                colliderTriangles.Add(vertices.Count - 4);
                colliderTriangles.Add(vertices.Count - 3);
                colliderTriangles.Add(vertices.Count - 2);

                colliderTriangles.Add(vertices.Count - 4);
                colliderTriangles.Add(vertices.Count - 2);
                colliderTriangles.Add(vertices.Count - 1);
            }
        }


    }
}
