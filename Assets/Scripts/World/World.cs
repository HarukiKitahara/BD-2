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
        private const int MAX_LENGTH = 512;     // 太大了内存会炸
        public readonly int length;     // 正方形边长
        public readonly int tileCount;  // 总格子数。只是不想每次重算，所以就缓存下来
        public readonly int seaLevel;
        public readonly WorldTile[] worldTiles;     // 【学到虚脱】一维数组大法好，可以用Linq的各种拓展方法。
        public World(int length, int seaLevel)
        {
            this.length = (length > MAX_LENGTH) ? MAX_LENGTH : length;
            tileCount = this.length * this.length;
            this.seaLevel = seaLevel;
            worldTiles = new WorldTile[tileCount];  // 不负责世界生成，要外部控制生成时机以及是否读档
        }
        public World(WorldDataPersistance worldDataPersistace) : this(worldDataPersistace.length, worldDataPersistace.seaLevel) 
        {
            IterateAllCoordinates(index => worldTiles[index] = new WorldTile(worldDataPersistace.worldTileDataPersistances[index])); 
        }

        /// <summary>
        /// 工具方法，快速遍历所有坐标
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
        /// <summary> 随机获得一个海平面以上的地块 </summary>
        public int GetRandomTileIndexAboveSeaLevel()
        {
            var ans = IterateWithRandomStartIndexAndBreatCondition(index => worldTiles[index].altitude >= seaLevel);
            if (ans == -1) throw new Exception("世界、沉没了！");
            return ans;
        }
        /// <summary> 含退出条件的随机起点遍历 </summary>
        /// <param name="predicate">返回true就退出</param>
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
