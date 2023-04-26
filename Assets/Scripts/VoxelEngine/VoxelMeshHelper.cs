using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
namespace MyProject.VoxelEngine
{
    public static class VoxelMeshHelper
    { 
        /// <summary>
        /// 输入Data，输出Mesh
        /// </summary>
        public static Mesh BuildMesh(VoxelMeshData data)
        {
            Mesh mesh = new();
            mesh.indexFormat = UnityEngine.Rendering.IndexFormat.UInt32;    // 必须修改位数，否则只能分成一个个小chunk(65536)
            mesh.vertices = data.vertices.ToArray();
            mesh.uv = data.uv.ToArray();
            
            mesh.SetTriangles(data.triangles.ToArray(), 0);

            mesh.RecalculateNormals();
            return mesh;
        }
        /// <summary>
        /// 批量BuildMesh
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
        /// 合成大mesh
        /// </summary>
        /// <param name="mergeSubmeshes">要合成一整个mesh，还是分成若干submesh</param>
        public static Mesh MergeMesh(bool mergeSubmeshes, params Mesh[] meshs)
        {
            Mesh mesh = new();
            var combineInstances = new CombineInstance[meshs.Length];
            for(int i = 0; i < meshs.Length; i++)
            {
                combineInstances[i] = new CombineInstance();
                combineInstances[i].mesh = meshs[i];
                //if (!mergeSubmeshes) combineInstances[i].subMeshIndex = i;        【有坑】不能这么写！这里的subMeshIndex意思是要把原mesh哪些submesh抽出来！
            }
            // 【有坑】mergeSubmeshes应该用在这里！另外useMetrices一定要手动设置成false，否则就会用上奇怪的matrices。
            mesh.CombineMeshes(combineInstances, mergeSubmeshes, false);   
            return mesh;
        }
    }
}
