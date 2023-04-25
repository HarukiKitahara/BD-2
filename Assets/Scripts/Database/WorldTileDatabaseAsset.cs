using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Database
{
    [CreateAssetMenu(fileName = "DatabaseAsset_WorldTile_Default", menuName = "MyDatabase/WorldTile")]
    [DatabaseKey("Name")]
    public class WorldTileDatabaseAsset : DatabaseAssetBase
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public string Description { get; private set; }
        [field: SerializeField] public float SpawnWeight { get; private set; }
        [field: SerializeField] public TextureInfo TextureInfo { get; private set; }
    }
    [System.Serializable]
    public struct TextureInfo
    {
        /// <summary> ˮƽ�����uv���� </summary>
        public Vector2Int Up;
        /// <summary> ����ˮƽ������� </summary>
        public Vector2Int Side;
        /// <summary> �����¾��Ǽ򵥲���ѭ�� </summary>
        public Vector2Int RepeatingSide;
    }
}
