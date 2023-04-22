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
        public World(WorldDataPersistace worldDataPersistace) : this(worldDataPersistace.length, worldDataPersistace.seaLevel) 
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
        public WorldDataPersistace DoDataPersistance()
        {
            var worldTileDataPersistances = new WorldTileDataPersistance[tileCount];
            IterateAllCoordinates(index => worldTileDataPersistances[index] = worldTiles[index].DoDataPersistance());
            return new WorldDataPersistace(length, seaLevel, worldTileDataPersistances);
        }
    }
}
