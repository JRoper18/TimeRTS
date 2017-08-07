using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class GraphicsComponent {
        public String textureName;
        public GameObjectTexture textureWrapper;
        protected static int cameraDir = 0;
        public GraphicsComponent(String textureName) {
            this.textureName = textureName;
            this.textureWrapper = GameRenderer.textures[this.textureName];
        }
        public virtual RenderData GetRenderData() {
            return new RenderData(this.textureWrapper.spriteSheet);
        }
    }
}
