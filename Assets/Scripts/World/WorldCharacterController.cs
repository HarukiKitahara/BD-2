using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace MyProject.World
{
    public enum EDirection
    {
        north, east, south, west
    }
    [System.Serializable]
    public class WorldCharacterControllerSaveData
    {
        public int tileIndex;
        public EDirection direction;
    }
    public class WorldCharacterController
    {
        private readonly World _world;
        private readonly Transform _transform;
        private readonly WorldCharacterControllerSaveData _dataPersistance;
        public int TileIndex { get => _dataPersistance.tileIndex; private set => _dataPersistance.tileIndex = value; }
        public EDirection Direction { get => _dataPersistance.direction; private set => _dataPersistance.direction = value; }
        public WorldCharacterController(World world, Transform transform, int tileIndex, EDirection direction = EDirection.north)
        {
            if (world == null || transform == null) throw new System.Exception("弟阿，你怎么甩给我null");
            _world = world;
            _transform = transform;
            _dataPersistance = new WorldCharacterControllerSaveData();
            TileIndex = tileIndex;
            Direction = direction;
            UpdateTransform();
        }
        public WorldCharacterController(World world, Transform transform, WorldCharacterControllerSaveData dataPersistance)
        {
            if (world == null || transform == null) throw new System.Exception("弟阿，你怎么甩给我null");
            _world = world;
            _transform = transform;
            if (dataPersistance == null) throw new System.Exception("弟阿，你怎么甩给我null");
            _dataPersistance = dataPersistance;
            UpdateTransform();
        }
        private void UpdateTransform()
        {
            _transform.position = _world.GetVoxelGroundCenterPositionByIndex(TileIndex);
            // TODO：_transform.rotation
            // 需要顺着A*寻路一起推，到时候输入的就是一串方向序列。
        }
        public WorldCharacterControllerSaveData DoDataPersistance()
        {
            return _dataPersistance;
        }

        // TODO：表现应该与功能解耦
        public void MoveTo(int index)
        {
            if (TileIndex == index) return;
            TileIndex = index;
            _transform.DOMove(_world.GetVoxelGroundCenterPositionByIndex(index), 1f);
        }
    }
}
