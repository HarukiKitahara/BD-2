using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;
namespace MyProject.VoxelEngine
{
    #region SaveLoad
    [Serializable]
    public class VoxelSpaceSaveData
    {
        public int length;
        public VoxelSaveData[] voxelSaveDatas;
        public VoxelSpaceSaveData(int length, VoxelSaveData[] voxelSaveDatas)
        {
            this.length = length;
            this.voxelSaveDatas = voxelSaveDatas;
        }
    }
    #endregion
    public class VoxelSpace
    {
        private const int MAX_LENGTH = 512;     // 太大了内存会炸
        public readonly int length;     // 正方形边长
        public readonly int voxelCount;  // 总格子数。只是不想每次重算，所以就缓存下来
        public readonly Voxel[] voxels;     // 【学到虚脱】一维数组大法好，可以用Linq的各种拓展方法。
        public VoxelSpace(int length)
        {
            this.length = (length > MAX_LENGTH) ? MAX_LENGTH : length;
            voxelCount = this.length * this.length;
            voxels = new Voxel[voxelCount];  // 不负责世界生成，要外部控制生成时机以及是否读档
        }
        /// <summary> 工具方法，快速遍历所有坐标 </summary>
        public void IterateAllCoordinates(Action<int, int> action)
        {
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    action.Invoke(i, j);
                }
            }
        }
        public void IterateAllCoordinates(Action<int> action)
        {
            for (int i = 0; i < voxelCount; i++)
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
        /// <summary> 检测坐标是否在Space边界 </summary>
        public bool IsOnEdge(int x, int y)
        {
            return IsPartialCoordinateOnEdge(x) || IsPartialCoordinateOnEdge(y);
            bool IsPartialCoordinateOnEdge(int x)
            {
                return x == 0 || x == length-1;
            }
        }
        /// <summary> 检测坐标是否在Space边界 </summary>
        public bool IsOnEdge(int index)
        {
            var coord = GetCoordinateByIndex(index);
            return IsOnEdge(coord.Item1, coord.Item2);
        }
        public Voxel GetVoxelAt(int x, int y)
        {
            if (!IsValidCoordinate(x, y)) return null;
            return voxels[GetIndexAt(x, y)];
        }
        public int GetIndexAt(int x, int y)
        {
            if (!IsValidCoordinate(x, y)) throw new Exception($"坐标越界！World没坐标({x}, {y}), 边长只有{length}");
            return x + y * length;
        }
        public (int, int) GetCoordinateByIndex(int index)
        {
            return (index % length, index / length);
        }

        /// <summary> 含退出条件的随机起点遍历 </summary>
        /// <param name="predicate">返回true就退出</param>
        public int IterateWithRandomStartIndexAndBreakCondition(Func<int, bool> predicate)
        {
            var randomIndex = Random.Range(0, voxelCount);
            var randomDirection = Random.value < 0.5f ? -1 : 1;
            var currentIndex = randomIndex;
            do
            {
                if (predicate.Invoke(randomIndex)) return randomIndex;
                randomIndex = (randomIndex + randomDirection + voxelCount) % voxelCount;  // 【有坑】学到虚脱：c#负数modulo正数还是负数，有点傻逼
            } while (currentIndex != randomIndex);
            return -1;
        }
        /// <summary> 获取地块上表面中心点坐标(+0.5, +1, +0.5) </summary>
        public Vector3 GetVoxelGroundCenterPositionByIndex(int index)
        {
            var originPosition = GetVoxelOriginPositionByIndex(index);
            return new Vector3(originPosition.x + 0.5f, originPosition.y + 1f, originPosition.z + 0.5f);
        }
        public Vector3 GetVoxelGroundCenterPositionByIndex(int x, int y)
        {
            return GetVoxelGroundCenterPositionByIndex(GetIndexAt(x, y));
        }
        /// <summary> 获取地块Local原点坐标(0, 0, 0) </summary>
        public Vector3 GetVoxelOriginPositionByIndex(int index)
        {
            var Coord = GetCoordinateByIndex(index);
            return new Vector3(Coord.Item1, voxels[index].altitude, Coord.Item2);
        }
        /// <summary> 输入空间坐标，输出打到的地块index </summary>
        public int GetIndexByPosition(Vector3 position)
        {
            return GetIndexAt((int)position.x, (int)position.z);
        }
        #region
        public VoxelSpace(VoxelSpaceSaveData voxelSpaceSaveData) : this(voxelSpaceSaveData.length)
        {
            IterateAllCoordinates(index => voxels[index] = new Voxel(voxelSpaceSaveData.voxelSaveDatas[index]));
        }
        public VoxelSpaceSaveData GetSaveData()
        {
            var voxelSaveDatas = new VoxelSaveData[voxelCount];
            IterateAllCoordinates(index => voxelSaveDatas[index] = voxels[index].GetSaveData());
            return new VoxelSpaceSaveData(length, voxelSaveDatas);
        }
        #endregion
    }
}
