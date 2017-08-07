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
            for(int i = 0; i<rows; i++) {
                int offsetX = 0;
                int length = i+1;
                if (i >= size.Y) {
                    offsetX = i - (int) size.Y + 1;
                    length = (int) size.Y - (i - (int) size.Y + 1);
                }
                for(int j = 0; j<length; j++) {
                    int posX = j + offsetX;
                    int posY = i - j;
                    if(i >= size.Y) {
                        posY = (int) size.Y - j - 1;
                    }
                    Debug.WriteLine(posX + " " + posY);

                    for(int z = (int) size.Z-1; z>=0; z--) {
                        Vector3 currentPosition = new Vector3(posX, posY, z);
                        GameObject currentTile = map.getTileAtPosition(currentPosition);
                        if(currentTile == null) {
                            continue;
                        }
                        RenderData currentRenderData = currentTile.GetRenderData();
                        spriteBatch.Draw(currentRenderData.texture, pointToIsometric(currentPosition), currentRenderData.sourceRectangle, Color.White);
                    }
                }
            }
            spriteBatch.End();
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
            return new Vector2(screenX + 500, screenY);
        }
    }
}