using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using System;

namespace MyProject.World
{
    public class World
    {
        public readonly int size;      // 正方形地块
        private const int MAX_SIZE = 128;
        public readonly WorldTileDatabaseAsset[,] worldTileDatas;

        public World(int size)
        {
            this.size = (size > MAX_SIZE) ? MAX_SIZE : size;
            worldTileDatas = new WorldTileDatabaseAsset[this.size, this.size];
        }

        public void IterateAllTiles(Action<int,int> action)
        {
            for(int i = 0; i < size; i++)
            {
                for(int j = 0; j < size; j++)
                {
                    action.Invoke(i, j);
                }
            }
        }
        public bool IsValidCoordinate(int x, int y)
        {
            return IsValidPartialCoordinate(x) && IsValidPartialCoordinate(y);
            bool IsValidPartialCoordinate(int x)
            {
                return x >= 0 && x < size;
            }
        }
        public void SetTile(WorldTileDatabaseAsset worldTileDatabaseAsset, int x, int y)
        {
            if (IsValidCoordinate(x, y))
            {
                worldTileDatas[x, y] = worldTileDatabaseAsset;
            }
        }
    }
}
