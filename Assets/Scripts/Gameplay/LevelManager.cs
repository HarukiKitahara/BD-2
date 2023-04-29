using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.VoxelEngine;
namespace MyProject.Gameplay
{
    public class LevelManager : MonoBehaviour
    {
        public VoxelSpace Space { get; private set; }
        [SerializeField] private MeshFilter _meshFilter;
        [SerializeField] private MeshCollider _meshCollider;
        [field: SerializeField] public GameObject CharacterGO { get; private set; }
        private void Start()
        {
            Space = VoxelSpaceGenerator.GenerateFlatSpace(50);
            var voxelMeshData = VoxelSpaceMeshGenerator.GenerateMesh(Space);
            _meshFilter.mesh = VoxelMeshHelper.BuildMesh(voxelMeshData.opaque);
            _meshCollider.sharedMesh = VoxelMeshHelper.BuildMesh(voxelMeshData.collider);

            CharacterGO.SetActive(true);
            CharacterGO.transform.position = Space.GetVoxelGroundCenterPositionByIndex(25, 25);
        }
    }
}
