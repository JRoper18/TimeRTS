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
        public static void Render(GameState state, SpriteBatch spriteBatch) {
            //Not permanent, just trying to render anything at all. 
            spriteBatch.Begin();
            MapState map = state.getMapAtTime(0);
            Vector3 size = map.getSize();
            int rows = (int)(size.X + size.Y-1);
            for(int i = 0; i<Math.Min(size.X, size.Y); i++) {
                for(int j = 0; j<=i; j++) {
                    int posX = j;
                    int posY = i - j;
                    Debug.WriteLine(posX + " " + posY);
                    renderColumn(posX, posY, (int)size.Z, spriteBatch, map);
                }
            }
            bool isXShortestDimension = (size.X < size.Y);
            int numberOfSameLengthRows = (int) Math.Abs(size.X - size.Y);
            if (isXShortestDimension) {
                for (int i = 0; i < numberOfSameLengthRows; i++) {
                    for (int j = 0; j < size.X; j++) {
                        int posX = j;
                        int posY = i - j + (int) size.X;
                        renderColumn(posX, posY, (int)size.Z, spriteBatch, map);
                    }
                }
                for (int i = 0; i < size.X - 1; i++) {
                    for (int j = 0; j < size.X - i - 1; j++) {
                        int posY = numberOfSameLengthRows + 1 + i + j;
                        int posX = (int)size.X - j - 1;
                        renderColumn(posX, posY, (int)size.Z, spriteBatch, map);
                    }
                }
            }
            else {
                for (int i = 0; i < numberOfSameLengthRows; i++) {
                    for (int j = 0; j < size.Y; j++) {
                        int posX = i + j + 1;
                        int posY = (int) size.Y - 1 - j;
                        renderColumn(posX, posY, (int) size.Z, spriteBatch, map);
                    }
                }
                for(int i = 0; i<size.Y - 1; i++) {
                    for(int j = 0; j < size.Y - i - 1; j++) {
                        int posX = numberOfSameLengthRows + 1 + i + j;
                        int posY = (int) size.Y - j - 1;
                        renderColumn(posX, posY, (int)size.Z, spriteBatch, map);
                    }
                }
            }
            spriteBatch.End();
        }
        private static void renderColumn(int posX, int posY, int sizeZ, SpriteBatch spriteBatch, MapState map) {
            for (int z = sizeZ - 1; z >= 0; z--) {
                Vector3 currentPosition = new Vector3(posX, posY, z);
                GameObject currentTile = map.getTileAtPosition(currentPosition);
                if (currentTile == null) {
                    continue;
                }
                RenderData currentRenderData = currentTile.GetRenderData();
                spriteBatch.Draw(currentRenderData.texture, pointToIsometric(currentPosition), currentRenderData.sourceRectangle, Color.White);
            }
        }
        public static void LoadAllContent() {
            foreach (KeyValuePair<string, GameObjectTexture> entry in textures) {
                entry.Value.LoadContent();
            }
        }

        private static Vector2 pointToIsometric(Vector3 point) {
            int isoX = (int) (point.X + point.Z);
            int isoY = (int) (point.Y + point.Z);
            int screenX = (isoX - isoY) * TILE_WIDTH / 2;
            int screenY = (isoY + isoX) * TILE_HEIGHT / 2;
            return new Vector2(screenX, screenY);
        }
    }
}