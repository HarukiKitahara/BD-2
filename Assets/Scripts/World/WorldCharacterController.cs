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
    public class WorldCharacterControllerDataPersistance
    {
        public int tileIndex;
        public EDirection direction;
    }
    public class WorldCharacterController
    {
        private readonly World _world;
        private readonly Transform _transform;
        private readonly WorldCharacterControllerDataPersistance _dataPersistance;
        private int TileIndex { get => _dataPersistance.tileIndex; set => _dataPersistance.tileIndex = value; }
        private EDirection Direction { get => _dataPersistance.direction; set => _dataPersistance.direction = value; }
        public WorldCharacterController(World world, Transform transform, int tileIndex, EDirection direction = EDirection.north)
        {
            if (world == null || transform == null) throw new System.Exception("�ܰ�������ô˦����null");
            _world = world;
            _transform = transform;
            _dataPersistance = new WorldCharacterControllerDataPersistance();
            TileIndex = tileIndex;
            Direction = direction;
            UpdateTransform();
        }
        public WorldCharacterController(World world, Transform transform, WorldCharacterControllerDataPersistance dataPersistance)
        {
            if (world == null || transform == null) throw new System.Exception("�ܰ�������ô˦����null");
            _world = world;
            _transform = transform;
            if (dataPersistance == null) throw new System.Exception("�ܰ�������ô˦����null");
            _dataPersistance = dataPersistance;
            UpdateTransform();
        }
        private void UpdateTransform()
        {
            _transform.position = _world.GetTileGroundCenterPositionByIndex(TileIndex);
            // TODO��_transform.rotation
            // ��Ҫ˳��A*Ѱ·һ���ƣ���ʱ������ľ���һ���������С�
        }
        public WorldCharacterControllerDataPersistance DoDataPersistance()
        {
            return _dataPersistance;
        }

        // TODO������Ӧ���빦�ܽ���
        public void MoveTo(int index)
        {
            if (TileIndex == index) return;
            TileIndex = index;
            _transform.DOMove(_world.GetTileGroundCenterPositionByIndex(index), 1f);
        }
    }
}
