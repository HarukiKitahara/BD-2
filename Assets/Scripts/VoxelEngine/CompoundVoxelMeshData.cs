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
        /// ����MeshData�ϲ���һ����Mesh��������submesh index��š�
        /// </summary>
        public Mesh CombineMesh()
        {
            Mesh mesh = new();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;    // �����޸�λ��������ֻ�ֳܷ�һ����Сchunk(65536)
            mesh.subMeshCount = datas.Length;

            int triangleCount = 0;
            datas.ToList().ForEach(o => triangleCount += o.TrisCount * 3);

            // ������mesh�Ķ����uv���Ͻ���
            List<Vector3> verticesTemp = new(triangleCount);
            List<Vector2> uvsTemp = new(triangleCount);
            for (int i = 0; i < datas.Length; i++)
            {
                verticesTemp.AddRange(datas[i].vertices);
                uvsTemp.AddRange(datas[i].uv);
            }
            mesh.vertices = verticesTemp.ToArray();
            mesh.uv = uvsTemp.ToArray();

            // ����unity�ϲ�֮���submesh index���
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
