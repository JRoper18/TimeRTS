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
            MapState mapState = state.getMapAtTime(0);
            if (InputHandler.WasPressed(Microsoft.Xna.Framework.Input.Keys.E)) {
                RotateCameraClockwise();
            }
            else if (InputHandler.WasPressed(Microsoft.Xna.Framework.Input.Keys.Q)) {
                RotateCameraCounterClockwise();
            }
            MapState map = mapState;
            Vector3 size = map.getSize();
            bool increasingX = (cameraDirection == Direction.NORTHEAST || cameraDirection== Direction.NORTHWEST);
            bool increasingY = (cameraDirection == Direction.NORTHEAST || cameraDirection== Direction.SOUTHEAST);
            bool yIsColumn = (increasingY != increasingX);
            int currentX = (increasingX) ? 0 : (int) size.X - 1;
            int currentY = (increasingY) ? 0 : (int) size.Y - 1;
            while ((yIsColumn) ? ((increasingY) ? currentY < size.Y : currentY >= 0) : ((increasingX) ? currentX < size.X : currentX >= 0)) {
                while ((yIsColumn) ? ((increasingX) ? currentX < size.X : currentX >= 0) : ((increasingY) ? currentY < size.Y : currentY >= 0)) {
                    renderColumn(new Vector2(currentX, currentY), spriteBatch, map);
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

        private static Vector2 rotatePoint(Vector2 point, Vector3 mapSize) {
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
        private static void renderColumn(Vector2 position, SpriteBatch spriteBatch, MapState map) {
            Vector2 rotated = rotatePoint(position, map.getSize());
            for (int z = 0; z < map.getSize().Z; z++) {
                Vector3 currentPosition = new Vector3(rotated.X, rotated.Y, z);
                GameObject currentTile = map.getTileAtPosition(new Vector3(position.X, position.Y, z));
                if (currentTile == null) {
                    continue;
                }
                RenderData currentRenderData = currentTile.GetRenderData();
                Vector2 screenPosition = isometricToScreen(currentPosition);
                spriteBatch.Draw(currentRenderData.texture, new Rectangle((int) screenPosition.X, (int) screenPosition.Y, TILE_WIDTH, TILE_HEIGHT), currentRenderData.sourceRectangle, Color.White);
            }
        }
        /// <summary>
        /// Converts a 3D point in isometric coordinates to a position on the screen
        /// </summary>
        /// <param name="point">The isometric point</param>
        /// <returns>The equivalent screen coordinates</returns>
        private static Vector2 isometricToScreen(Vector3 point) {
            int isoX = (int)(point.X - point.Z);
            int isoY = (int)(point.Y - point.Z);
            float screenX = (isoX - isoY) * TILE_WIDTH / 2;
            float screenY = (isoY + isoX) * (TILE_HEIGHT / 4);
            return new Vector2(screenX + (GameState.WINDOW_WIDTH / 2), screenY + 100);
        }
        public static void RotateCameraCounterClockwise() {
            if (cameraDirection== Direction.NORTHWEST) {
                cameraDirection= Direction.NORTHEAST;
            }
            else {
                cameraDirection++;
            }
            Debug.WriteLine(cameraDirection);

        }
        public static void RotateCameraClockwise() {
            if (cameraDirection== Direction.NORTHEAST) {
                cameraDirection= Direction.NORTHWEST;
            }
            else {
                cameraDirection--;
            }
        }
        public static Direction GetDirection() {
            return cameraDirection;
        }
        public static Vector3 GetDirectionUnitVector() {
            switch ((int) GameRenderer.GetDirection()) {
                case 0:
                    return new Vector3(0, -1, 0);
                case 1:
                    return new Vector3(1, 0, 0);
                case 2:
                    return new Vector3(0, 1, 0);
                case 3:
                    return new Vector3(-1, 0, 0);
                default:
                    return new Vector3(0, 0, 0);
            }
        }
    }
}