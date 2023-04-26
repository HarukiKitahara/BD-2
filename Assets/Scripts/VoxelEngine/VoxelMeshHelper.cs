using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyProject.VoxelEngine
{
    public static class VoxelMeshHelper
    { 
        /// <summary>
        /// ����Data�����Mesh
        /// </summary>
        public static Mesh BuildMesh(VoxelMeshData data)
        {
            Mesh mesh = new();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;    // �����޸�λ��������ֻ�ֳܷ�һ����Сchunk(65536)
            mesh.vertices = data.vertices.ToArray();
            mesh.uv = data.uv.ToArray();
            
            mesh.SetTriangles(data.triangles.ToArray(), 0);

            mesh.RecalculateNormals();
            return mesh;
        }
        /// <summary>
        /// ����BuildMesh
        /// </summary>
        public static Mesh[] BuildMeshs(VoxelMeshData[] datas)
        {
            var meshs = new Mesh[datas.Length];
            for(int i = 0; i < datas.Length; i++)
            {
                meshs[i] = BuildMesh(datas[i]);
            }
            return meshs;
        }
        /// <summary>
        /// �ϳɴ�mesh
        /// </summary>
        /// <param name="mergeSubmeshes">Ҫ�ϳ�һ����mesh�����Ƿֳ�����submesh</param>
        public static Mesh MergeMesh(bool mergeSubmeshes, params Mesh[] meshs)
        {
            Mesh mesh = new();
            var combineInstances = new CombineInstance[meshs.Length];
            for(int i = 0; i < meshs.Length; i++)
            {
                combineInstances[i] = new CombineInstance();
                combineInstances[i].mesh = meshs[i];
                //if (!mergeSubmeshes) combineInstances[i].subMeshIndex = i;        ���пӡ�������ôд�������subMeshIndex��˼��Ҫ��ԭmesh��Щsubmesh�������
            }
            // ���пӡ�mergeSubmeshesӦ�������������useMetricesһ��Ҫ�ֶ����ó�false������ͻ�������ֵ�matrices��
            mesh.CombineMeshes(combineInstances, mergeSubmeshes, false);   
            return mesh;
        }
    }
}
