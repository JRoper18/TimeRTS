using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
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
                    Texture2D tempTexture = map.getTileAtPosition(new Vector3(x, y, 0)).GetCurrentTexture(0);
                    spriteBatch.Draw(tempTexture, Vector2.Zero, Color.White);
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