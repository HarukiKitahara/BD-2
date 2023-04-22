using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyProject.Database;

namespace MyProject.World
{
    public class WorldTile
    {
        public readonly WorldTileDatabaseAsset databaseAsset;
        //public readonly int coordX, coordY;
        public readonly int altitude;
        public WorldTile(WorldTileDatabaseAsset databaseAsset,int altitude)
        {
            this.databaseAsset = databaseAsset;
            this.altitude = altitude;
        }
    }
}
