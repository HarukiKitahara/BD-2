using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyProject.VoxelEngine
{
    #region SaveLoad
    [System.Serializable]
    public class VoxelSaveData
    {
        public int voxelID;
        public int altitude;
        public VoxelSaveData(int voxelID, int altitude)
        {
            this.voxelID = voxelID;
            this.altitude = altitude;
        }
    }
    #endregion
    public class Voxel
    {
        public readonly int altitude;
        public readonly int voxelID;
        public Voxel(int voxelID, int altitude)
        {
            this.voxelID = voxelID;
            this.altitude = altitude;   
        }
        #region SaveLoad

        public Voxel(VoxelSaveData saveData) : this(saveData.voxelID, saveData.altitude) { }
        public virtual VoxelSaveData GetSaveData()
        {
            return new VoxelSaveData(voxelID, altitude);
        }
        #endregion
    }
}
