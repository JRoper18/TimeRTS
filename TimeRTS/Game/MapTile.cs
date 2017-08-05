using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game
{   
    enum MapTileType
    {
        GRASS,
        WATER
    }
    class MapTile : GameObject
    {
        public MapTileType type;
        public MapTile(Vector3 position, MapTileType type = MapTileType.GRASS) : base(position, "GrassTile"){
            this.type = type;
        }
       
    }
}
