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
    /// �����ͼ��Tile��ʵ����
    /// TODO: Ŀǰ��������û��Ĺ��ܣ��Ժ���г�����̬Ⱥϵ�ȸ������ݺͷ�����
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
