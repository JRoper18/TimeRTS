using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game
{
    /// <summary>
    /// The map at on a single turn. The map should NOT be able to be edited publicly. GameObjects can edit own their position, which will update in the GameState. 
    /// </summary>
    class MapState
    {
        private GameObject[, ,] map;
        /// <summary>
        /// The map at on a single turn. The map should NOT be able to be edited publicly. GameObjects can edit own their position, which will update in the GameState. 
        /// </summary>
        /// <param name="mapArray">An array of GameObjects to use. </param>
        public MapState(GameObject[,,] mapArray) {
            this.map = mapArray;
        }
        public MapState(Vector3 size){
            Random random = new Random();
            map = new GameObject[(int)size.X,(int)size.Y,(int)size.Z];
            for(int x = 0; x<size.X; x++)
            {
                for(int y = 0; y<size.Y; y++)
                {
                    if(x == 0) {
                        this.map[x, y, 1] = GameObjectFactory.CreateGameObject(GameObjectType.TILE_STAIR_GRASS, new Vector3(x, y, 1), Direction.NORTHWEST);
                    }
                    if(x == 7) {
                        this.map[x, y, 1] = GameObjectFactory.CreateGameObject(GameObjectType.TILE_STAIR_GRASS, new Vector3(x, y, 1), Direction.SOUTHEAST);
                    }
                    this.map[x, y, 0] = GameObjectFactory.CreateGameObject(GameObjectType.TILE_GRASS, new Vector3(x, y, 0));
                }
            }
            this.map[4, 4, 1] = GameObjectFactory.CreateGameObject(GameObjectType.UNIT_CAR, new Vector3(4, 4, 1));
        }
        public GameObject getTileAtPosition(Vector3 position)
        {
           return map[(int)position.X,(int)position.Y, (int)position.Z];
        }
        public GameObject[, ,] getMapClone() {
            return (GameObject[, ,]) this.map.Clone();
        }
        public Vector3 getSize()
        {
            return new Vector3(map.GetLength(0), map.GetLength(1), map.GetLength(2));
        }
        public void clearPosition(Vector3 position) {
            this.map[(int)position.X, (int)position.Y, (int)position.Z] = null;
        }
        public void moveGameObject(Vector3 oldPosition, Vector3 newPosition) {
            this.map[(int) newPosition.X, (int) newPosition.Y, (int) newPosition.Z] = this.getTileAtPosition(oldPosition);
            this.clearPosition(oldPosition);
        }
    }
}
