using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.Database
{
    [CreateAssetMenu(fileName = "WorldGenerationDatabaseAsset_DEFAULT", menuName = "MyDatabase/WorldGeneration")]
    public class WorldGenerationDatabaseAsset : DatabaseAssetBase
    {
        [SerializeField] private int _size = 100;
        public int Size => _size;
        [SerializeField] private int _seaLevel = 20;
        public int SeaLevel => _seaLevel;
        [SerializeField] private int _maxAltitude = 50;
        public int MaxAltitude => _maxAltitude;
        [SerializeField] private float _noiseScale = 0.05f;
        public float NoiseScale => _noiseScale;
        [SerializeField] private float _edgeRatio = 0.2f;
        public float EdgeRatio => _edgeRatio;
    }
}
