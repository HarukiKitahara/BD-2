using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace MyProject.Gameplay
{
    public class WorldGenerator : MonoBehaviour
    {
        [SerializeField]
        private int _width = 100;
        [SerializeField]
        private GameObject _groundBlockPrefab;

        private Transform _groundRootTransform;
        private void Start()
        {
            _groundRootTransform = transform.Find("Ground");
            if (_groundRootTransform == null) new GameObject("Ground").transform.SetParent(_groundRootTransform);

            GenerateGround();
        }

        private void GenerateGround()
        {
            for (int i = 0; i < _width; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    Instantiate(_groundBlockPrefab, new Vector3(i, 0, j), Quaternion.identity, _groundRootTransform);
                }
            }
        }
    }
}
