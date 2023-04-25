using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * 【Convention】
 * 规定所有Quad按下图方式拆成三角形：
 * 1 - 2
 * | / |
 * 0 - 3
 * 两个按【顺时针】方向定义的三角形分别是(0, 1, 2)和(0, 2, 3)
 * Normal朝向符合左手定则，按这种方法拆分Normal一定朝外，这样才能正常看到Quad表面。
 */
namespace MyProject.VoxelEngine
{
    /// <summary>
    /// Voxel方位表记法，规定“向外”Normal的方向。
    /// 【坑点】Vector3.up等property不是常数，没法用switch等方法快速判断，只能自己重新定义。
    /// </summary>
    public enum EVoxelSurface
    {
        up, left, forward, right, back, down
    }
    /// <summary>
    /// MeshData数据，用来生成真正的Mesh
    /// 只对应一种Material，不考虑任何SubMesh的情况。
    /// 实操下来可能就是一种Tile对应一个VoxelMeshData，送给不同的MeshCollider，然后再合并成大Mesh，用来渲染。
    /// 此外还提供便利的生成工具
    /// </summary>
    public class VoxelMeshData
    {
        public const float UV_OFFSET = 0.005f;    // 【有坑】【学到虚脱】狗屎，不加这个小偏移的话，最终效果会有“接缝”，所以得额外多取样一点点。。。
        public static Vector2 uvOffset0 = new Vector2(+UV_OFFSET, +UV_OFFSET);
        public static Vector2 uvOffset1 = new Vector2(+UV_OFFSET, -UV_OFFSET);
        public static Vector2 uvOffset2 = new Vector2(-UV_OFFSET, -UV_OFFSET);
        public static Vector2 uvOffset3 = new Vector2(-UV_OFFSET, +UV_OFFSET);

        public readonly List<Vector3> vertices = new();
        public readonly List<int> triangles = new();
        public readonly List<Vector2> uv = new();     
        /// <summary> 三角形总数，不是三角形index的总长度！ </summary>
        public int TrisCount { get; private set; }
        /// <summary> 将Quad拆成两个三角形，分别写入MeshData </summary>
        public void AddQuadInfo(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, Vector3 vertex4, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4)
        {
            AddTriangleInfo(vertex1, vertex2, vertex3, uv1, uv2, uv3);
            AddTriangleInfo(vertex1, vertex3, vertex4, uv1, uv3, uv4);
        }
        
        /// <summary> 将三角形三个顶点和uv以逆时针的顺序写入MeshData </summary>
        public void AddTriangleInfo(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, Vector2 uv1, Vector2 uv2, Vector2 uv3)
        {
            vertices.Add(vertex1);
            vertices.Add(vertex2);
            vertices.Add(vertex3);
            uv.Add(uv1);
            uv.Add(uv2);
            uv.Add(uv3);
            triangles.Add(TrisCount * 3);
            triangles.Add(TrisCount * 3 + 1);
            triangles.Add(TrisCount * 3 + 2);
            TrisCount++;    // 由于顶点和uv都无法重复利用，所以虽然三角形顶点有很多重合，但还是只能来多少写入多少。
        }
        
        /// <summary>
        /// 将Voxel某个表面的（Quad）写入MeshData。注意看各个参数的使用规范
        /// </summary>
        /// <param name="voxelOrigin"> 体素Local【零点】坐标（注意不是中心点，而是以“下层左下角”为000，“上层右上角”为111定义的）</param>
        /// <param name="uvIndex"> uv在整张Texture中的index坐标（从0计数）</param>
        /// <param name="uvSize"> uv的Texture一行一共多少个元素（对Texture格式有要求，一定是正方形，且均匀划分Grid）</param>
        /// <param name="surface"> 表面法线朝向 </param>
        public void AddVoxelSurface(Vector3 voxelOrigin, Vector2 uvIndex, float uvSize, EVoxelSurface surface)
        {
            // 【有坑】uvSize和unIndex理论上都应该是整数，但是整数相除还是整数，所以就都先转成float了
            switch (surface)
            {
                case EVoxelSurface.up:  // 按开头定义的顺序写入Quad顶点和uv信息，下同
                    AddQuadInfo(
                        voxelOrigin + new Vector3(0, 1, 0),
                        voxelOrigin + new Vector3(0, 1, 1),
                        voxelOrigin + new Vector3(1, 1, 1),
                        voxelOrigin + new Vector3(1, 1, 0),

                        uvIndex / uvSize + uvOffset0,
                        (uvIndex + new Vector2Int(0, 1)) / uvSize + uvOffset1,
                        (uvIndex + new Vector2Int(1, 1)) / uvSize + uvOffset2,
                        (uvIndex + new Vector2Int(1, 0)) / uvSize + uvOffset3
                        );
                    break;
                case EVoxelSurface.left:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(0, 0, 1),
                        voxelOrigin + new Vector3(0, 1, 1),
                        voxelOrigin + new Vector3(0, 1, 0),
                        voxelOrigin + new Vector3(0, 0, 0),

                        uvIndex / uvSize + uvOffset0,
                        (uvIndex + new Vector2Int(0, 1)) / uvSize + uvOffset1,
                        (uvIndex + new Vector2Int(1, 1)) / uvSize + uvOffset2,
                        (uvIndex + new Vector2Int(1, 0)) / uvSize + uvOffset3
                        );
                    break;
                case EVoxelSurface.forward:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(1, 0, 1),
                        voxelOrigin + new Vector3(1, 1, 1),
                        voxelOrigin + new Vector3(0, 1, 1),
                        voxelOrigin + new Vector3(0, 0, 1),

                        uvIndex / uvSize + uvOffset0,
                        (uvIndex + new Vector2Int(0, 1)) / uvSize + uvOffset1,
                        (uvIndex + new Vector2Int(1, 1)) / uvSize + uvOffset2,
                        (uvIndex + new Vector2Int(1, 0)) / uvSize + uvOffset3
                        );
                    break;
                case EVoxelSurface.right:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(1, 0, 0),
                        voxelOrigin + new Vector3(1, 1, 0),
                        voxelOrigin + new Vector3(1, 1, 1),
                        voxelOrigin + new Vector3(1, 0, 1),

                        uvIndex / uvSize + uvOffset0,
                        (uvIndex + new Vector2Int(0, 1)) / uvSize + uvOffset1,
                        (uvIndex + new Vector2Int(1, 1)) / uvSize + uvOffset2,
                        (uvIndex + new Vector2Int(1, 0)) / uvSize + uvOffset3
                        );
                    break;
                case EVoxelSurface.back:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(0, 0, 0),
                        voxelOrigin + new Vector3(0, 1, 0),
                        voxelOrigin + new Vector3(1, 1, 0),
                        voxelOrigin + new Vector3(1, 0, 0),

                        uvIndex / uvSize + uvOffset0,
                        (uvIndex + new Vector2Int(0, 1)) / uvSize + uvOffset1,
                        (uvIndex + new Vector2Int(1, 1)) / uvSize + uvOffset2,
                        (uvIndex + new Vector2Int(1, 0)) / uvSize + uvOffset3
                        );
                    break;
                default:
                    throw new System.Exception("特殊情况还没做（应该不会有正下方的情况吧）。。。");
            }
        }
    }
}
