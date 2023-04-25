using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * ��Convention��
 * �涨����Quad����ͼ��ʽ��������Σ�
 * 1 - 2
 * | / |
 * 0 - 3
 * ��������˳ʱ�롿������������ηֱ���(0, 1, 2)��(0, 2, 3)
 * Normal����������ֶ��򣬰����ַ������Normalһ�����⣬����������������Quad���档
 */
namespace MyProject.VoxelEngine
{
    /// <summary>
    /// Voxel��λ��Ƿ����涨�����⡱Normal�ķ���
    /// ���ӵ㡿Vector3.up��property���ǳ�����û����switch�ȷ��������жϣ�ֻ���Լ����¶��塣
    /// </summary>
    public enum EVoxelSurface
    {
        up, left, forward, right, back, down
    }
    /// <summary>
    /// MeshData���ݣ���������������Mesh
    /// ֻ��Ӧһ��Material���������κ�SubMesh�������
    /// ʵ���������ܾ���һ��Tile��Ӧһ��VoxelMeshData���͸���ͬ��MeshCollider��Ȼ���ٺϲ��ɴ�Mesh��������Ⱦ��
    /// ���⻹�ṩ���������ɹ���
    /// </summary>
    public class VoxelMeshData
    {
        public const float UV_OFFSET = 0.005f;    // ���пӡ���ѧ�����ѡ���ʺ���������Сƫ�ƵĻ�������Ч�����С��ӷ족�����Եö����ȡ��һ��㡣����
        public static Vector2 uvOffset0 = new Vector2(+UV_OFFSET, +UV_OFFSET);
        public static Vector2 uvOffset1 = new Vector2(+UV_OFFSET, -UV_OFFSET);
        public static Vector2 uvOffset2 = new Vector2(-UV_OFFSET, -UV_OFFSET);
        public static Vector2 uvOffset3 = new Vector2(-UV_OFFSET, +UV_OFFSET);

        public readonly List<Vector3> vertices = new();
        public readonly List<int> triangles = new();
        public readonly List<Vector2> uv = new();     
        /// <summary> ����������������������index���ܳ��ȣ� </summary>
        public int TrisCount { get; private set; }
        /// <summary> ��Quad������������Σ��ֱ�д��MeshData </summary>
        public void AddQuadInfo(Vector3 vertex1, Vector3 vertex2, Vector3 vertex3, Vector3 vertex4, Vector2 uv1, Vector2 uv2, Vector2 uv3, Vector2 uv4)
        {
            AddTriangleInfo(vertex1, vertex2, vertex3, uv1, uv2, uv3);
            AddTriangleInfo(vertex1, vertex3, vertex4, uv1, uv3, uv4);
        }
        
        /// <summary> �����������������uv����ʱ���˳��д��MeshData </summary>
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
            TrisCount++;    // ���ڶ����uv���޷��ظ����ã�������Ȼ�����ζ����кܶ��غϣ�������ֻ��������д����١�
        }
        
        /// <summary>
        /// ��Voxelĳ������ģ�Quad��д��MeshData��ע�⿴����������ʹ�ù淶
        /// </summary>
        /// <param name="voxelOrigin"> ����Local����㡿���꣨ע�ⲻ�����ĵ㣬�����ԡ��²����½ǡ�Ϊ000�����ϲ����Ͻǡ�Ϊ111����ģ�</param>
        /// <param name="uvIndex"> uv������Texture�е�index���꣨��0������</param>
        /// <param name="uvSize"> uv��Textureһ��һ�����ٸ�Ԫ�أ���Texture��ʽ��Ҫ��һ���������Σ��Ҿ��Ȼ���Grid��</param>
        /// <param name="surface"> ���淨�߳��� </param>
        public void AddVoxelSurface(Vector3 voxelOrigin, Vector2 uvIndex, float uvSize, EVoxelSurface surface)
        {
            // ���пӡ�uvSize��unIndex�����϶�Ӧ����������������������������������ԾͶ���ת��float��
            switch (surface)
            {
                case EVoxelSurface.up:  // ����ͷ�����˳��д��Quad�����uv��Ϣ����ͬ
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
                    throw new System.Exception("���������û����Ӧ�ò��������·�������ɣ�������");
            }
        }
    }
}
