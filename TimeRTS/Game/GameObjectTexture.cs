using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class GameObjectTexture {
        public String spriteSheetPath;
        public Texture2D spriteSheet;
        public static ContentManager content;
        public GameObjectTexture(String spriteSheetPath) {
            this.spriteSheetPath = spriteSheetPath;
        }
        public void LoadContent() {
            this.spriteSheet = content.Load<Texture2D>(this.spriteSheetPath);
        }
        
    }
}
