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
        private const int TILE_HEIGHT = 110;
        private const int TILE_WIDTH = (int) (TILE_HEIGHT * 0.886); //Multiply by sqrt(3)/2
        public static float scale = 1;
        public static Vector2 cameraOffset = new Vector2((GameState.WINDOW_WIDTH / 2), 100);
        private static Direction cameraDirection = Direction.NORTHEAST;
        /*
         * Camera directions work like this:
         *           Y  /     --       \  X   
         *             /  NW ----  NE   \
         *            \/    ------      \/
         *                SW ----  SE
         *                    --
         */
        public static Dictionary<String, GameObjectTexture> textures = new Dictionary<string, GameObjectTexture>();
        public static void Initialize() {
            textures.Add("CarUnit", new GameObjectTexture("Images/CarUnit"));
            textures.Add("GrassTile", new GameObjectTexture("Images/GrassTile"));
            textures.Add("StoneTile", new GameObjectTexture("Images/StoneTile"));
            textures.Add("GrassTileStair", new GameObjectTexture("Images/GrassTileStair"));
        }
        /// <summary>
        /// Renders the current game onto the screen. 
        /// </summary>
        /// <param name="state">The current state of the game. </param>
        /// <param name="spriteBatch">The spritebatch we are using to draw.</param>
        public static void Render(GameState state, SpriteBatch spriteBatch) {
            spriteBatch.Begin();
            MapState mapState = state.GetCurrentViewedMap();
            MapState map = mapState;
            Vector3 size = map.getSize();
            bool increasingX = (cameraDirection == Direction.NORTHEAST || cameraDirection== Direction.NORTHWEST);
            bool increasingY = (cameraDirection == Direction.NORTHEAST || cameraDirection== Direction.SOUTHEAST);
            bool yIsColumn = (increasingY != increasingX);
            int currentX = (increasingX) ? 0 : (int) size.X - 1;
            int currentY = (increasingY) ? 0 : (int) size.Y - 1;
            while ((yIsColumn) ? ((increasingY) ? currentY < size.Y : currentY >= 0) : ((increasingX) ? currentX < size.X : currentX >= 0)) {
                while ((yIsColumn) ? ((increasingX) ? currentX < size.X : currentX >= 0) : ((increasingY) ? currentY < size.Y : currentY >= 0)) {
                    RenderColumn(new Vector2(currentX, currentY), spriteBatch, map);
                    if (!yIsColumn) {
                        if (increasingY) {
                            currentY++;
                        }
                        else {
                            currentY--;
                        }
                    }
                    else {
                        if (increasingX) {
                            currentX++;
                        }
                        else {
                            currentX--;
                        }
                    }
                }
                if (yIsColumn) {
                    if (increasingY) {
                        currentY++;
                    }
                    else {
                        currentY--;
                    }
                    currentX = (increasingX) ? 0 : (int)size.X - 1;
                }
                else {
                    if (increasingX) {
                        currentX++;
                    }
                    else {
                        currentX--;
                    }
                    currentY = (increasingY) ? 0 : (int)size.Y - 1;
                }
            }
            spriteBatch.End();
        }
        private static Vector3 RotatePoint(Vector3 point, Vector3 mapSize) {
            Vector2 temp2D = new Vector2(point.X, point.Y);
            Vector2 rotated2D = RotatePoint(temp2D, mapSize);
            return new Vector3(rotated2D.X, rotated2D.Y, point.Z);
        }

        private static Vector2 RotatePoint(Vector2 point, Vector3 mapSize) {
            Vector2 center = new Vector2((mapSize.X-1)/2, (mapSize.Y-1)/2);
            Vector2 translated = point - center;
            Vector2 rotated = point;
            switch (cameraDirection) {
                case Direction.NORTHEAST:
                    return point;
                case Direction.SOUTHEAST:
                    rotated = new Vector2(translated.Y, -translated.X);
                    break;
                case Direction.SOUTHWEST:
                    rotated = new Vector2(-translated.X, -translated.Y);
                    break;
                case Direction.NORTHWEST:
                    rotated = new Vector2(-translated.Y, translated.X);
                    break;
            }
            return rotated + center;
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
        private static void RenderColumn(Vector2 position, SpriteBatch spriteBatch, MapState map) {
            Vector2 rotated = RotatePoint(position, map.getSize());
            for (int z = 0; z < map.getSize().Z; z++) {
                Vector3 currentPosition = new Vector3(rotated.X, rotated.Y, z);
                GameObject currentTile = map.getTileAtPosition(new Vector3(position.X, position.Y, z));
                if (currentTile == null) {
                    continue;
                }
                RenderData currentRenderData = currentTile.GetRenderData();
                Vector2 screenPosition = IsometricToScreen(currentPosition);
                spriteBatch.Draw(currentRenderData.texture, new Rectangle((int) screenPosition.X, (int) screenPosition.Y, (int) ((float) TILE_WIDTH * scale), (int)((float)TILE_HEIGHT * scale)), currentRenderData.sourceRectangle, Color.White);
            }
        }
        /// <summary>
        /// Converts a 3D point in isometric coordinates to a position on the screen
        /// </summary>
        /// <param name="point">The isometric point</param>
        /// <returns>The equivalent screen coordinates</returns>
        public static Vector2 IsometricToScreen(Vector3 point) {
            int isoX = (int)(point.X - point.Z);
            int isoY = (int)(point.Y - point.Z);
            float screenX = scale * ((isoX - isoY) * TILE_WIDTH / 2);
            float screenY = scale * ((isoY + isoX) * (TILE_HEIGHT / 4));
            return new Vector2(screenX + cameraOffset.X, screenY + cameraOffset.Y);
        }
        public static Vector3 ScreenToIsometric(Vector2 point, MapState map) {
            Vector2 offset = (point - cameraOffset);

            float tempX = offset.X * 2 / (TILE_WIDTH * scale);
            float tempY = offset.Y * 4 / (TILE_HEIGHT * scale);
            /* Our point is now (X - Z - Y + Z, Y - Z + X - Z)
             * Simplified: tempX = isoX - isoY
             *             tempY = isoY + isoX - 2(isoZ)
             * Solve: 
             *             isoZ = (isoY + isoX - tempY)/2 = (isoY + tempX + isoY - tempY) / 2 = isoY + ((tempX - tempY) / 2)
             *             isoY = isoZ - ((tempX - tempY) / 2)
             *             isoX = tempX + isoY
             * We can try every possible point facing out from the camera. The one with th highest z will be rendered on top of everything else, so 
             * that's what we're looking for. Assume isoPoint.Z is as high as possible. 
             */
            Vector3 size = map.getSize();
            for (float z = size.Z - 1; z >= 0; z--) {
                float isoY = z - ((tempX - tempY) / 2);
                float isoX = tempX + isoY;
                if(z == 0) {
                    //Count beneath the map as a tile. 
                    return new Vector3((int)isoX, (int)isoY, z);
                }
                if(isoY < 0 || isoX < 0 || isoY >= size.Y || isoX >= size.X) {
                    continue; //Don't check non-existent tiles. 
                }
                Vector3 isoPosition = RotatePoint(new Vector3((int)isoX, (int)isoY, z), size);
                GameObject possible = map.getTileAtPosition(isoPosition);
                if(possible != null) {
                    return isoPosition;
                }
                
            }
            throw new Exception("Position is off-map");
        }
        public static void RotateCameraCounterClockwise() {
            MapState map = GameState.Instance.GetCurrentViewedMap();
            Vector3 previousIsometricFocus;
            previousIsometricFocus = ScreenToIsometric(new Vector2(GameState.WINDOW_WIDTH/2, GameState.WINDOW_HEIGHT/2), map);
            Debug.WriteLine(previousIsometricFocus);
            if (cameraDirection== Direction.NORTHWEST) {
                cameraDirection= Direction.NORTHEAST;
            }
            else {
                cameraDirection++;
            }
            cameraOffset = IsometricToScreen(RotatePoint(previousIsometricFocus, map.getSize()));
        }
        public static void RotateCameraClockwise() {
            MapState map = GameState.Instance.GetCurrentViewedMap();
            Vector3 previousIsometricFocus;
            previousIsometricFocus = ScreenToIsometric(new Vector2(GameState.WINDOW_WIDTH / 2, GameState.WINDOW_HEIGHT / 2), map);
            map.clearPosition(previousIsometricFocus);
            Debug.WriteLine(previousIsometricFocus);
            if (cameraDirection== Direction.NORTHEAST) {
                cameraDirection= Direction.NORTHWEST;
            }
            else {
                cameraDirection--;
            }
            cameraOffset = IsometricToScreen(RotatePoint(previousIsometricFocus, map.getSize()));
        }
        public static Direction GetDirection() {
            return cameraDirection;
        }

        public static void MoveCameraToPoint(Vector3 isoPoint) {

        }
    }
}