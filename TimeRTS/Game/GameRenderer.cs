using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game
{
    static class GameRenderer
    {
        private const int TILE_WIDTH = 110;
        private const int TILE_HEIGHT = 64;
        public static Dictionary<String, GameObjectTexture> textures = new Dictionary<string, GameObjectTexture>();
        public static void Initialize() {
            textures.Add("GrassTile", new GameObjectTexture("Images/sprite"));
        }
        /// <summary>
        /// Renders the current game onto the screen. 
        /// </summary>
        /// <param name="state">The current state of the game. </param>
        /// <param name="spriteBatch">The spritebatch we are using to draw.</param>
        public static void Render(GameState state, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            MapState mapState = state.getMapAtTime(0);
            GameObject[,,] mapArray = rotateMapArray(mapState.getMapClone(), GraphicsComponent.GetCameraDirection());
            MapState map = new MapState(mapArray);
            Vector3 size = map.getSize();
            int rows = (int)(size.X + size.Y-1);
            for(int i = 0; i<Math.Min(size.X, size.Y); i++) {
                for(int j = 0; j<=i; j++) {
                    int posX = j;
                    int posY = i - j;
                    renderColumn(new Vector2(posX, posY), spriteBatch, map);
                }
            }
            bool isXShortestDimension = (size.X < size.Y);
            int numberOfSameLengthRows = (int) Math.Abs(size.X - size.Y);
            if (isXShortestDimension) {
                for (int i = 0; i < numberOfSameLengthRows; i++) {
                    for (int j = 0; j < size.X; j++) {
                        int posX = j;
                        int posY = i - j + (int) size.X;
                        renderColumn(new Vector2(posX, posY), spriteBatch, map);
                    }
                }
                for (int i = 0; i < size.X - 1; i++) {
                    for (int j = 0; j < size.X - i - 1; j++) {
                        int posY = numberOfSameLengthRows + 1 + i + j;
                        int posX = (int)size.X - j - 1;
                        renderColumn(new Vector2(posX, posY), spriteBatch, map);
                    }
                }
            }
            else {
                for (int i = 0; i < numberOfSameLengthRows; i++) {
                    for (int j = 0; j < size.Y; j++) {
                        int posX = i + j + 1;
                        int posY = (int) size.Y - 1 - j;
                        renderColumn(new Vector2(posX, posY), spriteBatch, map);
                    }
                }
                for(int i = 0; i<size.Y - 1; i++) {
                    for(int j = 0; j < size.Y - i - 1; j++) {
                        int posX = numberOfSameLengthRows + 1 + i + j;
                        int posY = (int) size.Y - j - 1;
                        renderColumn(new Vector2(posX, posY), spriteBatch, map);
                    }
                }
            }
            spriteBatch.End();
        }
        private static GameObject[, ,] rotateMapArray(GameObject[, ,] map, int dir) {
            GameObject[,,] transposedMap = new GameObject[map.GetLength(1), map.GetLength(0), map.GetLength(2)];
            int clockwiseRotations = dir % 4;
            //Transpose. Linear algebra is so cool. 
            for(int x = 0; x<map.GetLength(0); x++) {
                for(int y = 0; y<map.GetLength(1); y++) {
                    for(int z = 0; z<map.GetLength(2); z++) {
                        transposedMap[y, x, z] = map[x, y, z];
                    }
                }
            }
            GameObject[,,] newMap = new GameObject[map.GetLength(1), map.GetLength(0), map.GetLength(2)];
            //Reverse rows if going clockwise, columns if counter-clockwise, nothing if a full 180.  
            for (int x = 0; x < transposedMap.GetLength(0); x++) {
                for (int y = 0; y < transposedMap.GetLength(1); y++) {
                    for (int z = 0; z < transposedMap.GetLength(2); z++) {
                        switch (clockwiseRotations) {
                            case 1:
                                newMap[x, transposedMap.GetLength(1) - y - 1, z] = transposedMap[x, y, z];
                                break;
                            case 2:
                                return transposedMap;
                            case 3:
                                newMap[transposedMap.GetLength(0) - x - 1, y, z] = transposedMap[x, y, z];
                                break;
                            default:
                                return map;
                        }
                    }
                }
            }
            return newMap;
        }
        /// <summary>
        /// Loads all game content and textures.
        /// </summary>
        public static void LoadAllContent() {
            foreach (KeyValuePair<string, GameObjectTexture> entry in textures) {
                entry.Value.LoadContent();
            }
        }
        /// <summary>
        /// Renders a column of the given map onto the screen.
        /// </summary>
        /// <param name="position">The position of the column.</param>
        /// <param name="spriteBatch">The spritebatch to render onto.</param>
        /// <param name="map">The map that we are rendering from.</param>
        private static void renderColumn(Vector2 position, SpriteBatch spriteBatch, MapState map) {
            for (int z = (int) map.getSize().Z - 1; z >= 0; z--) {
                Vector3 currentPosition = new Vector3(position.X, position.Y, z);
                GameObject currentTile = map.getTileAtPosition(currentPosition);
                if (currentTile == null) {
                    continue;
                }
                RenderData currentRenderData = currentTile.GetRenderData();
                spriteBatch.Draw(currentRenderData.texture, isometricToScreen(currentPosition), currentRenderData.sourceRectangle, Color.White);
            }
        }
        /// <summary>
        /// Converts a 3D point in isometric coordinates to a position on the screen
        /// </summary>
        /// <param name="point">The isometric point</param>
        /// <returns>The equivalent screen coordinates</returns>
        private static Vector2 isometricToScreen(Vector3 point) {
            int isoX = (int) (point.X + point.Z);
            int isoY = (int) (point.Y + point.Z);
            int screenX = (isoX - isoY) * TILE_WIDTH / 2;
            int screenY = (isoY + isoX) * TILE_HEIGHT / 2;
            return new Vector2(screenX, screenY);
        }
    }
}