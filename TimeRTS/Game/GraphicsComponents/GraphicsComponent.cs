using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class GraphicsComponent {
        public String textureName;
        public Texture2D texture;
        public GraphicsComponent(String textureName) {
            this.textureName = textureName;
            this.texture = GameRenderer.textures[this.textureName].spriteSheet;
        }
        public virtual RenderData GetRenderData() {
            return new RenderData(this.texture);
        }
    }
}
