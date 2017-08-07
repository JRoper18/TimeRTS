using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimeRTS.Game {
    class IsometricGraphicsComponent : GraphicsComponent {
        public IsometricGraphicsComponent(string textureName) : base(textureName) {
        }
        public override RenderData GetRenderData() {
            Rectangle sourceRect = new Rectangle(0, cameraDir * 128, 128, 128);
            return new RenderData(this.textureWrapper.spriteSheet, sourceRect);
        }
    }
}
