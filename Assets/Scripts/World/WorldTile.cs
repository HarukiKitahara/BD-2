using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;

namespace MyProject.World
{
    [System.Serializable]
    public class WorldTileDataPersistance
    {
        public int worldTileID;
        public int altitude;
        public WorldTileDataPersistance(int worldTileID, int altitude)
        {
            this.worldTileID = worldTileID;
            this.altitude = altitude;
        }
    }
    /// <summary>
    /// 世界地图上Tile的实例。
    /// TODO: 目前除了数据没别的功能，以后会有城镇、生态群系等更多数据和方法。
    /// </summary>
    public class WorldTile
    {
        public readonly int worldTileID;
        public readonly int altitude;
        public WorldTile(int worldTileID, int altitude)
        {
            this.worldTileID = worldTileID;
            this.altitude = altitude;
        }
        public WorldTile(WorldTileDataPersistance worldTileDataPersistance) : 
            this(worldTileDataPersistance.worldTileID, worldTileDataPersistance.altitude) { }

        public WorldTileDataPersistance DoDataPersistance()
        {
            return new WorldTileDataPersistance(worldTileID, altitude);
        }
    }
}
