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
    public class World
    {
        public readonly int length;     // �����α߳�
        public readonly int tileCount;  // �ܸ�����
        private const int MAX_LENGTH = 512;
        public readonly WorldTile[] worldTiles;     // ��ѧ�����ѡ�һά����󷨺ã�������Linq�ĸ�����չ������
        public readonly int seaLevel;

        public World(int length, int seaLevel)
        {
            this.length = (length > MAX_LENGTH) ? MAX_LENGTH : length;
            tileCount = this.length * this.length;
            worldTiles = new WorldTile[tileCount];
            this.seaLevel = seaLevel;
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
            return worldTiles[GetTileIndexByCoordinates(x, y)];
        }
        public int GetTileIndexByCoordinates(int x, int y)
        {
            return x + y * length;
        }
    }
}
