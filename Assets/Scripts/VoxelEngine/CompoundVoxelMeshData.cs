using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyProject.VoxelEngine
{
    public class CompoundVoxelMeshData
    {
        public readonly VoxelMeshData[] datas;
        public CompoundVoxelMeshData(VoxelMeshData[] datas)
        {
            this.datas = datas;
        }
        /// <summary>
        /// 根据MeshData合并成一个大Mesh，并区分submesh index编号。
        /// </summary>
        public Mesh CombineMesh()
        {
            Mesh mesh = new();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;    // 必须修改位数，否则只能分成一个个小chunk(65536)
            mesh.subMeshCount = datas.Length;

            int triangleCount = 0;
            datas.ToList().ForEach(o => triangleCount += o.TrisCount * 3);

            // 把所有mesh的顶点和uv都合进来
            List<Vector3> verticesTemp = new(triangleCount);
            List<Vector2> uvsTemp = new(triangleCount);
            for (int i = 0; i < datas.Length; i++)
            {
                verticesTemp.AddRange(datas[i].vertices);
                uvsTemp.AddRange(datas[i].uv);
            }
            mesh.vertices = verticesTemp.ToArray();
            mesh.uv = uvsTemp.ToArray();

            // 告诉unity合并之后的submesh index编号
            triangleCount = 0;
            for (int i = 0; i < datas.Length; i++)
            {
                mesh.SetTriangles(datas[i].triangles.Select(o => o + triangleCount).ToArray(), i);
                triangleCount += datas[i].TrisCount * 3;
            }

            mesh.RecalculateNormals();
            return mesh;
        }
    }
}
