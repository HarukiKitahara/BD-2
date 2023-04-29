using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Database
{
    public enum ERenderCategory { none, opaque, transparent }

    [CreateAssetMenu(fileName = "DatabaseAsset_Voxel_Default", menuName = "MyDatabase/Voxel")]
    [DatabaseKey("Name")]
    public class VoxelDatabaseAsset : DatabaseAssetBase
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public TextureInfo TextureInfo { get; private set; }
        [field: SerializeField] public ERenderCategory RenderCategory { get; private set; }
        [field: SerializeField] public bool HasCollider { get; private set; }
    }
    [System.Serializable]
    public struct TextureInfo
    {
        /// <summary> 水平表面的uv坐标 </summary>
        public Vector2Int Up;
        /// <summary> 紧挨水平表面侧面 </summary>
        public Vector2Int Side;
        /// <summary> 再往下就是简单材质循环 </summary>
        public Vector2Int RepeatingSide;
    }
}
