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
        public static Dictionary<String, GameObjectTexture> textures = new Dictionary<string, GameObjectTexture>();
        public static void Initialize() {
            textures.Add("GrassTile", new GameObjectTexture("Images/sprite"));
        }
        public static void Render(GameState state, SpriteBatch spriteBatch) {
            //Not permanent, just trying to render anything at all. 
            spriteBatch.Begin();
            MapState map = state.getMapAtTime(0);
            Vector3 size = map.getSize();
            for(int x = 0; x<size.X; x++){
                for (int y = 0; y < size.Y; y++){
                    RenderData renderData = map.getTileAtPosition(new Vector3(x, y, 0)).GetRenderData(0);
                    spriteBatch.Draw(renderData.texture, Vector2.Zero, renderData.sourceRectangle, Color.White);
                }
            }
            spriteBatch.End();
        }
        public static void LoadAllContent() {
            foreach (KeyValuePair<string, GameObjectTexture> entry in textures) {
                entry.Value.LoadContent();
            }
        }
    }
}