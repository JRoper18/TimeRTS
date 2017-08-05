using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game
{
    class MapState
    {
        private GameObject[, ,] map;
        public MapState(Vector3 size){
            map = new GameObject[(int)size.X,(int)size.Y,(int)size.Z];
            for(int x = 0; x<size.X; x++)
            {
                for(int y = 0; y<size.Y; y++)
                {
                    this.map[x, y, 0] = new IsometricGameObject(new Vector3(x, y, 0));
                }
            }
        }
        public GameObject getTileAtPosition(Vector3 position)
        {
            return map[(int)position.X,(int)position.Y, (int)position.Z];
        }
        public Vector3 getSize()
        {
            return new Vector3(map.GetLength(0), map.GetLength(1), map.GetLength(2));
        }
    }
}
