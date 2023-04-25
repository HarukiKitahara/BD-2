using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;
using System;
using System.Linq;
using Random = UnityEngine.Random;
using MyProject.Utils;
using MyProject.VoxelEngine;

namespace MyProject.World
{
    #region SaveLoad
    [Serializable]
    public class WorldSaveData : VoxelSpaceSaveData
    {
        public int seaLevel;
        public WorldSaveData(int seaLevel, VoxelSpaceSaveData voxelSpaceSaveData) : base(voxelSpaceSaveData.length, voxelSpaceSaveData.voxelSaveDatas)
        {
            this.seaLevel = seaLevel;
        }
    }
    #endregion
    public class World : VoxelSpace
    {
        public readonly int seaLevel;
        public World(int length, int seaLevel) : base(length)
        {
            this.seaLevel = seaLevel;
        }

        /// <summary> 随机获得一个海平面以上的地块 </summary>
        public int GetRandomTileIndexAboveSeaLevel()
        {
            var ans = IterateWithRandomStartIndexAndBreakCondition(index => voxels[index].altitude >= seaLevel);
            if (ans == -1) throw new Exception("世界、沉没了！");
            return ans;
        }
        #region SaveLoad
        public World(WorldSaveData worldSaveData) : base(worldSaveData)
        {
            seaLevel = worldSaveData.seaLevel;
        }
        public new WorldSaveData GetSaveData()
        {
            return new WorldSaveData(seaLevel, base.GetSaveData());
        }
        #endregion
    }
}
