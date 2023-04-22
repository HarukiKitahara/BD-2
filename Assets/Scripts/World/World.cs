using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using System;
using System.Linq;
using Random = UnityEngine.Random;
using MyProject.Utils;

namespace MyProject.World
{
    [Serializable]
    public class WorldDataPersistace
    {
        public int length;
        public int seaLevel;
        public WorldTileDataPersistance[] worldTileDataPersistances;
        public WorldDataPersistace(int length, int seaLevel, WorldTileDataPersistance[] worldTileDataPersistances)
        {
            this.length = length;
            this.seaLevel = seaLevel;
            this.worldTileDataPersistances = worldTileDataPersistances;
        }
    }
    public class World
    {
        private const int MAX_LENGTH = 512;     // ̫�����ڴ��ը
        public readonly int length;     // �����α߳�
        public readonly int tileCount;  // �ܸ�������ֻ�ǲ���ÿ�����㣬���Ծͻ�������
        public readonly int seaLevel;
        public readonly WorldTile[] worldTiles;     // ��ѧ�����ѡ�һά����󷨺ã�������Linq�ĸ�����չ������
        public World(int length, int seaLevel)
        {
            this.length = (length > MAX_LENGTH) ? MAX_LENGTH : length;
            tileCount = this.length * this.length;
            this.seaLevel = seaLevel;
            worldTiles = new WorldTile[tileCount];  // �������������ɣ�Ҫ�ⲿ��������ʱ���Լ��Ƿ����
        }
        public World(WorldDataPersistace worldDataPersistace) : this(worldDataPersistace.length, worldDataPersistace.seaLevel) 
        {
            IterateAllCoordinates(index => worldTiles[index] = new WorldTile(worldDataPersistace.worldTileDataPersistances[index])); 
        }

        /// <summary>
        /// ���߷��������ٱ�����������
        /// </summary>
        public void IterateAllCoordinates(Action<int,int> action)
        {
            for(int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    action.Invoke(i, j);
                }
            }
        }
        public void IterateAllCoordinates(Action<int> action)
        {
            for (int i = 0; i < tileCount; i++)
            {
                action.Invoke(i);
            }
        }
        public bool IsValidCoordinate(int x, int y)
        {
            return IsValidPartialCoordinate(x) && IsValidPartialCoordinate(y);
            bool IsValidPartialCoordinate(int x)
            {
                return x >= 0 && x < length;
            }
        }
        public WorldTile GetWorldTileAt(int x, int y)
        {
            if (!IsValidCoordinate(x, y)) return null;
            return worldTiles[GetIndexAt(x, y)];
        }
        public int GetIndexAt(int x, int y)
        {
            return x + y * length;
        }
        public WorldDataPersistace DoDataPersistance()
        {
            var worldTileDataPersistances = new WorldTileDataPersistance[tileCount];
            IterateAllCoordinates(index => worldTileDataPersistances[index] = worldTiles[index].DoDataPersistance());
            return new WorldDataPersistace(length, seaLevel, worldTileDataPersistances);
        }
    }
}
