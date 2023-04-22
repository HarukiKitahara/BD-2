using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.World
{
    public enum EVoxelSurface
    {
        up, left, forward, right, back, down
    }
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
    /// <summary>
    /// 通用VoxelMesh数据及生成法
    /// </summary>
    public class VoxelMeshData
    {
        public const float UV_OFFSET = 0.005f;    // 狗屎，不加这个小偏移的话，最终效果会有“接缝”，所以得多取样一点点
        public static Vector2 uvOffset0 = new Vector2(+UV_OFFSET, +UV_OFFSET);
        public static Vector2 uvOffset1 = new Vector2(+UV_OFFSET, -UV_OFFSET);
        public static Vector2 uvOffset2 = new Vector2(-UV_OFFSET, -UV_OFFSET);
        public static Vector2 uvOffset3 = new Vector2(-UV_OFFSET, +UV_OFFSET);
        public readonly List<Vector3> vertices = new();
        public readonly List<int> triangles = new();
        public readonly List<Vector2> uv = new();
        public readonly List<Vector3> colliderVertices = new();
        public readonly List<int> colliderTriangles = new();        
        public int TrisCount { get; private set; }
        public void AddQuadInfo(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, Vector3 vertex4, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4)
        {
            AddTriangleInfo(vertex1, vertex2, vertex3, uv1, uv2, uv3);
            AddTriangleInfo(vertex1, vertex3, vertex4, uv1, uv3, uv4);
        }
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
            TrisCount++;
        }
        /// <summary>
        /// 将体素某个表面加入Mesh
        /// </summary>
        /// <param name="voxelOrigin"> 体素Loacl原点坐标（000~111）</param>
        /// <param name="uvIndex"> uv在整张Texture中的index坐标</param>
        /// <param name="uvSize"> uv的Texture一共多少个元素</param>
        /// <param name="surface"> 法线朝向 </param>
        public void AddVoxelSurface(Vector3 voxelOrigin, Vector2Int uvIndex, int uvSize, EVoxelSurface surface)
        {
            Vector2 uvIndexToCalculate = uvIndex;
            float uvSizeToCalculate = uvSize;
            switch (surface)
            {
                case EVoxelSurface.up:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(0, 1, 0),
                        voxelOrigin + new Vector3(0, 1, 1),
                        voxelOrigin + new Vector3(1, 1, 1),
                        voxelOrigin + new Vector3(1, 1, 0),

                        uvIndexToCalculate / uvSizeToCalculate + uvOffset0,
                        (uvIndexToCalculate + new Vector2Int(0, 1)) / uvSizeToCalculate + uvOffset1,
                        (uvIndexToCalculate + new Vector2Int(1, 1)) / uvSizeToCalculate + uvOffset2,
                        (uvIndexToCalculate + new Vector2Int(1, 0)) / uvSizeToCalculate + uvOffset3
                        );
                    break;
                case EVoxelSurface.left:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(0, 0, 1),
                        voxelOrigin + new Vector3(0, 1, 1),
                        voxelOrigin + new Vector3(0, 1, 0),
                        voxelOrigin + new Vector3(0, 0, 0),

                        uvIndexToCalculate / uvSizeToCalculate + uvOffset0,
                        (uvIndexToCalculate + new Vector2Int(0, 1)) / uvSizeToCalculate + uvOffset1,
                        (uvIndexToCalculate + new Vector2Int(1, 1)) / uvSizeToCalculate + uvOffset2,
                        (uvIndexToCalculate + new Vector2Int(1, 0)) / uvSizeToCalculate + uvOffset3
                        );
                    break;
                case EVoxelSurface.forward:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(1, 0, 1),
                        voxelOrigin + new Vector3(1, 1, 1),
                        voxelOrigin + new Vector3(0, 1, 1),
                        voxelOrigin + new Vector3(0, 0, 1),

                        uvIndexToCalculate / uvSizeToCalculate + uvOffset0,
                        (uvIndexToCalculate + new Vector2Int(0, 1)) / uvSizeToCalculate + uvOffset1,
                        (uvIndexToCalculate + new Vector2Int(1, 1)) / uvSizeToCalculate + uvOffset2,
                        (uvIndexToCalculate + new Vector2Int(1, 0)) / uvSizeToCalculate + uvOffset3
                        );
                    break;
                case EVoxelSurface.right:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(1, 0, 0),
                        voxelOrigin + new Vector3(1, 1, 0),
                        voxelOrigin + new Vector3(1, 1, 1),
                        voxelOrigin + new Vector3(1, 0, 1),

                        uvIndexToCalculate / uvSizeToCalculate + uvOffset0,
                        (uvIndexToCalculate + new Vector2Int(0, 1)) / uvSizeToCalculate + uvOffset1,
                        (uvIndexToCalculate + new Vector2Int(1, 1)) / uvSizeToCalculate + uvOffset2,
                        (uvIndexToCalculate + new Vector2Int(1, 0)) / uvSizeToCalculate + uvOffset3
                        );
                    break;
                case EVoxelSurface.back:
                    AddQuadInfo(
                        voxelOrigin + new Vector3(0, 0, 0),
                        voxelOrigin + new Vector3(0, 1, 0),
                        voxelOrigin + new Vector3(1, 1, 0),
                        voxelOrigin + new Vector3(1, 0, 0),

                        uvIndexToCalculate / uvSizeToCalculate + uvOffset0,
                        (uvIndexToCalculate + new Vector2Int(0, 1)) / uvSizeToCalculate + uvOffset1,
                        (uvIndexToCalculate + new Vector2Int(1, 1)) / uvSizeToCalculate + uvOffset2,
                        (uvIndexToCalculate + new Vector2Int(1, 0)) / uvSizeToCalculate + uvOffset3
                        );
                    break;
                default:
                    throw new System.Exception("特殊情况还没做");
            }
        }
    }
}
