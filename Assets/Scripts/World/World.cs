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
    public class WorldDataPersistance
    {
        public int length;
        public int seaLevel;
        public WorldTileDataPersistance[] worldTileDataPersistances;
        public WorldDataPersistance(int length, int seaLevel, WorldTileDataPersistance[] worldTileDataPersistances)
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
        public World(WorldDataPersistance worldDataPersistace) : this(worldDataPersistace.length, worldDataPersistace.seaLevel) 
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
        public (int, int) GetCoordinateByIndex(int index)
        {
            return (index % length, index / length);
        }
        public WorldDataPersistance DoDataPersistance()
        {
            var worldTileDataPersistances = new WorldTileDataPersistance[tileCount];
            IterateAllCoordinates(index => worldTileDataPersistances[index] = worldTiles[index].DoDataPersistance());
            return new WorldDataPersistance(length, seaLevel, worldTileDataPersistances);
        }
        /// <summary> ������һ����ƽ�����ϵĵؿ� </summary>
        public int GetRandomTileIndexAboveSeaLevel()
        {
            var ans = IterateWithRandomStartIndexAndBreatCondition(index => worldTiles[index].altitude >= seaLevel);
            if (ans == -1) throw new Exception("���硢��û�ˣ�");
            return ans;
        }
        /// <summary> ���˳���������������� </summary>
        /// <param name="predicate">����true���˳�</param>
        public int IterateWithRandomStartIndexAndBreatCondition(Func<int, bool> predicate)
        {
            var randomIndex = Random.Range(0, tileCount);
            var randomDirection = Random.value < 0.5f ? -1 : 1;
            var currentIndex = randomIndex;
            do
            {
                if (predicate.Invoke(randomIndex)) return randomIndex;
                randomIndex = (randomIndex + randomDirection) % tileCount;
            } while (currentIndex != randomIndex);
            return -1;
        }
        public Vector3 GetTileGroundCenterPositionByIndex(int index)
        {
            var Coord = GetCoordinateByIndex(index);
            return new Vector3(Coord.Item1 + 0.5f, worldTiles[index].altitude + 0.5f, Coord.Item2 + 0.5f);
        }
    }
}
